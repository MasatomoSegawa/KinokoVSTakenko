using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleLogic : MonoBehaviour {

    float coolTime = 1.0f;
    float nextTime;

    void Start()
    {

		SoundManager.Instance.PlaySE("kasasu");

		nextTime = Time.time + coolTime;
    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetMouseButton(0) || Input.anyKey) && nextTime <= Time.time)
        {
			FadeManager.Instance.LoadLevel ("GamePlay", 1.0f);
        }

	}

}
