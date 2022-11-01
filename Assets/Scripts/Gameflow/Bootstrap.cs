using UnityEngine;
using System.Collections;



public class Bootstrap : MonoBehaviour {

	public enum State 
	{
		Init,
		Home,
		Play,
	}

	private static CStateMachine m_stateMachine;

	public static CStateMachine StateMachine
	{
		get 
		{
			return m_stateMachine;
		}
	}

	// Use this for initialization
	void Start () {
		GameObject obj = new GameObject("StateMachine");
		m_stateMachine = obj.AddComponent<CStateMachine>();

		m_stateMachine.Configure(obj);

		m_stateMachine.AddState<CStateInit>(State.Init);
		m_stateMachine.AddState<CStateHome>(State.Home);
		m_stateMachine.AddState<CStatePlay>(State.Play);

		DontDestroyOnLoad(obj);

		m_stateMachine.ChangeState(State.Init);
	}
}
