//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
public class GameStatePause: GameStateBase
{
	public override void Enter(UnityEngine.GameObject entity)
	{
		base.Enter(entity);
//        m_Game.m_ingameUI.setActiveContinuePopup(true);
//        m_Game.m_ingameUI.onQuit += OnQuit;
//        m_Game.m_ingameUI.onContinue += OnContinue;
//        m_Game.Pause();
	}

	public override void Exit()
	{
//        m_Game.m_ingameUI.setActiveContinuePopup(false);
//        m_Game.m_ingameUI.onQuit -= OnQuit;
//        m_Game.m_ingameUI.onContinue -= OnContinue;
//        m_Game.Resume();
	}

	public override void Update()
	{
	}

    void OnQuit()
    {
        m_Game.Quit();
    }

    void OnContinue()
    {
        m_Game.StateManager.RestoreState();
    }
}

