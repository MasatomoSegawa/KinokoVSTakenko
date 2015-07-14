using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GaugeSlider : MonoBehaviour {

    public delegate void ReachGoalDelegate();
    public event ReachGoalDelegate ReachGoalEvent;

    public float GoalDistance = 100;
    private float currentDistance = 0;

    Slider mySlider;

    float nextTime;
    float coolTime = 1.0f;

    bool isReachGoal = false;

	// Use this for initialization
	void Start () {

        mySlider = this.GetComponent<Slider>();

        mySlider.maxValue = this.GoalDistance;

        // ゲージのタイムスパンを設定.
        nextTime = Time.time + coolTime;

	}
	
	// Update is called once per frame
	void Update () {

        // ゲージが１メモリ進む.
        if (nextTime <= Time.time && isReachGoal == false)
        {
            nextTime = Time.time + coolTime;

            currentDistance++;

            mySlider.value = currentDistance / GoalDistance;
        }

	}

    public void ChangeValue(float value)
    {

        if (value >= mySlider.maxValue)
        {
            ReachGoal();
        }

    }

    /// <summary>
    /// ゴールに到達した時の処理.
    /// </summary>
    void ReachGoal()
    {

        this.isReachGoal = true;

        if (ReachGoalEvent != null)
        {
            ReachGoalEvent();
        }

    }
}
