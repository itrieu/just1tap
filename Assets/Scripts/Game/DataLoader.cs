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
using UnityEngine;

public class DataLoader
{
	public LevelDataMaster loadLevel(int level) 
	{
		string assetPath = "levels/" +  level.ToString ();
		LevelDataMaster data = Resources.Load<LevelDataMaster>(assetPath);
		return data;
	}

	public ClipData loadClip(string clip)
	{
		ClipData data = Resources.Load<ClipData>(clip);
		return data;
	}
}
