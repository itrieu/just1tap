using UnityEngine;
using System.Collections;

public class SpectrumElement : MonoBehaviour
{
	public SpectrumVisualizer m_manager;

	public int m_freq;

	private Vector3 oriScale;

	public Vector3 updateScale;


	private float maxheight = 1000f;

	private  Color colorStart = Color.white;

	private  Color colorEnd = Color.clear;

	public float colorTimer;

	public float m_colorDuration = 0.4f;

	private float colorDurationInv;

	private Material m_material;

    public Vector3 size;

	private void Start()
	{
		this.oriScale = base.transform.localScale;
		this.updateScale = this.oriScale;
		this.colorDurationInv = 1f / this.m_colorDuration;
		this.m_freq = Mathf.RoundToInt(base.transform.localPosition.x / 18f * 600f + 20f); // => x >= -20*18/600 => x >= -0.6f 
        Renderer renderer = GetComponent<Renderer>();
        m_material = renderer.sharedMaterial;
		m_material.color = this.colorEnd;
        size = renderer.bounds.size;
	}

	private void Update()
	{
		float[] spectrum = this.m_manager.getSpectrum();

		float num = Mathf.Max(updateScale.y - 0.1f - this.oriScale.y, 
			spectrum[this.m_freq] * this.maxheight * (this.m_manager.GetFailMultiplier() + 1f));

		updateScale.y = num + this.oriScale.y;
		base.transform.localScale = updateScale;

		this.colorTimer += Time.deltaTime;
//        Debug.Log(m_material.color);
		m_material.color = Color.Lerp(this.colorStart, this.colorEnd, this.colorTimer * this.colorDurationInv);
	}

	public void Flash(Color start, Color end)
	{
		this.colorStart = start;
		this.colorEnd = end;
		this.colorTimer = 0f;
	}
}
