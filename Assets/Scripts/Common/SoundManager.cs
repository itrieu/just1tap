using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private static SoundManager s_instance;

    [SerializeField] private AudioClip[] m_clips;
    [SerializeField] private AudioSource m_audioSource;

    public static SoundManager getInstance()
    {
        return s_instance;
    }

	void Awake () 
    {
        if (s_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        s_instance = this;
        DontDestroyOnLoad(this.gameObject);
	}
	

    public void playSfx(int id)
    {
        if (!UserData.getInstance().IsSoundOn)
            return;
        
        if (id < m_clips.Length)
        {
            m_audioSource.Stop();
            m_audioSource.clip = m_clips[id];
            m_audioSource.Play();
        }
    }
}
