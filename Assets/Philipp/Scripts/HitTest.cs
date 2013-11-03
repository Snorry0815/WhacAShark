using UnityEngine;
using System.Collections;

public class HitTest : MonoBehaviour {
	
	public Camera cam;
	public WaterVolume waterVolume;
	
	// Use this for initialization
	public void Start () 
	{
	
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if(Input.GetMouseButtonDown(0)) 
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit)) {
				this.waterVolume.AddWave(new CircularWave(hit.point.x, hit.point.z, 1.5f));
			}
		}
	}
}
