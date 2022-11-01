using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CStateMachine : MonoBehaviour{
	private GameObject owner;
	private MonoBehaviour currentStateComp;
	public StateInterface<GameObject> currentState;
	private List<StateInterface<GameObject>> stateStack  = new List<StateInterface<GameObject>>();
	//Dictionnary that will hold all the possible state for this state machine
	public Dictionary<System.Enum,StateInterface<GameObject>> possibleStates = new Dictionary<System.Enum, StateInterface<GameObject>>();
	
	public CStateMachine() {
		currentState = null;
	}
	
	public CStateMachine(GameObject owner) {
		this.owner = owner;
		currentState = null;
	}
	
	public void Configure(GameObject owner, string stateId = null) {
		this.owner = owner;
		//		if(possibleStates.Count > 0) ChangeState(stateId != null ? stateId : possibleStates[possibleStates.Keys]);
		//ChangeState(stateId);
	}
	
	
	public bool CheckStateIDIsValid(System.Enum stateID){
		return possibleStates.ContainsKey(stateID);
	}
	
	
	public void AddState<NewStateType>(System.Enum stateID) where NewStateType : CState{
		//Verify there is no state id conflict
		if(CheckStateIDIsValid(stateID)){
			Debug.LogWarning("Adding state id conflicting with already existing one, skipping");
			return;
		}
		
		if(owner == null) owner = gameObject;
		CState tmpNewState = owner.AddComponent<NewStateType>() as CState;
		tmpNewState.stateID = stateID;
		possibleStates.Add(stateID,tmpNewState);
		tmpNewState.enabled = false;
	}
	
	public void AddState(System.Enum stateID, BasicCState.EnterDelegate enterDelegate, BasicCState.ExitDelegate exitDelegate, BasicCState.ExecuteDelegate executeDelegate = null){
		//Verify there is no state id conflict
		if(possibleStates.ContainsKey(stateID)){
			Debug.LogWarning("Adding state id conflicting with already existing one, skipping");
			return;
		}
		
		if(owner == null) owner = gameObject;
		BasicCState tmpNewState = owner.AddComponent<BasicCState>();
		tmpNewState.stateID = stateID;
		tmpNewState.enterDelegate = enterDelegate;
		tmpNewState.executeDelegate = executeDelegate;
		tmpNewState.exitDelegate = exitDelegate;
		possibleStates.Add(stateID,tmpNewState);
		tmpNewState.enabled = false;
	}
	
	public void ChangeState(System.Enum stateID, bool addToHistory = true) {
		if(owner == null){
			Debug.LogWarning("Trying to use a StateMachine which has no owner set");
			return;
		}

//		Debug.Log (stateID);
		
		if (currentState != null){ 
			//If trying to change to the same state, exiting
			if(stateID == currentState.stateID) return;
			
			(currentState as MonoBehaviour).enabled = false;
			currentState.Exit(owner);
		}
		
		StateInterface<GameObject> newState =  possibleStates[stateID];
		currentState = newState;
		
		if (currentState != null) {
			if(addToHistory) stateStack.Add(currentState);
			currentStateComp = currentState as MonoBehaviour;
			currentStateComp.enabled = true;
			currentState.Enter(owner);
		}
	}
	
	public void  RevertToPreviousState() {
		if (stateStack.Count > 1)
		{
			//Delete latest state
			stateStack.RemoveAt(stateStack.Count-1);
			//Switch to previous state
			ChangeState(stateStack[stateStack.Count-1].stateID,false);
		}
	}
	
	public List<StateInterface<GameObject>> getStateHistoric(){
		return stateStack;
	}
	
	// Tuan : a hack to know which state before current state
	public StateInterface<GameObject> GetPreviousState ()
	{
		StateInterface<GameObject> preState = null;
		
		List<StateInterface<GameObject>> stateList = getStateHistoric();
		if (stateList.Count > 1)
		{
			preState = stateList[stateList.Count - 2];
		}
		
		return preState;
	}
	
	// MapState is the root state, so every time go to this we need to clear the stack
	public void ClearStateHistoric ()
	{
		stateStack.Clear();
	}
	
	public void Update() {
		//The handling of update and behaving is not done by the state machine but directly by the component
	}
	
}