using UnityEngine;
using System.Collections;

[System.Serializable]
public class HomeController : GUIController {

	private HomeCanvas m_homeCanvas;

	public override bool Show ()
	{
		if (base.Show ()) {
			m_homeCanvas =  m_canvasInstance.GetComponent<HomeCanvas>();
		}

		BindActions(true);
        m_homeCanvas.enableResumeButton(UserData.getInstance().MaxLevel > 0);
        m_homeCanvas.showOptionScreen(false);

        m_homeCanvas.setCheckboxMusic(UserData.getInstance().IsMusicOn);
        m_homeCanvas.setCheckboxSound(UserData.getInstance().IsSoundOn);

		return true;
	}

	private void OnNewGameClick(object sender)
	{
        UserData.getInstance().CurrentLevel = 0;
		Bootstrap.StateMachine.ChangeState(Bootstrap.State.Play);
	}

	private void OnResumeClick(object sender)
	{
        UserData.getInstance().CurrentLevel = UserData.getInstance().MaxLevel;
        Bootstrap.StateMachine.ChangeState(Bootstrap.State.Play);
	}

	private void OnOptionClick(object sender)
	{
        m_homeCanvas.showOptionScreen(true);
	}

    private void OnOptionExitClick(object sender)
    {
        m_homeCanvas.showOptionScreen(false);
    }

    private void OnSoundToggle(bool on)
    {
        UserData.getInstance().IsSoundOn = on;
        UserData.getInstance().save();
    }

    private void OnMusicToggle(bool on)
    {
        UserData.getInstance().IsMusicOn = on;
        UserData.getInstance().save();
    }

	private void BindActions(bool plugin)
	{
		if (plugin)
		{
			m_homeCanvas.onNewGameClick += OnNewGameClick;
			m_homeCanvas.onResumeClick += OnResumeClick;
			m_homeCanvas.onOptionClick += OnOptionClick;
            m_homeCanvas.onOptionExitClick += OnOptionExitClick;
            m_homeCanvas.onPlayClick += OnPlay;
            m_homeCanvas.onMusicToggle += OnMusicToggle;
            m_homeCanvas.onSoundToggle += OnSoundToggle;
		}
		else
		{
			m_homeCanvas.onNewGameClick -= OnNewGameClick;
			m_homeCanvas.onResumeClick -= OnResumeClick;
			m_homeCanvas.onOptionClick -= OnOptionClick;
            m_homeCanvas.onOptionExitClick  -= OnOptionExitClick;
            m_homeCanvas.onPlayClick -= OnPlay;
            m_homeCanvas.onMusicToggle -= OnMusicToggle;
            m_homeCanvas.onSoundToggle -= OnSoundToggle;
		}
	}

	public override void Hide ()
	{
		base.Hide ();
		BindActions(false);
	}

    public void OnPlay(int level)
    {
        UserData.getInstance().CurrentLevel = level;
        Bootstrap.StateMachine.ChangeState(Bootstrap.State.Play);
    }

}
