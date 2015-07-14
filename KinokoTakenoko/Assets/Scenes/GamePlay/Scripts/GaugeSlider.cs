using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GaugeSlider : MonoBehaviour {

    // ゴールした時のイベント.
    public delegate void ReachGoalDelegate();
    public event ReachGoalDelegate ReachGoalEvent;

    // Goalまでの距離.
    public int GoalDistance = 100;
    private int currentDistance = 0;

    Slider mySlider;

    float nextTime;
    float coolTime = 1.0f;

    bool isReachGoal = false;

    public bool FreezeFlag = false;

	// Use this for initialization
	void Start () {

        mySlider = this.GetComponent<Slider>();

        mySlider.maxValue = this.GoalDistance;

        // ゲージのタイムスパンを設定.
        nextTime = Time.time + coolTime;

	}
	
	// Update is called once per frame
	void Update () {

        if (FreezeFlag == true)
        {
            return;
        }

        // ゲージが１メモリ進む.
        if (nextTime <= Time.time && isReachGoal == false)
        {
            nextTime = Time.time + coolTime;

            currentDistance++;
        }

        mySlider.value = Mathf.Lerp(mySlider.value, currentDistance, Time.deltaTime);

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
