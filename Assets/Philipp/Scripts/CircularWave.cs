using UnityEngine;
using System.Collections;

public class CircularWave : Wave {
	private static float waveSpeed = 20f; 
	private static float sqrtTwo = 1 / Mathf.Sqrt(2);
	
	private float centerX;
	private float centerY;
	private float energy;
	
	private float radius = 0f;
	private float innerRadius = 0f;
	private float innerSquare = 0f;
	private float outerRadius = 0f;
	
	public CircularWave(float centerX, float centerY, float energy)
	{
		this.centerX = centerX;
		this.centerY = centerY;
		this.energy = energy;
		this.radius = Mathf.PI * energy;
	}
	
	public float GetHeightInfluence(float x, float y)
	{
		float centeredX = Mathf.Abs(x - centerX);
		float centeredY = Mathf.Abs(y - centerY);
		
		
		if(centeredX > outerRadius || centeredY > outerRadius )
		{
			return 0f;
		}
		
		if(centeredX < innerSquare && centeredY < innerSquare)
		{
			return 0f;
		}
		
		float distanceCenter = Mathf.Sqrt(centeredY*centeredY+centeredX*centeredX);
		if(distanceCenter > outerRadius || distanceCenter < innerRadius)
		{
			return 0f;
		}
		
		float distanceOnRing = distanceCenter - radius;
		
		return energy * Mathf.Cos(distanceOnRing /energy) + energy;	
	}
	
	public bool Update(float deltaTime)
	{
		this.radius += waveSpeed*deltaTime; 
		
		this.innerRadius = this.radius - Mathf.PI*energy;
		this.innerSquare = this.innerRadius * sqrtTwo;
		this.outerRadius = this.radius + Mathf.PI*energy;
		
		
		if(this.radius > 50f)
		{
			// die
			return false;
		}
		
		return true;	
	}
}
