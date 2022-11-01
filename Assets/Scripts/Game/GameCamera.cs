using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private GameObject following;
	
	public Vector3 topos;
	
	public Vector3 frompos;
	
	private Color fromcol;
	
	private Color tocol;
	
	private float coltimer;
	
	private float coldur = 0f;
	
	private Color col;
	
	private float torot;
	
	private float fromrot;
	
	private float rot;
	
	public float timer {get; set;}

	[SerializeField]
	private float pulsemagnitude = 0.2f;

	[SerializeField]
	public float camsize = 5f;
	
	public float Camspeed {get; set;}
	
	private float tosize;
	
	private float fromsize;
	
	private float pulsetimer;
	
	private float rottimer;
	
	private float pulsedur = 0.2f;

    public ColourScheme currentColourScheme;

	private void Start()
	{
		this.topos = base.GetComponent<Camera>().transform.position;
		this.frompos = base.GetComponent<Camera>().transform.position;
		this.tocol = base.GetComponent<Camera>().backgroundColor;
		this.tosize = this.camsize;
	}
	
	private void Update()
	{
		this.timer += Time.deltaTime;
		base.GetComponent<Camera>().transform.position = Vector3.Slerp(this.frompos, this.topos, this.timer / this.Camspeed);
		this.pulsetimer += Time.deltaTime;
		base.GetComponent<Camera>().orthographicSize = Mathf.Lerp(this.fromsize, this.tosize, this.pulsetimer / this.pulsedur);
		this.rottimer += Time.deltaTime;
		this.rot = Mathf.Lerp(this.fromrot, this.torot, this.rottimer * 2f);
		base.GetComponent<Camera>().transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, this.rot);
		this.coltimer += Time.deltaTime;
		this.col = Color.Lerp(this.fromcol, this.tocol, this.coltimer / this.coldur);
		base.GetComponent<Camera>().backgroundColor = this.col;
	}
	
	public void Pulse()
	{
		this.fromsize = this.camsize - this.pulsemagnitude;
		this.tosize = this.camsize;
		this.pulsetimer = 0f;
	}
	
	public void zoomout()
	{
		this.pulsetimer = 0f;
		this.tosize = 100f;
		this.pulsedur = 10f;
	}
	
	public void RotateSmooth(float angle)
	{
		this.torot += angle;
		this.fromrot = this.rot;
		this.rottimer = 0f;
	}
	
	public void FlashBg(Color? _fromcol = null)
	{
		this.fromcol = ((!_fromcol.HasValue) ? Color.white : _fromcol.Value);
		this.tocol = currentColourScheme.colourBg;
		this.coltimer = 0f;
	}
	
	public void SetBgColour()
	{
		this.tocol = currentColourScheme.colourBg;
		this.coltimer = this.coldur;
	}
}
