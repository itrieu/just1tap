using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Conductor : MonoBehaviour {

	public AudioSource song;

    private bool m_isPlaying = true;

	void Awake()
	{
//        QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
//        Debug.Log(">>" + AudioSettings.dspTime);
	}

    public void initialize(GameData data)
    {
        if (Offset == 99)
        {
            SetOffsetToDefaults();
        }    
        this.setClip(data.m_clip);
    }

    public void mute()
    {
        song.volume = 0.001f;
    }

	float crotchet;

	public float Crotchet
	{
		get 
		{
			return crotchet;
		}
	}

	float m_songPosition;

	public float Position
	{
		get {
			return m_songPosition;
		}
	}

	double m_dspTimeSong;

	public double DspTime
	{
		get {
			return m_dspTimeSong;
		}
	}

    float m_offset;

    public float Offset
    {
        get
        {
            return m_offset;
        }
    }

	float m_addoffset;

    float deltasongpos;

    public float Deltasongpos
    {
        get
        {
            return deltasongpos;
        }
    }

	float nextbeattime;

	int m_beatnumber;

	public int BeatNum
	{
		get {
			return m_beatnumber;
		}
	}

	float nextbartime;

	int m_barNumber;

	public int BarNum
	{
		get { return m_barNumber; }
	}

	float crotchetsperbar = 8;

    float buffer = 0f;

	[HideInInspector] public float lastHitPosition;

	[HideInInspector] public float ActualLastHit;

	private ClipData m_clip;

	public Action<Conductor> onBeat;

	public Action<Conductor> onReady;

	public Action<Conductor> onPlay;

	public Action<Conductor> onBar;

	public Action<Conductor> onFinished;

	public void setClip(ClipData clip)
	{
		m_clip = clip;

		this.GetComponent<AudioSource>().clip = m_clip.clip;
		this.m_addoffset = m_clip.addoffset;
		this.song.pitch = m_clip.multiplier;
        this.song.volume = m_clip.volume;

		this.crotchet = 60f / m_clip.bpm;
		this.nextbeattime = 0f;
		this.nextbartime = 0f;
	}

	public void SetOffsetToDefaults()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			this.m_offset = 0.23f;
		}
		else
		{
			this.m_offset = 0.1f;
		}
	}

	public void StartMusic(Action onComplete = null)
	{
		float deplayTime = this.buffer + 0.5f;

		this.m_dspTimeSong = AudioSettings.dspTime + deplayTime;

		base.StartCoroutine(this.Countdown(onComplete)); 
	}

	private IEnumerator Countdown(Action onComplete)
	{
        #if UNITY_ANDROID && !UNITY_EDITOR

        while(AudioSettings.dspTime < this.m_dspTimeSong)
        {
            yield return null;
        }

        this.OnReady();

        this.song.Play();

        while(m_beatnumber < 5) {
            yield return null;
        }

        this.OnPlay();
        #else
        this.OnReady();

        while(AudioSettings.dspTime < this.m_dspTimeSong)
        {
            yield return null;
        }

        this.song.Play();

        while(m_beatnumber < 5) {
        yield return null;
        }

        this.OnPlay();
        #endif
		

		yield return null;
		
		this.OnFinished();
	}
	
    // Update is called once per frame
    void OnAudioFilterRead(float[] data, int channels)
	{
        if (m_isPlaying)
        {
            float num = this.m_songPosition;
            this.m_songPosition = (float)(AudioSettings.dspTime - this.m_dspTimeSong) * this.m_clip.multiplier
                - this.m_offset - this.m_addoffset;
            this.deltasongpos = this.m_songPosition - num;
        }
	}

    void FixedUpdate()
    {
        if (this.m_songPosition > this.nextbeattime)
        {
            this.nextbeattime += this.crotchet;
            this.m_beatnumber++;
            this.OnBeat();
        }
        if (this.m_songPosition > this.nextbartime)
        {
            this.OnBar();
            this.nextbartime += this.crotchet * (float)this.crotchetsperbar;
            this.m_barNumber++;
        }
    }

	private void OnBeat()
	{
		if(onBeat != null)
			onBeat.Invoke(this);
	}
	
	private void OnBar()
	{
		if(onBar != null)
			onBar.Invoke(this);
	}

	private void OnReady()
	{
		if(onReady != null)
			onReady.Invoke(this);
	}

	private void OnPlay()
	{
		if(onPlay != null)
			onPlay.Invoke(this);
	}

	private void OnFinished()
	{
		if(onFinished != null)
			onFinished.Invoke(this);
	}

    public void pause()
    {
        song.Pause();
        m_isPlaying = false;
    }

    public void resume()
    {
        song.UnPause();
        m_isPlaying = true;
    }
}
