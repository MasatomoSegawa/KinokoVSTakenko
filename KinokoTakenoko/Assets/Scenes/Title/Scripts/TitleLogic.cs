using UnityEngine;
using System.Collections;

public class TitleLogic : MonoBehaviour {

    public Animator TextAnimator;
    
    float nextTime;
    float coolTime;
    bool onceFlag = true;

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown && onceFlag == true)
        {
            float coolTime = SoundManager.Instance.PlaySE(0);
            TextAnimator.SetTrigger("Wait");
            nextTime = coolTime + Time.time;
            onceFlag = false;
        }

        if (nextTime <= Time.time && onceFlag == false)
        {
            FadeManager.Instance.LoadLevel("GamePlay", 0.2f);
        }


	}

}
