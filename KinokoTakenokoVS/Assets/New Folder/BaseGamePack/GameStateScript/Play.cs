using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play : State
{

    PlayerController player;

    Ground ground;

    GameObject kinokoHandle;

    Slider meter;

    Text DeathText;

	int goalDistance = 30;
    int currentDistance = 0;

    float coolTime = 1.0f;
    float nextTime;

    bool DeathRoot = false;

    float retryCoolTime = 3.0f;
    float retryNextTime;

	public override void StateStart ()
	{

        SoundManager.Instance.PlayBGM("bgm");

        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        player.OnHitDeleteLineEvent += PlayerReachEndLine;
        player.OnHitTakenokoEvent += PlayerDeath;

        meter = GameObject.Find("Slider").GetComponent<Slider>();

        meter.maxValue = goalDistance;

        meter.onValueChanged.AddListener(MeterOnValueChanged);

        nextTime = coolTime + Time.time;

        kinokoHandle = GameObject.Find("Handle");

        kinokoHandle.GetComponent<Animator>().SetTrigger("Shake");

        ground = GameObject.Find("Ground").GetComponent<Ground>();

        DeathText = GameObject.Find("DeathText").GetComponent<Text>();

	}

	public override void StateUpdate ()
	{

        if (DeathRoot == false)
        {
            SetMeter();
        }
        else
        {
            if (retryNextTime <= Time.time)
            {
                DeathText.text = "PushAnyKey!";
                if (Input.anyKey)
                {

                    //FadeManager.Instance.LoadLevel("GamePlay", 0.5f);
                    Application.LoadLevel("GamePlay");
                }
            }
        }

    }

    void SetMeter()
    {
        if (nextTime <= Time.time)
        {
            nextTime = coolTime + Time.time;

            currentDistance++;
        }

        meter.value = Mathf.Lerp(meter.value, currentDistance, Time.deltaTime);
    }

    void MeterOnValueChanged(float value)
    {

        if ((int)value >= goalDistance)
        {
            player.Goal = true;
            ground.Freeze();
            SoundManager.Instance.PlaySE("goal");
            SoundManager.Instance.StopBGM("bgm");
        }

    }

    void PlayerDeath()
    {
        Debug.Log("Death");
        DeathText.enabled = true;

        DeathRoot = true;

        SoundManager.Instance.PlaySE("death");
        SoundManager.Instance.StopBGM("bgm");

        retryNextTime = Time.time + retryCoolTime;
    }

    void PlayerReachEndLine()
    {
        Application.LoadLevel("ClearScene");
        //FadeManager.Instance.LoadLevel("ClearScene", 1.0f);
    }

	public override void StateDestroy ()
	{

	}

}
