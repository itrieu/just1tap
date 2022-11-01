using UnityEngine;
using UnityEngine.UI;
using System;

public class HomeCanvas : UICanvas 
{
    [SerializeField] private Button m_resumeButton;
    [SerializeField] private Text m_resumeButtonText;
    [SerializeField] private Toggle m_musicToggle;
    [SerializeField] private Toggle m_soundToggle;

    public System.Action<object> onNewGameClick;
	public System.Action<object> onResumeClick;
	public System.Action<object> onOptionClick;
    public System.Action<object> onOptionExitClick;
    public System.Action<int> onPlayClick;
    public System.Action<bool> onMusicToggle;
    public System.Action<bool> onSoundToggle;

    [SerializeField]
    private GameObject m_homePanelRef;

    [SerializeField]
    private GameObject m_optionPanelRef;

	public void OnNewGameClick()
	{
		if(onNewGameClick != null)
			onNewGameClick.Invoke(this);
	}

	public void OnResumeClick()
	{
		if (onResumeClick != null)
			onResumeClick.Invoke (this);
	}

	public void OnOptionClick()
	{
		if (onOptionClick != null)
			onOptionClick.Invoke (this);
	}

    public void enableResumeButton(bool enable)
    {
        if (m_resumeButton != null)
        {
            m_resumeButton.interactable = enable;
        }
    }

    public void showOptionScreen(bool show)
    { 
        if (show)
        {
            if (m_optionPanelRef != null)
                m_optionPanelRef.transform.SetSiblingIndex(1);
        }
        else
        {
            if (m_homePanelRef != null)
                m_homePanelRef.transform.SetSiblingIndex(1);
        }
    }

    public void setCheckboxSound(bool on)
    {
        if (m_soundToggle != null)
            m_soundToggle.isOn = on;
    }

    public void setCheckboxMusic(bool on)
    {
        if (m_musicToggle != null)
            m_musicToggle.isOn = on;
    }

    public void OnOptionExitClick()
    {
        if (onOptionClick != null)
            onOptionExitClick.Invoke(this);
    }

    public void OnPlayClick(Dropdown dropdown)
    {
        if (onPlayClick != null)
        {
            onPlayClick.Invoke(dropdown.value);
        }
    }

    public void OnMusicToggleClick(Toggle toggle)
    {
        if (onMusicToggle != null)
            onMusicToggle.Invoke(toggle.isOn);
    }

    public void OnSoundToggleClick(Toggle toggle)
    {
        if (onSoundToggle != null)
            onSoundToggle.Invoke(toggle.isOn);
    }
}
