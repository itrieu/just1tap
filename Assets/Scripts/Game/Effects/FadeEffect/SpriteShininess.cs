using System;
using UnityEngine;

public class SpriteShininess : Effect
{
    public float m_duration = 0.3f;

    public float m_signature = 0.7f;

    public Shader m_shader;

    public Color m_color = Color.red;

    private GameObject m_target;

    public override void play(GameObject target, OnFinishCallback callback, params object[] paramList)
    {
        this.m_callback = callback;
        m_target = target;
        Debug.Assert(m_target != null);
        SpriteRenderer r = target.GetComponent<SpriteRenderer>();
        Debug.Assert(r != null);

        if (r != null)
        {
            StartCoroutine(_AnimateManaShininess(r, m_duration));
        }
    }

    private System.Collections.IEnumerator _AnimateManaShininess (Renderer r, float duration) {
        
        Material material = r.material;
        Shader saveShader = material.shader;
        Color saveColor = material.GetColor("_Color");

        material.shader = m_shader;
        material.SetColor("_Color", m_color);

        float startTime = 0f;
        while (startTime < duration){
            startTime += Time.deltaTime;
            float shininess = Mathf.PingPong(Time.time, this.m_signature);
            material.SetFloat("_Shininess", shininess);
            yield return null; 
        }
       
        if (m_callback != null)
            m_callback.Invoke(this);

        material.shader = saveShader;
        material.SetColor("_Color", saveColor);
    }
}
