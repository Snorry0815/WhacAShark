using UnityEngine;
using System.Collections;

public class Surface : MonoBehaviour {
	
	public float tauntTime;
	public float startWhacAble;
	public float stopWhacAble;
	
	private Animation animationInt;
	
	private int x;
	private int y;
	
	public void SetCoordinates(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	private enum AnimState
	{
		SURFACE,
		TAUNT,
		DIVE
	}
	
	private AnimState animState;
	
	// Use this for initialization
	public void Start () {
		this.animationInt = this.gameObject.GetComponentInChildren<Animation>();
		this.animationInt["attack"].speed = 1;
		this.animationInt.Play();
		this.animState = AnimState.SURFACE;
	}
	
	// Update is called once per frame
	public void Update () {
		if(!this.animationInt.isPlaying)
		{
			switch(animState)
			{
			case AnimState.SURFACE:
				this.animState = AnimState.TAUNT;
				break;
			case AnimState.TAUNT:
				tauntTime -= Time.deltaTime;
				if(tauntTime <= 0)
				{
					this.animState = AnimState.DIVE;
					this.animationInt["attack"].time = this.animationInt["attack"].length;
					this.animationInt["attack"].speed = -1f;
					this.animationInt.Play();
				}
				break;			
			case AnimState.DIVE:
			default:
				GameDirector.TriggerEscape(x,y,this.gameObject);
				Destroy(this.gameObject);
				break;
			}
		}
	}
	
	public void Whac()
	{
		if(WhacAble())
		{
			GameDirector.TriggerWhacObject(x,y,this.gameObject);
			Destroy(this.gameObject);	
		}
	}
	
	private bool WhacAble()
	{
		AnimationState attakState = this.animationInt["attack"];
		float percentDone = attakState.time / attakState.length;
		switch(animState)
		{
		case AnimState.SURFACE:
			if(percentDone > this.startWhacAble)
			{
				return true;	
			}
			break;
		case AnimState.TAUNT:
			return true;
		case AnimState.DIVE:
			if(percentDone > this.stopWhacAble)
			{
				return true;	
			}
			break;
		default:
			return false;
		}
		return false;
	}
}
