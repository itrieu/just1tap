using UnityEngine;
using System.Collections;

public abstract class CState : MonoBehaviour,StateInterface<GameObject> {
	private System.Enum _stateID;
	public System.Enum stateID {
		get {
			return _stateID;
		}
		set {
			_stateID = value;
		}
	}
	
	public abstract void Enter (GameObject entity);
	public abstract void Execute (GameObject entity);
	public abstract void Exit(GameObject entity);
	
	// Update is called once per frame
	void Update () {
		Execute(gameObject);
	}
}