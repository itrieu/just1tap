using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeEffectAnimator : FadeAnimator {

  
    private struct AnimationHolder
    {
        GameObject obj;
        float fromAlpha;
        float toAlpha;
        float duration;
    }

    public override void FadeIn(GameObject obj, float to,  float duration)
    {
//        Debug.Log("Fade in " + obj.name);
        Fade(obj, duration, 0f, to);
    }

    public override void FadeOut(GameObject obj, float to, float duration)
    {
//        Debug.Log("Fade out " + obj.name);
        Fade(obj, duration, 1f, to);
    }

    private void Fade(GameObject obj, float duration, float fromAlpha, float toAlpha)
    {
        if (fromAlpha == toAlpha)
            return;
        
        if (duration > 0)
        {
            StartCoroutine(_Fade(obj, duration, fromAlpha, toAlpha));
        }
        else
        {
            SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer r in renderers)
            {
                Color color = r.color;
                color.a = toAlpha;
                r.color = color;
            }
        }
    }

    private IEnumerator _Fade(GameObject obj, float duration, float fromAlpha, float toAlpha)
    {
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
        float step = (toAlpha - fromAlpha) / duration;

        float value = fromAlpha;
        float min = Mathf.Min(fromAlpha, toAlpha);
        float max = Mathf.Max(fromAlpha, toAlpha);
        
        while (value != toAlpha)
        {
            value += step * Time.deltaTime;
            value = Mathf.Clamp(value, min, max);
//            Debug.Log("step " + step + " value " + value);
            foreach (SpriteRenderer r in renderers)
            {
                Color color = r.color;
                color.a = value;
                r.color = color;
            }
            yield return null;
        }
        yield return null;
    }
}
