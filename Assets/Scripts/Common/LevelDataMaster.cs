using UnityEngine;

public class LevelDataMaster: ScriptableObject
{
	public int level;
	public string clipName;
    public SpriteData spriteData;
	public int world;

    public Sprite m_spriteBackground;
    public Sprite m_sprDot;
    public Sprite m_sprDotOther;

    public Color m_trailColor;
    public Color m_trailOtherColor;

    public bool m_Preview = true;
    public int m_maxPreviewBar = 4;
}

