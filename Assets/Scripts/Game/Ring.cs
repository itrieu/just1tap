using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{
    public DotObject dot;

    public Vector3 scaleStart;

    public Vector3 scaleEnd;

    public float scaletimer;

    public float scaleduration = 0.1f;

    public bool modeflag = true;

    private void Start()
    {
    }

    private void Update()
    {
        base.transform.Rotate(Vector3.back, 0.5f);
        if (this.dot.ischosen && !this.modeflag)
        {
            this.modeflag = true;
            this.scaleStart = Vector3.zero;
            float num = 2f - this.dot.transform.localScale.x;
            this.scaleEnd = new Vector3(num, num, num);
            this.scaletimer = 0f;
        }
        if (!this.dot.ischosen && this.modeflag)
        {
            this.modeflag = false;
            this.scaleStart = Vector3.one;
            this.scaleEnd = Vector3.zero;
            this.scaletimer = 0f;
        }
        this.scaletimer += Time.deltaTime;
        base.transform.localScale = Vector3.Slerp(this.scaleStart, this.scaleEnd, this.scaletimer / this.scaleduration);
    }
}
