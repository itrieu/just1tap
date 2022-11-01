using UnityEngine;
using System.Collections;

public class Blinking : Effect {

    private SpriteRenderer m_renderer;

	public override void play(GameObject target, OnFinishCallback callback, params object[] paramList)
    {
        m_renderer = target.GetComponent<SpriteRenderer>();
        StartCoroutine(_play(callback));
    }

    IEnumerator _play( OnFinishCallback callback)
    {
        int count = 2;
        float length = 0.7f;
        float duration = length * count;
        float time = 0.01f;
        float speed = 1 / length;
        float a = 1;
        Color color = m_renderer.color;

        while(count > 0)
        {
            a += speed * Time.deltaTime;
            color.a = a - Mathf.FloorToInt(a);
            m_renderer.color = color;

            time += Time.deltaTime;
            if (time >= duration)
                break;
            yield return null;
        }

        color.a = 1f;
        m_renderer.color = color;

        if (callback != null)
            callback.Invoke(this);
        yield return null;
    }
	
}
