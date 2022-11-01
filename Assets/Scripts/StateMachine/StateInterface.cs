using UnityEngine;
using System.Collections;

public interface StateInterface<T> {
	System.Enum stateID {
		get;
		set;
	}
	void Enter (T entity);
	void Execute (T entity);
	void Exit(T entity);
}