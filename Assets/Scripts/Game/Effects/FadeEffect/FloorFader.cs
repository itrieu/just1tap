using UnityEngine;
using System.Collections;

public abstract class FadeAnimator: MonoBehaviour
{
    public abstract void FadeIn(GameObject obj, float to, float duration);   
    public abstract void FadeOut(GameObject obj, float to, float duration);
}

public class FloorFader : MonoBehaviour {

    public Conductor m_conductor;
    public Game m_game;
    public FadeAnimator m_animator;
    public float m_fadeInAlpha = 1f;
    public float m_fadeOutAlpha = 0f;

    private int m_currentBarIndex = 0;
    private int m_fadeoutIndex = 0;


    public void StartAnimation()
    {
        for (int i = 1; i < m_game.World.Count; ++i)
        {
            m_animator.FadeOut(m_game.World.getObject(i).gameObject, 0f, 0f);
        }
        m_animator.FadeIn(m_game.World.getObject(0).gameObject, m_fadeInAlpha, m_conductor.Crotchet);
        m_currentBarIndex = 0;
    }

    public void initialize(Game game)
    {
        m_conductor.onBeat += OnBeat;
        m_conductor.onBar += OnBar;
        m_game.Dot.onHit += OnHit;
        m_game.DotOther.onHit += OnHit;
        m_maxPreview = m_game.Data.m_data.m_maxPreviewBar;
        StartAnimation();
        m_offset = 5- m_maxPreview;
    }

    void OnDisable()
    {
        m_conductor.onBeat -= OnBeat;
        m_conductor.onBar -= OnBar;
        m_game.Dot.onHit -= OnHit;
        m_game.DotOther.onHit -= OnHit;
    }

    [HideInInspector]
    public int m_maxPreview = 4;

    private int m_offset;

    void OnBeat(Conductor obj)
    {
        if (obj.BeatNum >= m_offset)
        {
            if (obj.BeatNum < 6)
            {
                FadeIn();
            }
        }
    }
     
    private FloorObject m_lastFloor = null;

    void OnHit(DotObject arg1, FloorObject arg2)
    {
        FadeOut();
        FadeIn();
        m_lastFloor = arg2;
    }

    void FadeOut()
    {
        if (m_fadeoutIndex < m_game.World.Count)
        {
            m_animator.FadeOut(m_game.World.getObject(m_fadeoutIndex).gameObject,
                m_fadeOutAlpha, m_conductor.Crotchet * (m_maxPreview * 2));
            m_fadeoutIndex++;
        }
    }

    void FadeIn()
    {
       // Debug.Log("Fade " + m_currentBarIndex);
        m_currentBarIndex++;
        if (m_currentBarIndex < m_game.World.Count)
            m_animator.FadeIn(m_game.World.getObject(m_currentBarIndex).gameObject, m_fadeInAlpha, m_conductor.Crotchet * (m_maxPreview+1));
    }


    void OnBar(Conductor obj)
    {
//        Debug.Log("OnBar " + obj.BarNum + " Beat "+ obj.BeatNum );
    }

    void OnPlay(Conductor obj)
    {
//        throw new System.NotImplementedException();
    }

    void OnReady(Conductor obj)
    {
//        throw new System.NotImplementedException();

    }

}
