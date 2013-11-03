using UnityEngine;
using System.Collections;

public class HitTest : MonoBehaviour {
	public Camera cam;

	public void Start () 
	{ }

	public void Update () 
	{
		if(Input.GetMouseButtonDown(0)) 
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit)) {
				GameDirector.TriggerWaterHit(hit.point.x, hit.point.z);
			}
		}
	}
}
