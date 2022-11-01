//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class FloorObject: GameEntity
{
    [SerializeField]
    private Renderer m_mainRenderer;

//	private static bool isDebug = true;

	public SpriteRenderer floorsprite;
	
	public SpriteRenderer iconsprite;
	
	public Sprite sprStraight;
	
	public Sprite sprBend;
	
	public Sprite sprPortal;
	
	public Sprite sprIconFast;
	
	public Sprite sprIconSlow;
	
	public Sprite sprIconCamera;
	
	public Sprite sprIconPortal;
	
//	public Color[] arrcolors;
    public SpriteData spriteData;
	
	public bool flashycolor;
	
	public bool isportal;
	
	public int gotoworldnumber = 999;
	
	public int levelnumber = 999;
	
	public int colourschemechange = 999;
	
    public bool IsSpecial { get; set;}
	
	public int sfxnum = -1;
	
	public float Speed = 1f;
	
	public float rotatecamera;
	
	public int seqID;

	private char _dir;

    public int spriteIndex = -1;
	
	public char direction {
		get {
			return _dir;
		}
		set {
			_dir = value;
		}
	}
	
	public string tilecode;
	
	private void Start()
	{
	}
	
	public void SetToPortal(int level = -1, bool isendofworld = false)
	{
        this.isportal = true;
        this.levelnumber = level;
	}
	
	private void Update()
	{
		if (this.isportal)
		{
			this.iconsprite.sprite = this.sprIconPortal;
			this.iconsprite.gameObject.transform.Rotate(Vector3.back, 0.5f);
		}
	}
	
	private void OnBeat()
	{
		if (this.flashycolor)
		{
			this.SetToRandomSprite();
		}
	}
	
	public void SetToRandomSprite()
	{
        SetSprite(UnityEngine.Random.Range(0, spriteData.sprites.Length));
	}

    public void SetSprite(int i)
    {
        Sprite[] sprites = spriteData.sprites;
        if (sprites.Length > i)
        {
            this.floorsprite.sprite = sprites[i];
            spriteIndex = i;
        }
    }

    public int getSpriteIndex()
    {
        return spriteIndex;
    }
	
	public void SetSpriteFromChar(char charafter)
	{
		string a = new string(new char[]
		                      {
			this.direction,
			charafter
		});
		Sprite sprite = this.sprStraight;
		float rotation = 0f;
		this.tilecode = a;
		if (a == "LL" || a == "RR" || a == "LE" || a == "RE")
		{
			sprite = this.sprStraight;
		}
		if (a == "UU" || a == "DD" || a == "UE" || a == "DE")
		{
			sprite = this.sprStraight;
			rotation = 90f;
		}
		if (a == "UR" || a == "LD")
		{
			sprite = this.sprBend;
		}
		if (a == "RD" || a == "UL")
		{
			sprite = this.sprBend;
			rotation = 270f;
		}
		if (a == "DL" || a == "RU")
		{
			sprite = this.sprBend;
			rotation = 180f;
		}
		if (a == "LU" || a == "DR")
		{
			sprite = this.sprBend;
			rotation = 90f;
		}
		this.floorsprite.sprite = sprite;
        Utils.Rotate2D(this.floorsprite.transform, rotation);
	}

	public void OnHit(DotObject dot)
	{
		SetToRandomSprite();
		flashycolor = true;
	}

}
