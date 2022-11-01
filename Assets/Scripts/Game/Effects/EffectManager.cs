using UnityEngine;
using System;
using System.Collections.Generic;


public delegate void OnFinishCallback(Effect effect);

public abstract class Effect: MonoBehaviour
{
    protected OnFinishCallback m_callback;

    public abstract void play(GameObject target, OnFinishCallback callback, params object[] paramList);    
   
}

public class EffectManager : MonoBehaviour {
    public List<Effect> m_effects;
}
