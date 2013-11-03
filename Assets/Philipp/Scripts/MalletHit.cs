using UnityEngine;
using System.Collections;

public class MalletHit : MonoBehaviour {
	
	public float malletLength;
	
	private enum AnimState
	{
		IDLE,
		MOVEDOWN,
		MOVEUP
	}
	
	private float x;
	private float y;
	
	private AnimState animState = AnimState.IDLE;
	private Animation animationInt;
	
	// Use this for initialization
	public void Start () 
	{
		GameDirector.MalletHit += Hit;
		this.animationInt = this.gameObject.GetComponentInChildren<Animation>();
	}

	public void  Hit(float x, float y)
	{
		if(animState == AnimState.IDLE)
		{
			this.gameObject.transform.position = new Vector3(x,3f,y-malletLength);
			this.animationInt["hit"].speed = 1;
			this.animationInt.Play();
			this.animState = AnimState.MOVEDOWN;
			this.x = x;
			this.y = y;
		}
	}
	
	public void Update () 
	{
		if(!this.animationInt.isPlaying)
		{
			switch(animState)
			{
			case AnimState.MOVEDOWN:
				this.animState = AnimState.MOVEUP;
				this.animationInt["hit"].speed = -1;
				this.animationInt.Play();
				Wave wave = new CircularWave(x,y,1.5f);
				GameDirector.TriggerWave(wave);
				break;
			case AnimState.MOVEUP:	
				this.animState = AnimState.IDLE;
				this.gameObject.transform.position = new Vector3(0f,0f,-15f);
				break;			
			case AnimState.IDLE:
			default:
				break;
			}
		}
	
	}
}
