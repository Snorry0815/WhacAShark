using UnityEngine;
using System.Collections;

public class ReturnRandom {
	
	float maxSingleTimeChange;
	float maxChange;
	
	public ReturnRandom(float maxSingleTimeChange, float maxChange)
	{
		this.maxSingleTimeChange = maxSingleTimeChange;
		this.maxChange = maxChange;
	}
	
	public Vector2 GetNextRandomSimple(float errorX, float errorY)
	{
		float nextX = GetNextRandomSimple(errorX);
		float nextY = GetNextRandomSimple(errorY);
		return new Vector2(nextX,nextY);
	}
	
	float GetNextRandomSimple(float errorX)
	{
		float next = 2 * (Random.value - 0.5f) * maxSingleTimeChange;
		if(Mathf.Abs(next+errorX) > maxChange)
		{
			next = -next;
		}
		return next;
	}
	
	public Vector2 GetNextRandom(float errorX, float errorY)
	{
		float nextX = GetNextRandomValue(Random.value,errorX);
		float nextY = GetNextRandomValue(Random.value,errorY);
		
		return new Vector2(nextX,nextY);
	}
	
	private float GetNextRandomValue(float v, float error)
	{
		float vStroke = v - (maxChange + error) / (2 * maxChange);
		if(v > 0)
		{
			if(error >= 0)
			{
				return GetNextRandomValueEnd(vStroke);
			}
			else
			{
				return GetNextRandomValueEnd(vStroke,error);
			}
		}
		else
		{
			if(error <= 0)
			{
				return GetNextRandomValueEnd(vStroke);
			}
			else
			{
				return GetNextRandomValueEnd(vStroke,error);
			}
		}
	}
	
	private float GetNextRandomValueEnd(float v)
	{
		return 2 * maxSingleTimeChange * v;	
	}
	
	private float GetNextRandomValueEnd(float v,float error)
	{
		return v * (2 * maxSingleTimeChange * maxChange) / (maxChange + error);	
	}
	
}
