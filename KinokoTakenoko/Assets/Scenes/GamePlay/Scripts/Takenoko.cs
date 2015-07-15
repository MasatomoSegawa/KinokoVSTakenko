using UnityEngine;
using System.Collections;

public class Takenoko : MonoBehaviour {

    public float speed;

    public bool isFreeze = false;

	// Update is called once per frame
	void Update () {

        if (isFreeze == false)
        {
            this.transform.position += this.transform.forward * speed;
        }

	}

    public void Death()
    {
        if (this.gameObject.GetComponent<Rigidbody>() == false) { 
            this.gameObject.AddComponent<Rigidbody>();
        }

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 500.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DeleteTakenokoLine")
        {
            Destroy(this.gameObject);
        }
    }

}
