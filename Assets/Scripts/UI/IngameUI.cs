using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour {

    [SerializeField] private CanvasRenderer m_canvas;

    [SerializeField] private CanvasRenderer m_canvasWon;

    [SerializeField]
    private Text m_text;

    [SerializeField] 
    private Slider m_manaSlider;

    public Text BeatText
    {
        get
        {
            return m_text;
        }
    }

    public bool m_isDebug = true;

    [SerializeField]
    private Text m_titleText;

    [SerializeField]
    private Text m_percentageText;


    [SerializeField]
    private Text m_fpsText;

    public System.Action onContinue;

    public System.Action onQuit;

    public System.Action onPause;

    public System.Action onNextLevel;

	// Use this for initialization
	void Start () {
        if(m_fpsText)
            m_fpsText.gameObject.SetActive(m_isDebug);
	}
	
	// Update is called once per frame
	void Update () {
        if (m_isDebug)
            if(m_fpsText != null)
                m_fpsText.text = (Mathf.FloorToInt(1f / Time.deltaTime)).ToString();

	}

    IEnumerator lazyFading(CanvasRenderer canvas)
    {
        float alpha = 0f;
        float speed = 5;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * speed;
            canvas.SetAlpha(alpha);
            yield return null;
        }
    }

    public void setReplayActive(bool active)
    {
        m_canvas.SetAlpha(0f);
        m_canvas.gameObject.SetActive(true);
        StartCoroutine(lazyFading(m_canvas));
    }

    public void setWonActive(bool active)
    {
        m_canvasWon.SetAlpha(0f);
        m_canvasWon.gameObject.SetActive(true);
        StartCoroutine(lazyFading(m_canvasWon));
    }

    public void OnBeat(Conductor conductor)
    {
        if (conductor.BeatNum < 5)
        {
            m_text.text = (5 - conductor.BeatNum).ToString();
        }
    }

    public void setBeatText(int beatCount)
    {
        if (m_text != null)
        {
            m_text.text = beatCount.ToString();
        }
    }

    public void setTitleText(string text)
    {
        if (m_titleText != null)
            m_titleText.text = text;
    }

    public void setPercentCompleteText(float percent)
    {
        if (m_percentageText != null)
            m_percentageText.text = string.Format("{0}%", percent);
    }

    public void setActiveBeatText(bool enable)
    {
        if (m_text != null)
            m_text.gameObject.SetActive(enable);
    }

    public void setActiveReplayPopup(bool enable)
    {
        if (m_canvas != null)
        {
            if (enable)
            {
                setReplayActive(enable);
            }
            else
                m_canvas.gameObject.SetActive(false);
        }
    }

    public void setActiveWonPopup(bool enable)
    {
        if (m_canvasWon != null)
        {
            if (enable)
            {
                setWonActive(enable);
            }
            else
                m_canvasWon.gameObject.SetActive(false);
        }
    }

    public void OnContinue()
    {
        if (onContinue != null)
            onContinue.Invoke();
    }

    public void OnQuitClick()
    {
        if (onQuit != null)
            onQuit.Invoke();
    }

    public void OnNextLevelClick()
    {
        if (onNextLevel != null)
            onNextLevel.Invoke();
    }

    public void OnPauseClick()
    {
        if (onPause != null)
        {
            onPause.Invoke();
        }
    }


}
