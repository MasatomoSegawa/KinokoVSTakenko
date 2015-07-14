using UnityEngine;
using System.Collections;

public class Takenoko : MonoBehaviour {

	private Vector3 vector = -Vector3.forward;

	public float speed;

	public bool FreezeFlag = false;

	bool powerFlag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(FreezeFlag == false)
			this.GetComponent<Rigidbody>().velocity = vector * speed;

	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "DeleteLine"){
			Destroy(this.gameObject);
		}

	}

	void Freeze(){
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.FreezeFlag = true;
	}

}
