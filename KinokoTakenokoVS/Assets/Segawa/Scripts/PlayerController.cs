using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool Freez = false;

    public Transform PointA;
    public Transform PointB;

    private float pointAX;
    private float pointBX;

    public bool Goal = false;

    public delegate void OnHitTakenko();
    public event OnHitTakenko OnHitTakenokoEvent;

    public delegate void OnHitDeleteLine();
    public event OnHitDeleteLine OnHitDeleteLineEvent;

    void Start()
    {
        pointAX = PointA.position.x;
        pointBX = PointB.position.x;
    }

	// Update is called once per frame
	void Update () {

        if (Freez == false && Goal == false) {
            Move();        
        }else if (Goal){
            GoalMove();
        }

	}

    void GoalMove()
    {

        Vector3 moveDirection = this.transform.forward;
        float speed = 0.1f;

        Vector3 prePosition = this.transform.position + moveDirection * speed;

        this.transform.position = prePosition;

    }

    /// <summary>
    /// 移動する.
    /// </summary>
    void Move()
    {

        float xAxis = Input.GetAxis("Horizontal");

        Vector3 moveDirection = this.transform.right * xAxis;
        float speed = 0.1f;

        Vector3 prePosition = this.transform.position + moveDirection * speed;

        if (pointAX <= prePosition.x && prePosition.x <= pointBX)
        {
            this.transform.position += moveDirection * speed;
        }

    }

    void Death()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 500.0f);
    }

    void OnTriggerEnter(Collider other)
    {

        switch(other.tag){

            case "Takenoko":
                if (this.Goal == false)
                {
                    Debug.Log("Hit!");
                    Death();
                    if (OnHitTakenokoEvent != null)
                    {
                        OnHitTakenokoEvent();
                    }
                }
                break;

            case "DeleteLine":
                Debug.Log("DeleteLine");

                if (OnHitDeleteLineEvent != null)
                {
                    OnHitDeleteLineEvent();
                }
                break;
        }

    }


}
