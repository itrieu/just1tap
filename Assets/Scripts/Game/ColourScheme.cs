using System;
using UnityEngine;

[Serializable]
public class ColourScheme: ScriptableObject
{
	public Color colourBg = Color.white;

	public Color colourBarsHit = Color.green;

	public Color colourBarsIdle = Color.white;

	public Color colourText = Color.black;

	public Color colourBarsMiss = Color.red;
}
