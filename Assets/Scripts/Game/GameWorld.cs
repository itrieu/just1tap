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
using System.Collections.Generic;
using UnityEngine;

public class GameWorld: MonoBehaviour
{
   
	private List<GameEntity> mObjects = new List<GameEntity>();

	public GameWorld ()
	{
	}

	public void addObject(GameEntity o)
	{
		mObjects.Add(o);
	}

	public void removeObject(GameEntity o)
	{
		mObjects.Remove(o);
	}

	public GameEntity getObject(int index)
	{
		return mObjects[index];
	}

	public int Count 
	{
		get 
		{
			return mObjects.Count;
		}
	}

}
