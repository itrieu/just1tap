using UnityEngine;
using System;

public class StateMachine<T>{
	private T owner;
	private StateInterface<T> CurrentState;
	private StateInterface<T> PreviousState;
	
	public StateMachine() {
		CurrentState = null;
		PreviousState = null;
	}
	
	public StateMachine(T owner) {
		this.owner = owner;
		CurrentState = null;
		PreviousState = null;
	}
	
	public void Configure(T owner, StateInterface<T> InitialState) {
		this.owner = owner;
		ChangeState(InitialState);
	}
	
	public void  Update() {
		if (CurrentState != null) CurrentState.Execute(owner);
		//if (CurrentState != null) CurrentState.Execute();
	}
	
	public void  ChangeState( StateInterface<T> NewState) {
		PreviousState = CurrentState;
		if (CurrentState != null) CurrentState.Exit(owner);
		CurrentState = NewState;
		if (CurrentState != null) CurrentState.Enter(owner);
	}
	
	public void  RevertToPreviousState() {
		if (PreviousState != null) ChangeState(PreviousState);
	}
	
	// Tuan: temp for store state to return
	public State<T> GetPreviousState () { 
		return PreviousState as State<T>; 
	}
}