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

public class GameStateInit: GameStateBase
{
	private LevelLoader m_loader;

	private DataLoader m_dataLoader;

	public override void Enter(UnityEngine.GameObject entity)
	{
		base.Enter(entity);

		m_loader = new LevelLoader();
		m_dataLoader = new DataLoader();

		// change to playstate
		Setup();

		m_Game.StateManager.ChangeState(Game.State.Play);
	}

	private void Setup()
	{
		// Load data
		GameData data = new GameData();
		int level = UserData.getInstance().CurrentLevel;
		data.m_data = m_dataLoader.loadLevel(level);
		data.m_clip = m_dataLoader.loadClip(data.m_data.clipName);

        // Make level
        m_loader.MakeLevel(m_Game.World, m_Game.m_floorPrefab, data.m_clip.levelData);

        // add 2 dots
        m_Game.Dot = m_loader.createGameEntity<DotObject>(m_Game.m_dotPrefab, m_Game.World.transform);
        m_Game.DotOther = m_loader.createGameEntity<DotObject>(m_Game.m_dotOtherPrefab, m_Game.World.transform);
       
        m_Game.initialize(data);

        m_Game.m_ingameUI.onQuit += m_Game.Quit;
        m_Game.m_ingameUI.onContinue += m_Game.Restart;
        m_Game.m_ingameUI.onNextLevel += m_Game.nextLevel;
	}

	public override void Exit()
	{
		// clean up
		m_loader = null;
		m_dataLoader = null;

    }

	public override void Update()
	{
       
    }
}