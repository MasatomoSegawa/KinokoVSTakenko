using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Initailizer : State
{

    Ground ground;

    Text StartText;

    PlayerController player;

    float nextTime = 0.0f;
    float coolTime = 4.0f;

	public override void StateStart ()
	{

        this.player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        player.Freez = true;

        nextTime = coolTime + Time.time;

        StartText = GameObject.Find("StartText").GetComponent<Text>();

        ground = GameObject.Find("Ground").GetComponent<Ground>();

        ground.Freeze();

	}

	public override void StateUpdate ()
	{


           if (Input.anyKey)
            {
                this.isEnd = true;
            }

	}

	public override void StateDestroy ()
	{

        player.Freez = false;

        if (StartText != null)
        {

            StartText.enabled = false;
        }

        ground.Reset();

	}

}
