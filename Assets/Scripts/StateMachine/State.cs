using UnityEngine;
using System;
using System.Collections;

public abstract class State <T> : StateInterface<T> {
	
	private System.Enum _stateId;
	public System.Enum stateID {
		get {
			return _stateId;
		}
		set {
			_stateId = value;
		}
	}
	public abstract void Enter (T entity);
	public abstract void Execute (T entity);
	public abstract void Exit(T entity);
	
}