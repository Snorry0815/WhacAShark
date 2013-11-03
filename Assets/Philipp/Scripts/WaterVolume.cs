using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterVolume : MonoBehaviour {
	public int width;
	public int height;
	
	private float[,] heightMap;
	
	public float deltaOffset= 0.4f;
	private float offset = 0f;
	
	private List<Wave> waves = new List<Wave>();
	
	// Use this for initialization
	void Start () {
		heightMap = new float[width,height];
		for(int x = 0; x < width;++x)
		{
			for(int y=0;y<height;++y)
			{
				heightMap[x,y] = 1f;
			}
		}
		
		GameDirector.Wave += AddWave;
	}
	
	public float getHeight(int x, int y)
	{
		return heightMap[x,y];
	}
	
	public float getHeight(float x, float y)
	{
		int x1 = (int)x;
		int y1 = (int)y;
		
		if(x1 == x && y == y1)
		{
			return heightMap[x1,y1];
		}
		
		float h1 = heightMap[x1,y1];
		float h2 = heightMap[x1+1,y1];
		float h3 = heightMap[x1,y1+1];
		float h4 = heightMap[x1+1,y1+1];		
		
		float dx = x - x1;
		float dy = y - y1;
		float h12 = (1f - dx) * h1 + dx * h2;
		float h34 = (1f - dx) * h3 + dx * h4;
		
		float h1234 = (1f - dy) * h12 + dy * h34;
		
		return h1234;
	}

	public void AddWave(Wave wave)
	{
		this.waves.Add(wave);
	}
	
	// Update is called once per frame
	public void Update () {
		this.offset += this.deltaOffset;
		
		List<Wave> removeFromList = new List<Wave>();
		
		foreach(Wave wave in this.waves)
		{
			if(!wave.Update(Time.deltaTime))
			{
				removeFromList.Add(wave);
			}
		}
		foreach(Wave wave in removeFromList)
		{
			waves.Remove(wave);
		}
		
		for(int x = 0; x < this.width;++x)
		{
			for(int y=0;y<this.height;++y)
			{
				// heightMap[x,y] = Mathf.Sin((x+offset)*0.2f)*Mathf.Sin((y)*0.2f)*3 + testWave.getHeightInfluence(x,y);
				heightMap[x,y] = 0;
				foreach(Wave wave in waves)
				{
					heightMap[x,y] += wave.GetHeightInfluence(x,y);
				}
			}
		}
	}
}
