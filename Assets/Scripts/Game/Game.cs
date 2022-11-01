using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public enum State
	{
		Init 	= 1,
		Play 	= 2,
		Pause 	= 3,
		Failed  = 4,
        Won     = 5,
        Exit    = 6,
	}

	[SerializeField]
	private GameStateManager m_stateManager;

	public GameStateManager StateManager
	{
		get 
		{
			return m_stateManager;
		}
	}

	public GameData Data {
		get;
		set;
	}

	[SerializeField]
	public GameObject m_floorPrefab;

    public SpectrumVisualizer m_visualizer;

    public FloorFader m_previewController;
   
	public GameObject m_dotPrefab;
	
	public GameObject m_dotOtherPrefab;

	public DotObject Dot {get; set;}

	public DotObject DotOther {get; set;}

	public DotObject ChoosenDot {get; set;}

    private bool m_isStarted = false;

	public GameCamera m_gameCam;

    public EffectManager m_effectManager;

    //	public scrFailBar failbar;
    public SpriteRenderer m_backgroundRenderer;

    //	public scrBackgroundBars bgbars;

    public IngameUI m_ingameUI;

	[SerializeField]
	private GameWorld m_world;

	public GameWorld World
	{
		get
		{
			return m_world;
		}
	}

    public bool IsStarted
    {
        get
        {
            return m_isStarted;
        }
    }

    public Conductor m_conductor;

	private void Awake()
	{
		m_stateManager.Configure (gameObject);

		m_stateManager.AddState<GameStateInit>(State.Init, new GameStateInit());
		m_stateManager.AddState<GameStatePlay>(State.Play, new GameStatePlay());
		m_stateManager.AddState<GameStatePause>(State.Pause, new GameStatePause());
		m_stateManager.AddState<GameStateFailed>(State.Failed, new GameStateFailed());
        m_stateManager.AddState<GameStateWon>(State.Won, new GameStateWon());
		m_stateManager.ChangeState (State.Init);
	}

	// Use this for initialization
	void Start () {
        m_isStarted = true;
	}

    void Update()
    {
//        m_ingameUI.setManaValue(m_mana);
    }

	[HideInInspector] public bool failed;
	
	[HideInInspector] public bool isalive;
	
	[HideInInspector] public bool won;

	[HideInInspector] public float speed = 1f;
	
	[HideInInspector] public bool gameworld;

	[HideInInspector] public int leveltogoto = -1;
	
	[HideInInspector] public bool warning;
	
	[HideInInspector] public bool clearwarning;
	
	[HideInInspector] public bool isgameworld;
	
	[HideInInspector] public float pitchadd;
	
	[HideInInspector] public bool firsttime = true;
	
	[HideInInspector] public int colourscheme = -1;
	
	[HideInInspector] public int d_startlevel;
	
	[HideInInspector] public bool d_tiling;
	
    public bool Responsive {get; set;}
	
    public bool CountDowned { get; set;}
	
    public int CurrentSeqID { get; set;}

    public float Percent
    {
        get;
        set;
    }

    public void initialize(GameData data)
    {
        this.Data = data;

        Dot.initialize(this, DotOther, false, false);
        DotOther.initialize(this, Dot, false, true);
        ChoosenDot = Dot;

        // Setup conductor
        m_conductor.initialize(Data);

        if (!UserData.getInstance().IsMusicOn)
            m_conductor.mute();

        // Setup camera
        m_gameCam.Camspeed = m_conductor.Crotchet * 2f;

        Responsive = true;
        CountDowned = false;
        CurrentSeqID = 0;
        isgameworld = true;

        Mana = 1f;

        m_ingameUI.setTitleText(Data.m_clip.caption);
        m_ingameUI.setPercentCompleteText(0);
        m_ingameUI.setActiveReplayPopup(false);
        m_ingameUI.setActiveWonPopup(false);
//        m_ingameUI.setManaValue(1f);

        //background
        m_backgroundRenderer.sprite = Data.m_data.m_spriteBackground;

        if (Data.m_data.m_Preview)
        {
            m_previewController.initialize(this);
        }
        else
        {
            m_previewController.gameObject.SetActive(false);
        }

        m_visualizer.Multiply = 1f / m_conductor.song.volume;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

    public void Quit()
    {
        Bootstrap.StateMachine.ChangeState(Bootstrap.State.Home);
    }

    public void nextLevel()
    {
        // save score
        UserData userData = UserData.getInstance();
        userData.CurrentLevel += 1;
        userData.save();

        Restart();
    }
  
    private float m_mana;

    public float Mana {
        get { 
            return m_mana; 
        } 
        set { 
            m_mana = Mathf.Clamp01(value);
        }
    }

    public void takeDamage(float val)
    {
        Mana -= val;
        if (Mana < 0)
            Mana = 0f;
    }

}
