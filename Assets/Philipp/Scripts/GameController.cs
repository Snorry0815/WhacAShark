using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int width;
	public int height;
	
	public int squareWidth;
	public int borderWidth;
	
	public float spawnTime;
	
	
		
	public GameObject shark;
	
	private int squareReturnOffest;
	private int scaledBorderWidth;
	
	private GameObject[][] animals;
	private float lastUpdate = 0;
	
	public void Start () {
		GameDirector.WaterHit += OnWaterHit;
		GameDirector.Escape += OnEscape;
		GameDirector.Whac += OnWhac;
		GameDirector.WhacObject += OnWhacObject;
		
		this.squareReturnOffest = this.squareWidth/2 + this.borderWidth;
		this.scaledBorderWidth = this.borderWidth / this.squareWidth;
		
		this.animals = new GameObject[this.width][];
		for(int i=0;i<this.width;++i)
		{
			this.animals[i] = new GameObject[this.height];
		}
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
		
		float incrementX = GetRescaled(hitX);
		float incrementY = GetRescaled(hitY);
		
		GameDirector.TriggerMalletHit(incrementX,incrementY, hitX, hitY);
	}
	
	// Update is called once per frame
	public void Update () {
		GameLoop(Time.deltaTime);	
	}
	
	private void GameLoop(float dt)
	{
		lastUpdate += dt;
		if(lastUpdate > spawnTime)
		{
			SpawnShark();
			lastUpdate -= spawnTime;
		}
	}
	
	private float GetRescaled(float v)
	{
		return v * this.squareWidth + this.squareReturnOffest;
	}
	
	private void SpawnShark()
	{
		int x = (int) (Random.value * width);
		int y = (int) (Random.value * height);
		
		if(this.animals[x][y] == null)
		{
			float fx = GetRescaled(x);
			float fy = GetRescaled(y);
			
			Vector3 position = new Vector3(fx,shark.transform.position.y,fy);
			GameObject gO = (GameObject)Instantiate(this.shark,position,Quaternion.identity);
			gO.transform.rotation = shark.transform.rotation;
			gO.GetComponent<Surface>().SetCoordinates(x,y);
			this.animals[x][y] = gO;
		}
	}
	
	public void OnEscape(int x, int y, GameObject gO)
	{
		this.animals[x][y] = null;
	}
	
	public void OnWhac(int x, int y)
	{
		if(this.animals[x][y] != null)
		{
			this.animals[x][y].GetComponent<Surface>().Whac();
		}
	}
	
	public void OnWhacObject(int x, int y, GameObject gO)
	{
		this.animals[x][y] = null;
	}
}
