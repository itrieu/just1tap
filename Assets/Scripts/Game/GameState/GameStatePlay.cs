using System;
using UnityEngine;

public class GameStatePlay: GameStateBase
{
//	private GameData m_data;
    private Conductor m_conductor;

    private float damageval = 0.5f;

    private float m_cooldownVal = 0.2f;

	public override void Enter(UnityEngine.GameObject entity)
	{
		base.Enter(entity);

//		m_data = m_Game.Data;
        m_conductor = m_Game.m_conductor;

        BindEvents(m_Game.Dot);
		BindEvents(m_Game.DotOther);

        m_conductor.onBeat += m_Game.m_ingameUI.OnBeat;
		m_conductor.onReady += this.OnConductorReady;
		m_conductor.onPlay += this.OnConductorPlay;
		m_conductor.onFinished += this.OnConductorFinished;
       
		m_conductor.StartMusic();
    }

	public override void Exit()
	{
		RemoveEvents(m_Game.Dot);
		RemoveEvents(m_Game.DotOther);

        m_conductor.onBeat -= m_Game.m_ingameUI.OnBeat;
		m_conductor.onReady -= this.OnConductorReady;
		m_conductor.onPlay -= this.OnConductorPlay;
		m_conductor.onFinished -= this.OnConductorFinished;
    }

	public override void Update()
	{
        if (m_Game.Mana <= 0)
        {
            FailLevel(true);
            return;
        }

        if (m_Game.failed)
            return;
        
        m_Game.Mana += this.m_cooldownVal * m_conductor.Deltasongpos / (m_conductor.Crotchet * 0.5f);

        if (m_Game.won)
        {
            m_Game.StateManager.ChangeState(Game.State.Won);
            return;
        }

		if (m_Game.m_conductor.BeatNum >= 4)
		{
			m_Game.CountDowned = true;
		}

		m_Game.isalive = (m_Game.CountDowned && !m_Game.failed && !m_Game.won && m_Game.Responsive);

		if (Input.anyKeyDown || Utils.GetTouch())
		{
			if (m_Game.CountDowned && m_Game.isalive)
			{
                m_Game.ChoosenDot.SwitchChosen();
			}
		}

        if (this.m_Game.isalive && 
            this.m_conductor.BeatNum > 5)
        {
            if ((this.m_conductor.Position - this.m_conductor.ActualLastHit) >
                (this.m_conductor.Crotchet * 2f / this.m_Game.speed))
            {
                OnMissed(this.m_Game.ChoosenDot.other);
            }
        }
	}

    private void OnSwitched(DotObject choosenDot)
    {
        
    }

    private void BindEvents(DotObject dot)
	{
		if(dot != null) 
		{
			dot.onDamage += OnDamage;
			dot.onHit += OnHit;
//			dot.onMissed += OnMissed;
			dot.onStop += OnStopped;
            dot.onSwitched += OnSwitch;
         
		}
    }
	
	private void RemoveEvents(DotObject dot)
	{
		if(dot != null) 
		{
			dot.onDamage -= OnDamage;
			dot.onHit -= OnHit;
//			dot.onMissed -= OnMissed;
			dot.onStop -= OnStopped;
            dot.onSwitched -= OnSwitch;
		}
    }
   
	// CONDUCTOR EVENTS
	private void OnConductorReady(Conductor sender)
	{
		m_Game.ChoosenDot.ischosen = true;
        m_Game.m_ingameUI.setActiveBeatText(true);
	}

	private void OnConductorPlay(Conductor sender)
	{
        m_Game.m_ingameUI.setActiveBeatText(false);
	}

	private void OnConductorFinished(Conductor sender)
	{
	}
	// END CONDUCTOR EVENTS

	// DOT EVENTS
	private void OnHit(DotObject dotObj, FloorObject floorObj)
	{
        // set current dot
        m_Game.ChoosenDot = dotObj.other;
        m_Game.CurrentSeqID++;

        // move cam
        m_Game.m_gameCam.frompos = m_Game.m_gameCam.transform.position;
        m_Game.m_gameCam.topos = new Vector3(m_Game.ChoosenDot.transform.position.x, 
            m_Game.ChoosenDot.transform.position.y, 
            m_Game.m_gameCam.transform.position.z);
        m_Game.m_gameCam.timer = 0f;
        m_Game.m_gameCam.Pulse();

        // update seq
        m_Game.CurrentSeqID = floorObj.seqID;

        // update percent
        m_Game.Percent = (float)(m_Game.CurrentSeqID + 1) / m_Game.World.Count * 100f;
        m_Game.Percent = Mathf.Floor(m_Game.Percent);
       
		if(floorObj.isportal) 
		{
			m_Game.won = true;
		}

        // ui & effect
        m_Game.m_ingameUI.setPercentCompleteText(m_Game.Percent);
        GameWorld world = m_Game.World;
        for (int i = 0, n = world.Count; i < n; i++)
        {
            FloorObject obj = world.getObject(i) as FloorObject;
            if (obj != null && obj.flashycolor)
            {
                if (obj.floorsprite.isVisible)
                    obj.SetToRandomSprite();
            }
            else
            {
                break;
            }
        }
        if (floorObj.IsSpecial)
        {
            
            //          scrCamera.instance.FlashBg(new Color?(Color.green));
        }
        if (floorObj.sfxnum != -1)
        {
            SoundManager.getInstance().playSfx(floorObj.sfxnum);
        }
	}

    private void OnSwitch(DotObject dot, FloorObject floor)
    {
        if (floor != null)
        {
            m_Game.ChoosenDot = dot.other;
            m_Game.m_gameCam.frompos = m_Game.m_gameCam.transform.position;
            m_Game.m_gameCam.topos = new Vector3(m_Game.ChoosenDot.transform.position.x, 
                m_Game.ChoosenDot.transform.position.y, 
                m_Game.m_gameCam.transform.position.z);
            m_Game.m_gameCam.timer = 0f;
            m_Game.m_gameCam.Pulse();
        }
    }

	private void OnDamage(DotObject dot)
	{
        float damage;
        if ((this.m_conductor.Position - this.m_conductor.ActualLastHit) >
            (this.m_conductor.Crotchet / this.m_Game.speed))
        {
            damage = this.damageval * 2 + 0.1f;
        }
        else
        {
            damage = this.damageval;
        }
        m_Game.takeDamage(damage);
//        m_Game.m_effectManager.m_effects[0].play(this.m_Game.ChoosenDot.other.GetComponentInChildren<SpriteRenderer>().gameObject, null);
        m_Game.m_visualizer.Flash(SpectrumVisualizer.BarStatus.miss);
       
	}

	private void OnMissed(DotObject dot)
	{
        m_Game.takeDamage(damageval);
//        m_Game.m_effectManager.m_effects[0].play(this.m_Game.ChoosenDot.other.GetComponentInChildren<SpriteRenderer>().gameObject, null);
        m_Game.m_visualizer.Flash(SpectrumVisualizer.BarStatus.miss);
       
	}

	private void OnStopped(DotObject dot)
	{
		
	}

	public void FailLevel(bool disable = false)
	{
		if (m_Game.failed || !m_Game.isalive )
            return;
  
		m_Game.failed = true;

        m_Game.Responsive = !disable;

		m_conductor.GetComponent<AudioSource>().Pause();

        m_Game.Dot.enabled = false;
        m_Game.DotOther.enabled = false;

        if (m_Game.ChoosenDot.other.m_isEnableReplay)
        {
            m_Game.ChoosenDot.other.replayLastPosition();
            m_Game.StateManager.ChangeState(Game.State.Failed);
        }

        if (m_Game.won)
        {
            m_Game.StateManager.ChangeState (Game.State.Won);
        }
        else
        {
            m_Game.StateManager.ChangeState (Game.State.Failed);    
        }

	}
	// END DOT EVENTS

}
