using UnityEngine;

public class GameStateFailed: GameStateBase
{
    private bool m_waitForReplace = true;

	public override void Enter (UnityEngine.GameObject entity)
	{
		base.Enter (entity);
		m_Game.Responsive = true;

        UserData.getInstance().save();

      
        Effect effect = m_Game.m_effectManager.m_effects[0];
        m_Game.Dot.enabled = false;
        m_Game.DotOther.enabled = false;

        effect.play(m_Game.ChoosenDot.other.GetComponentInChildren<Renderer>().gameObject, startCamZoom);

        m_waitForReplace = m_Game.ChoosenDot.m_isEnableReplay;
	}

    private void startCamZoom(Effect e)
    {
        m_Game.StartCoroutine(LazyZoomout());
    }

    private System.Collections.IEnumerator LazyZoomout()
    {
        yield return new WaitForSeconds(0.3f);
        m_Game.m_gameCam.zoomout();
        yield return new WaitForSeconds(0.3f);
        m_Game.m_ingameUI.setActiveReplayPopup(true);
    }

	public override void Exit ()
	{

	}

	public override void Update ()
	{
        if (m_waitForReplace)
        {
            bool isFinish = !m_Game.ChoosenDot.other.IsReplay;
            if (Input.anyKeyDown || Utils.GetTouch())
            {
                isFinish = true;
                m_Game.m_ingameUI.setActiveReplayPopup(true);
            }

            if (isFinish)
            {
                m_waitForReplace = false;
//                m_Game.Dot.enabled = true;
//                m_Game.DotOther.enabled = true;
                m_Game.m_gameCam.zoomout();
                m_Game.m_ingameUI.setActiveReplayPopup(true);
            }
        }
	}

    void OnQuit()
    {
        m_Game.Quit();
    }

    void OnContinue()
    {
        m_Game.Restart();
    }
}