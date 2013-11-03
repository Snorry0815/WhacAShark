using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int width;
	public int height;
	
	public int squareWidth;
	public int borderWidth;
	
	private int squareReturnOffest;
	private int scaledBorderWidth;
	
	public void Start () {
		GameDirector.WaterHit += OnWaterHit;
		squareReturnOffest = squareWidth/2 + borderWidth;
		scaledBorderWidth = borderWidth / squareWidth;
	}
	
	public void OnWaterHit(float x, float y)
	{
		
		int hitX = (int)(x / squareWidth) - scaledBorderWidth;
		int hitY = (int)(y / squareWidth) - scaledBorderWidth;
		
		if(hitX < 0 || hitX >= width)
		{
			// outside x
			return;
		}
			
		if(hitY < 0 || hitY >= height)
		{
			// outside y
			return;
		}
		
		float incrementX = hitX * squareWidth + squareReturnOffest;
		float incrementY = hitY * squareWidth + squareReturnOffest;
		
		GameDirector.TriggerMalletHit(incrementX,incrementY);
	}
	
	// Update is called once per frame
	public void Update () {
	
	}
}
