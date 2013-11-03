using UnityEngine;
using System.Collections;

public class PositionOnWater : MonoBehaviour {
	
	public WaterVolume waterVolume;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		this.transform.transform.position = new Vector3(pos.x, waterVolume.getHeight(pos.x,pos.z), pos.z);
	}
}
