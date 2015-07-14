using UnityEngine;
using System.Collections;

public class Kinoko : MonoBehaviour {

    // ゴールについたかどうか.
    bool isGoalReach = false;

    // ゴールしたかどうか.
    public bool _isGoal;
    public bool isGoal{

        set
        {
            _isGoal = value;
            if (_isGoal == true)
            {
                this.StartGoalMotion();
            }
        }

    }

    // 移動スピード.
    public float speed;

    // ゴールへの移動スピード.
    public float goalMoveSpeed;

    public float coolTime;
    private float nextTime;

    // 可動範囲.
    public Transform LineA;
    public Transform LineB;

    // きのこが死んだ時のイベント.
    public delegate void DeathDelegate();
    public event DeathDelegate DeathEvent;

    // きのこのゴール用モーションイベント.
    public delegate void EndGoalMotionDelegate();
    public event EndGoalMotionDelegate EndGoalMotionEvent;
	
	void Update () {

        if (_isGoal == false)
        {
            Move();
        }
        else
        {
            MoveGoal();
        }

	}

    /// <summary>
    /// きのこが死んだ時の処理.
    /// </summary>
    void Death()
    {
        Debug.Log("HitTakenoko");

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 500.0f);

        if (DeathEvent != null)
        {
            DeathEvent();
        }

    }

    void Move()
    {

        // キーボード入力を取得.
        float xAxis = Input.GetAxis("Horizontal");

        // 右向きを取得.
        Vector3 right = this.transform.right;

        // 移動.
        this.transform.position += right * xAxis * speed;

        // Clamp処理.
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, LineA.position.x, LineB.position.x);
        this.transform.position = pos;

    }

    /// <summary>
    /// ゴール演出用のメソッド.
    /// </summary>
    void EndGoalMotion()
    {
        isGoalReach = true;
        EndGoalMotionEvent();
    }

    /// <summary>
    /// ゴール演出用のメソッド.
    /// </summary>
    void StartGoalMotion()
    {

        // タイマーをセット.
        nextTime = Time.time + coolTime;


    }

    /// <summary>
    /// ゴール演出用のMove関数.
    /// </summary>
    void MoveGoal()
    {

        Vector3 direction = this.transform.forward;

        this.transform.position += direction * goalMoveSpeed;

        if (nextTime <= Time.time && isGoalReach == false)
        {

            if (EndGoalMotionEvent != null)
            {
                EndGoalMotion();
            }

        }

    }

    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {

            case "Takenoko":
                if (_isGoal == false)
                {
                    Death();
                }
                else
                {
                    other.GetComponent<Takenoko>().Death();
                }
                break;

        }

    }

}
