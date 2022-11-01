using UnityEngine;
using System.Collections;

public class DotObject : GameEntity {

    private const float PI = 3.14159274f;

    private const float PI2 = 1.57079637f;

    private const float PI_INV = PI * 0.5f;
        
	private float radius = 1f;
	
	public DotObject other;

    [SerializeField]
    private SpriteRenderer spriteRender;

    [SerializeField] 
    private TrailRenderer trailRenderer;

	public bool ischosen {get; set;}
	
	public float angle {get; set;}
	
//	private float lastangle = 0f;
//
//	public float LastAngle {
//		get {
//			return lastangle;
//		}
//	}
	
//	private float lastbeat;
//
//	public float LastBeat {
//		get {
//			return lastbeat;
//		}
//	}
	
	private float snappednextangle;

	public float SnappedNextAngle {
		get {
			return snappednextangle;
		}
	}
	
	private float snappedlastangle;

	public float SnappedLastAngle {
		get {
			return snappedlastangle;
		}
	}

	private bool flashed;

	private Game m_gameRef;

	private Conductor m_conductorRef;

//	public System.Action<DotObject> onMissed;
	public System.Action<DotObject> onStop;
	public System.Action<DotObject> onDamage;
	public System.Action<DotObject, FloorObject> onHit;
    public System.Action<DotObject, FloorObject> onSwitched;

    private Transform m_cacheTransf;
    private Transform m_cacheOtherTransf;
    private Vector3 m_cachePosition;

    /// <summary>
    private const int MAX_CACHE_SIZE = 120;

    private Vector3[] m_lastPositions;

    private int m_storeCounter = 0;

    private int m_currentPointer = -1;

    public bool m_isEnableReplay = true;
    /// </summary>

    private bool m_isReplay = false;

    public bool IsReplay
    {
        get
        {
            return m_isReplay;
        }
    }

    public void storePosition(Vector3 position)
    {
        m_currentPointer++;

        if (m_currentPointer >= MAX_CACHE_SIZE)
            m_currentPointer = 0;

        m_lastPositions[m_currentPointer] = position;

        m_storeCounter++;
    }

    public void replayLastPosition()
    {
        m_isReplay = true;
        StartCoroutine(_replayLastPosition());
    }

    public IEnumerator _replayLastPosition()
    {
        float delayTimeStep = 0.1f;
//        float delayTime = delayTimeStep * m_storeCounter;
      

        if (m_storeCounter > 0)
        {
            int startIndex = 0;
            if (m_storeCounter > MAX_CACHE_SIZE)
            {
                startIndex = m_currentPointer + 1;
                if (startIndex >= MAX_CACHE_SIZE)
                    startIndex = 0;
            }
            int count = Mathf.Clamp(m_storeCounter, 0, MAX_CACHE_SIZE);
            //            Debug.Log(string.Format("start {0} count {1}", startIndex, count));

            yield return new WaitForSeconds(1f);

            int cnt = 0;
            while(cnt < count)
            {
                //                Debug.Log(m_lastPositions[startIndex]);
                m_cacheTransf.position = m_lastPositions[startIndex];
                startIndex++;
                cnt++;
                if (startIndex >= MAX_CACHE_SIZE)
                    startIndex = 0;
                yield return new WaitForSeconds(delayTimeStep);
            }
        }
        yield return new WaitForSeconds(1f);
        m_isReplay = false;
    }

    public void initialize(Game game, DotObject other, bool isChoosen, bool isOther)
	{
        this.other = other;
		this.m_gameRef = game;
		this.m_conductorRef = game.m_conductor;
		this.ischosen = isChoosen;

        LevelDataMaster data = game.Data.m_data;
        spriteRender.sprite = isOther ? data.m_sprDotOther : data.m_sprDot;
        Color trailColor = isOther ? data.m_trailOtherColor : data.m_trailColor;
        trailRenderer.material.SetColor("_TintColor", trailColor);
      
	}

	private void Start()
	{
		if (this.ischosen)
		{
            this.snappedlastangle = PI2;
		}

        m_cacheTransf = transform;

        if (m_isEnableReplay)
        {
            m_lastPositions = new Vector3[MAX_CACHE_SIZE];
        }
	}
	
	private void Update()
	{
        update();
	}

    private void FixedUpdate()
    {
        update();
    }

    private void LateUpdate()
    {
        if (m_switchChoosen)
        {
            if (!m_switched)
            {
                update();
            }

            _switch();

            OnSwitched(m_hitObject);

            m_switchChoosen = false;
            m_switched = false;
            m_hitObject = null;
        }
    }

    private void update()
    {
        if (m_isReplay)
            return;
        
        if (this.ischosen)
        {
            this.angle = this.snappedlastangle + 
                (this.m_conductorRef.Position - m_conductorRef.lastHitPosition) /
                m_conductorRef.Crotchet * PI * this.m_gameRef.speed;
            this.SetOthersToAngle();
        }

        if (this.m_gameRef.failed && this.radius > 0f)
        {
            this.radius -= 0.05f;
        }

        if (this.radius < 0f && !this.flashed)
        {
            this.flashed = true;
            base.gameObject.SetActive(false);

            OnStop();
        }

        if (m_switchChoosen && !m_switched)
        {
            checkSwitch();
        }
    }


	private void OnStop()
	{
		//this.controller.FailLevel2();
		if(onStop != null)
			onStop.Invoke(this);
	}

	private void OnDamage()
	{
		//this.controller.OnDamage();
		if(onDamage != null)
			onDamage.Invoke(this);
	}

    private void OnSwitched(FloorObject obj)
    {
        if (onSwitched != null)
            onSwitched.Invoke(this, obj);
    }

	private void OnHit(FloorObject obj)
	{
        if (onHit != null)
			onHit.Invoke(this, obj);
		
		obj.OnHit(this);

	}
	
    private bool m_switchChoosen;
    private bool m_switched;
    private FloorObject m_hitObject;

    public void SwitchChosen()
	{
        m_switchChoosen = true;
        m_switched = false;
        m_hitObject = null;
	}

    private void checkSwitch()
    {
        Vector3 vector = this.SnappedCardinalDirection(SnapAngle(this.angle));

        Collider2D[] array = Physics2D.OverlapPointAll(new Vector2(vector.x, vector.y), 1 << LayerMask.NameToLayer("Floor"));

        FloorObject component = null;
        if (array.Length > 0)
        {
            Collider2D collider = null;
            for (int i = 0; i < array.Length; i++)
            {
                FloorObject com = array[i].GetComponent<FloorObject>();
                if ((com.seqID == m_gameRef.CurrentSeqID + 1))
                {
                    collider = array[i];
                    component = collider.GetComponent<FloorObject>();
                    break;
                }
                else if (com.seqID == m_gameRef.CurrentSeqID)
                {
                    OnDamage();
                    return;
                }
            }
        }

        if (component != null)
        {
            m_switched = true;
            m_hitObject = component;
        }
    }

    private void _switch()
    {
        FloorObject component = m_hitObject;

        if (component == null)
        {
            OnDamage();
            return;
        }

        component.flashycolor = true;
        component.SetToRandomSprite();
        m_gameRef.CurrentSeqID++;

        this.snappednextangle = SnapAngle(this.angle);
        this.other.snappedlastangle = this.snappednextangle - PI;
        this.m_conductorRef.ActualLastHit = this.m_conductorRef.Position;
        this.m_conductorRef.lastHitPosition += (this.snappednextangle - this.snappedlastangle) 
            / PI * this.m_conductorRef.Crotchet / this.m_gameRef.speed;

        if (component.isportal)
        {
            this.m_gameRef.won = true;
            this.m_gameRef.leveltogoto = component.levelnumber;
        }
        this.m_gameRef.speed = component.Speed;
        this.m_gameRef.m_gameCam.RotateSmooth(component.rotatecamera);

       
        this.ischosen = false;
        this.other.ischosen = true;
        this.other.SnapToCardinalDirection(this.snappednextangle);

        OnHit (component);
    }

	private void SetOthersToAngle()
	{
        if (m_cacheOtherTransf == null)
            m_cacheOtherTransf = this.other.transform;
        
        m_cachePosition = m_cacheTransf.position;
        m_cachePosition.x += Mathf.Sin(this.angle) * this.radius;
        m_cachePosition.y += Mathf.Cos(this.angle) * this.radius;
        m_cacheOtherTransf.position = m_cachePosition;

        if(this.m_isEnableReplay)
            this.other.storePosition(m_cachePosition);
	}
	
	private bool SnapToGrid()
	{
		Vector3 position = base.transform.position;
		base.transform.position = new Vector3((float)Mathf.RoundToInt(position.x), (float)Mathf.RoundToInt(position.y), 0f);
		return true;
	}
	
	private Vector3 SnappedCardinalDirection(float snappedangle)
	{
		Vector3 zero = Vector3.zero;
		float f = snappedangle / 1.57079637f;
		int num = Mathf.RoundToInt(f);
		switch (num % 4)
		{
		case 0:
			zero = new Vector3(0f, 1f, 0f);
			break;
		case 1:
			zero = new Vector3(1f, 0f, 0f);
			break;
		case 2:
			zero = new Vector3(0f, -1f, 0f);
			break;
		case 3:
			zero = new Vector3(-1f, 0f, 0f);
			break;
		}
		return base.transform.position + zero;
	}
	
	private void SnapToCardinalDirection(float snappedangle)
	{
		Vector3 zero = Vector3.zero;
		float f = snappedangle / 1.57079637f;
		int num = Mathf.RoundToInt(f);
		switch (num % 4)
		{
		case 0:
			zero = new Vector3(0f, 1f, 0f);
			break;
		case 1:
			zero = new Vector3(1f, 0f, 0f);
			break;
		case 2:
			zero = new Vector3(0f, -1f, 0f);
			break;
		case 3:
			zero = new Vector3(-1f, 0f, 0f);
			break;
		}
		base.transform.position = this.other.transform.position + zero;
	}

	public static float SnapAngle(float angle)
	{
		float f = angle / 1.57079637f;
		int num = Mathf.RoundToInt(f);
		return (float)num * 1.57079637f;
	}

}
