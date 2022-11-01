using UnityEngine;
using System.Collections;

public class UserData
{
	private static UserData s_userData;

    public const int MAX_LEVEL = 17;

	private int m_currentLevel;

    private int m_maxLevel;

    private bool m_isSoundOn = true;

    public bool IsSoundOn
    {
        get
        {
            return m_isSoundOn;
        }
        set
        {  
            m_isSoundOn = value;
        }
    }

    private bool m_isMusicOn = true;

    public bool IsMusicOn
    {
        get
        {
            return m_isMusicOn;
        }
        set
        {
            m_isMusicOn = value;
        }
    }

    public int MaxLevel
    {
        get
        {
            return m_maxLevel;
        }
    }

	public static UserData getInstance()
	{
		if (s_userData == null) 
		{
			loadUserData();
		}
		return s_userData;
	}

    private const string KEY_MAX_LEVEL = "max_level";
    private const string KEY_CUR_LEVEL = "cur_level";
    private const string KEY_SOUND_ON  = "snd_on";
    private const string KEY_MUSIC_ON  = "music_on";


	public static UserData loadUserData()
	{
		s_userData = new UserData();

        s_userData.m_maxLevel = PlayerPrefs.GetInt(KEY_MAX_LEVEL, 0);
        s_userData.m_currentLevel = PlayerPrefs.GetInt(KEY_CUR_LEVEL, 0);
        s_userData.m_isSoundOn = PlayerPrefs.GetInt(KEY_SOUND_ON, 1) > 0;
        s_userData.m_isMusicOn = PlayerPrefs.GetInt(KEY_MUSIC_ON, 1) > 0;

        return s_userData;
	}

    public void save()
    {
        PlayerPrefs.SetInt(KEY_MAX_LEVEL, m_maxLevel);
        PlayerPrefs.SetInt(KEY_CUR_LEVEL, m_currentLevel);
        PlayerPrefs.SetInt(KEY_SOUND_ON, m_isSoundOn ? 1 : 0);
        PlayerPrefs.SetInt(KEY_MUSIC_ON, m_isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public int CurrentLevel
    {
        get 
        { 
            return m_currentLevel;
        }
        set
        { 
            m_currentLevel = value;

            if (m_currentLevel >= MAX_LEVEL)
                m_currentLevel = MAX_LEVEL - 1;
            
            if (m_maxLevel < m_currentLevel)
                m_maxLevel = m_currentLevel;
        }
    }
}
