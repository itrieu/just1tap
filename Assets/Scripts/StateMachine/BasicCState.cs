using UnityEngine;
using System.Collections;

public class BasicCState : CState {
	
	public delegate void EnterDelegate(GameObject entity);
	public delegate void ExecuteDelegate(GameObject entity);
	public delegate void ExitDelegate(GameObject entity);
	public EnterDelegate enterDelegate = null;
	public ExecuteDelegate executeDelegate = null;
	public ExitDelegate exitDelegate = null;
	
	public override void Enter(GameObject entity){
		if(enterDelegate != null) enterDelegate(entity);
	}
	public override void Execute(GameObject entity){
		if(executeDelegate != null) executeDelegate(entity);
	}
	public override void Exit(GameObject entity){
		if(exitDelegate != null) exitDelegate(entity);
	}
	
}