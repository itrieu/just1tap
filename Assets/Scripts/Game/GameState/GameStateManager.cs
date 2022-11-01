using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class GameState {
	public System.Enum stateId;
	
	public abstract void Enter(GameObject entity);
	
	public abstract void Exit();
	
	public abstract void Update();
}

public class GameStateManager: MonoBehaviour
{
	GameObject owner;
	GameState currentState;
    GameState lastState = null;

    public GameState LastState
    {
        get
        {
            return lastState;
        }
    }
	
	public static float stateTime = 0f;
	Dictionary<System.Enum, GameState> stateStack= new Dictionary<Enum, GameState>();
	
	public void Configure (GameObject go) {
		this.owner = go;
	}
	
	public void AddState <T> (System.Enum stateId, T state) where T: GameState   {
		if (!stateStack.ContainsKey(stateId)) {
			state.stateId = stateId;
			stateStack.Add(stateId, state);
		} else {
			Debug.LogWarning ("Already added state " + stateId );
		}
	}
	
	public GameState GetState(System.Enum stateId) {
		GameState t = null;
		if (stateStack.ContainsKey(stateId)) {
			t = stateStack[stateId];
		} 
		return t;
	}
	
	public void ChangeState(System.Enum stateId) {
//		Debug.Log ("change state " + stateId);
		if (stateStack.ContainsKey(stateId)) {
			if (currentState != null){
				if(stateId == currentState.stateId) 
					return;
				lastState = currentState;
				currentState.Exit();
			}
			currentState = stateStack[stateId];
			currentState.Enter(owner);
			stateTime = 0f;
		} else {
            throw new Exception(string.Format("State {0} selectionMode exist in state stack", stateId));
		}
	}
	
	public void Update() {
		if (isPlaying) {
			if (currentState != null) {
				stateTime += Time.deltaTime;
				currentState.Update();
			}
		}
	}
	
	public GameState CurrentState {
		get {
			return currentState;
		}
	}
	
	public System.Enum CurrentStateId {
		get {
			return currentState.stateId;
		}
	}

	bool isPlaying = true;

	public bool IsPlaying {
		get {
			return isPlaying;
		}
		set { 
			isPlaying = value;
		}
	}

    private System.Enum m_storedState;

    public void SaveState()
    {
        m_storedState = CurrentState.stateId;
    }

    public void RestoreState()
    {
        ChangeState(m_storedState);
    }
}