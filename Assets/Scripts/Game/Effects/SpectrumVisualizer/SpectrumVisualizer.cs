using UnityEngine;
using System.Collections;

public class SpectrumVisualizer : MonoBehaviour {
	
	public enum BarStatus
	{
		idle,
		hit,
		miss,
		special
	}

	public Game m_game;

	public AudioSource targetAudio;

	public SpectrumElement[] m_elements;

	private float[] m_spectrum = new float[1024];

	public FFTWindow m_fft = FFTWindow.BlackmanHarris;

	public ColourScheme m_colorScheme;

    public Color defaultColor = new Color(188, 188, 188, 90);

	public float[] getSpectrum()
	{
		return m_spectrum;
	}

    float m_multiply = 1f;

    public float Multiply
    {
        get
        {
            return m_multiply;
        }
        set
        {
            m_multiply = value;
        }
    }

	private void Start()
    {
//		this.Flash(BarStatus.hit);
		m_game.Dot.onHit += OnDotHit;
		m_game.Dot.onDamage += OnDotDamage;
        m_game.DotOther.onHit += OnDotHit;
        m_game.DotOther.onDamage += OnDotDamage;
        m_game.m_conductor.onBeat += OnBeat;

	}

    public Transform m_lineContainer;

    public SpriteData m_spriteData;

	void OnDotDamage (DotObject obj)
	{
		this.Flash(BarStatus.miss);
	}

    public void OnBeat(Conductor d)
    {
//        if (m_game.m_conductor.BeatNum < 5)
//        {
//            int index = (m_game.World.getObject(0) as FloorObject).getSpriteIndex();
//            if (index < 0)
//                index = 0;
//            Color color = m_spriteData.colors[index];
//            Flash (color, default(Color));
//        }
    }

	public void OnDotHit(DotObject sender, FloorObject target)
	{
        Color color = m_spriteData.colors[target.getSpriteIndex()];
        Color endColor = color;
        endColor.a = color.a * 0.5f;
        Flash (color, endColor);
	}

	private void Update()
	{
		this.targetAudio.GetSpectrumData(this.m_spectrum, 0, m_fft);
	}

	public void Flash(Color fromColor, Color toColor)
	{
        
//        Debug.Log(string.Format("from {0} to {1}", fromColor, toColor));
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			this.m_elements[i].Flash(fromColor, toColor);
		}
	}

	public void Flash(BarStatus status)
	{
        Color start = defaultColor;
		ColourScheme currentColourScheme = m_colorScheme;

        if (status == BarStatus.miss)
		{
			start = currentColourScheme.colourBarsMiss;
		}

        Color endColor = start;
        endColor.a = start.a * 0.5f;

        this.m_elements[0].Flash(start, endColor);

	}

	public float GetFailMultiplier()
	{
//		return this.m_game.failbar.value * 10f;
        return m_multiply;
	}

}
