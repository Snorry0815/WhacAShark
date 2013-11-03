using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	public float width;
	public float height;

	public float dx;
	public float dy;
	
	public WaterVolume waterVolume;
	
	public float deltaOffset;
	public Material[] materials;
	
	
	private Vector3[] vertices;
	private Vector2[] uv;
	private int[] triangles;
	
	private float offset = 0;
	private int widthCount;
	private int heightCount;
	
	private ReturnRandom returnRandom = new ReturnRandom(0.02f,0.8f);
			
	// Use this for initialization
	void Start () {
		
		this.widthCount = (int)(width / dx);
		this.heightCount = (int)(height / dy);
			
		vertices = new Vector3[widthCount * heightCount];
		uv = new Vector2[widthCount  * heightCount];
		triangles = new int[6 * widthCount * heightCount];
			
		int count = 0;
		int triCount = -1;
				
		for(int i=0; i<heightCount; ++i) {
			for(int j=0; j<widthCount; ++j) {
				
				vertices[count] = new Vector3(j*dx,0,i*dy);		
				uv[count] = new Vector2(i,j);
				
				if(j < width-1 && i < height-1) {		
					triangles[++triCount] = count;
					triangles[++triCount] = count + heightCount;
					triangles[++triCount] = count + 1;
						
					triangles[++triCount] = count + heightCount;
					triangles[++triCount] = count + heightCount + 1;
					triangles[++triCount] = count + 1;		
				}		
				++count;
			}
		}
		
		this.gameObject.AddComponent("MeshFilter");
		this.gameObject.AddComponent("MeshRenderer");
		this.gameObject.AddComponent("MeshCollider");
		
		Mesh mesh = new Mesh();
		this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
		mesh.Clear();
		mesh.vertices = vertices;
	    mesh.uv = uv;
		mesh.uv2 = uv;
	    mesh.triangles = triangles;
		mesh.RecalculateNormals();	
		
		this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
		
		this.gameObject.GetComponent<MeshRenderer>().materials = this.materials;
	}
	
	// Update is called once per frame
	void Update () {
		offset += deltaOffset;
		int count = 0;
		for(int i=0; i<heightCount; ++i) {
			for(int j=0; j<widthCount; ++j) {
				vertices[count].y = waterVolume.getHeight(j,i);
				uv[count] += returnRandom.GetNextRandom(uv[count].x-i, uv[count].y-j);
				++count;
			}
		}
		
		Mesh mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
		mesh.vertices = vertices;
	    mesh.uv = uv;
		mesh.RecalculateNormals();	
		this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

	}
}
