using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour {

	private int size = 30;
	private byte[,,]content;

	//0 nada
	//1 bloco

	protected Mesh mesh;
	protected MeshCollider meshCollider;
	protected List<Vector3> vertices = new List<Vector3>();
	protected List<int> triangles = new List<int>();
	protected List<Vector2> uvs = new List<Vector2>();

	// Use this for initialization
	void Start () {
		content = new byte[size,size,size];
		for (int x=0; x<size; x++) {
			for (int z=0; z<size; z++) {
				content[x,0,z] = 2;
				content[x,1,z]= (byte)Mathf.RoundToInt(Random.value);
			}
		}

	
	    
		Generate ();
	}
	private void GenerateBlock(int x, int y, int z, byte block){

		Vector3 start = new Vector3 (x, y, z);
		Vector3 dim1, dim2;

		if (ItsTransparent (x, y, z - 1)) {
			dim1 = Vector3.left;
			dim2 = Vector3.up;

			GenerateFace(start+Vector3.right+Vector3.back, dim1,dim2,block);

		}
		if (ItsTransparent (x, y, z + 1)) {
			dim1 = Vector3.right;
			dim2 = Vector3.up;
			
			GenerateFace(start, dim1,dim2,block);
			
		}

		if (ItsTransparent (x-1, y, z)) {
			dim1 = Vector3.up;
			dim2 = Vector3.back;
			
			GenerateFace(start, dim1,dim2,block);
			
		}

		if (ItsTransparent (x+1, y, z)) {
			dim1 = Vector3.down;
			dim2 = Vector3.back;
			
			GenerateFace(start+Vector3.right + Vector3.up, dim1,dim2,block);
			
		}

		if (ItsTransparent (x, y-1, z)) {
			dim1 = Vector3.left;
			dim2 = Vector3.back;
			
			GenerateFace(start+Vector3.right, dim1,dim2,block);
			
		}

		if (ItsTransparent (x, y+1, z)) {
			dim1 = Vector3.right;
			dim2 = Vector3.back;
			
			GenerateFace(start+Vector3.up, dim1,dim2,block);
			
		}


	}
	private void  GenerateFace(Vector3 start, Vector3 dim1, Vector3 dim2, byte block){

		int cont = vertices.Count;

		vertices.Add (start);
		vertices.Add (start+dim1);
		vertices.Add (start+dim2);
		vertices.Add (start+dim1+dim2);


		Vector2 uvBase = new Vector2 (0f, 0.5f);
        
		if(block ==2)uvBase = new Vector2 (0f, 0f);
		else if(block ==3)uvBase = new Vector2 (0.5f, 0.5f);
		else if(block ==4)uvBase = new Vector2 (0.5f, 0f);


		uvs.Add (uvBase);
		uvs.Add (uvBase + new Vector2(0f,0.5f));
		uvs.Add (uvBase + new Vector2(0.5f,0f));
		uvs.Add (uvBase + new Vector2(0.5f,0.5f));


		triangles.Add (cont);
		triangles.Add (cont+1);
		triangles.Add (cont+2);
		triangles.Add (cont+3);
		triangles.Add (cont+2);
		triangles.Add (cont+1);

	}
	private bool ItsTransparent(int x, int y, int z){
		if ((x < 0) || (y < 0) || (z < 0) || (x >= size) || (y >= size) || (z >= size))
			return true;

		return content [x, y, z] == 0;
	}
	// Update is called once per frame
	private void Generate(){

		mesh = new Mesh();
		GetComponent<MeshFilter> ().mesh = mesh;
		meshCollider = GetComponent<MeshCollider> ();

		vertices.Clear ();
		triangles.Clear ();
		uvs.Clear ();

		mesh.triangles = triangles.ToArray ();

		for (int x=0; x<size; x++) {
			for (int y=0; y<size; y++) {
				for (int z=0; z<size; z++) {
					 
					byte block = content[x,y,z];

					if(block==0)continue;
					else GenerateBlock(x,y,z,block);

				}
			}
		}

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.uv = uvs.ToArray ();

		mesh.RecalculateNormals ();

		meshCollider.sharedMesh.Clear ();
		meshCollider.sharedMesh = mesh;

	}

	public void SetBlock(int x, int y, int z, byte block){
		if ((x < 0) || (y < 0) || (z < 0) || (x >= size) || (y >= size) || (z >= size))
			return ;

		Debug.Log (x+" "+y+"  "+z);

		if (content [x, y, z] != block) {
			content [x, y, z] = block;
			Generate();
		}
	}
}
