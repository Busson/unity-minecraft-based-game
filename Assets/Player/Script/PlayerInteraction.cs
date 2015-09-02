using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
		//botao direito = criar bloco
		//botao esquerdo = destruir bloco

		if (Input.GetMouseButtonDown (0)) {

			Ray ray = new Ray(transform.position + transform.forward/2 ,transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,10f)){
				Vector3 position = hit.point - hit.normal/2;
				Destroy( new Vector3(Mathf.Floor(position.x),Mathf.Floor(position.y),Mathf.Ceil(position.z)),hit.collider.gameObject);
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			
			Ray ray = new Ray(transform.position + transform.forward/2,transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,10f)){
				Vector3 position = hit.point + hit.normal/2;
				Create( new Vector3(Mathf.Floor(position.x),Mathf.Floor(position.y),Mathf.Ceil(position.z)),hit.collider.gameObject);
			}
		}
	}

    void Destroy(Vector3 position, GameObject piece){
				Piece p = piece.GetComponent<Piece> ();

				p.SetBlock (Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z),0);
	}
	void Create(Vector3 position, GameObject piece){
		Piece p = piece.GetComponent<Piece> ();
		
		 p.SetBlock (Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z),1);
	}
}
