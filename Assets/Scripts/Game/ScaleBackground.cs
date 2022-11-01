using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class ScaleBackground : MonoBehaviour {
    /// <summary> Do you want the sprite to maintain the aspect ratio? </summary>
    public bool KeepAspectRatio = true;
    /// <summary> Do you want it to continually check the screen size and update? </summary>
    public bool ExecuteOnUpdate = true;

    void Start () {
        m_cacheTrans = this.transform;
        Resize(KeepAspectRatio);
    }

    void Update () {
        if (ExecuteOnUpdate)
            Resize(KeepAspectRatio);
    }

    Vector3 m_scale = Vector3.one;
    Transform m_cacheTrans;
    float m_lastCamSize = 0f;

    /// <summary>
    /// Resize the attached sprite according to the camera view
    /// </summary>
    /// <param name="keepAspect">bool : if true, the image aspect ratio will be retained</param>
    void Resize(bool keepAspect = false)
    {
        float orthographicSize = Camera.main.orthographicSize;
        if (m_lastCamSize == orthographicSize)
            return;
        m_lastCamSize = orthographicSize;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
//        transform.localScale = new Vector3(1, 1, 1);

        // example of a 640x480 sprite
        float width = sr.sprite.bounds.size.x; // 4.80f
        float height = sr.sprite.bounds.size.y; // 6.40f
        float x, y;

        // and a 2D camera at 0,0,-10
        float worldScreenHeight = orthographicSize * 2f; // 10f
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; // 10f

//        Vector3 imgScale = new Vector3(1f, 1f, 1f);

        // do we scale according to the image, or do we stretch it?
        if (keepAspect)
        {
            Vector2 ratio = new Vector2(width / height, height / width);
            if ((worldScreenWidth / width) > (worldScreenHeight / height))
            {
                // wider than tall
                x = worldScreenWidth / width;
                y = m_scale.x * ratio.y; 
            }
            else
            {
                // taller than wide
                y = worldScreenHeight / height;
                x = m_scale.y * ratio.x;             
            }
        }
        else
        {
            x = worldScreenWidth / width;
            y = worldScreenHeight / height;
        }

        // apply change
        if (x != m_scale.x || y != m_scale.y)
        {
            m_scale.x = x;
            m_scale.y = y;
            m_cacheTrans.localScale = m_scale;
        }
    }
}
