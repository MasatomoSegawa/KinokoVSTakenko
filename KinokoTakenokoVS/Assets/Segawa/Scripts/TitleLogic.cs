using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleLogic : MonoBehaviour {

    public bool endFlag = false;

    float coolTime = 1.0f;
    float nextTime;

    void Start()
    {


    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetMouseButton(0) || Input.anyKey) && endFlag == true && nextTime <= Time.time)
        {
            Application.LoadLevel("GamePlay");
        }

        if ((Input.GetMouseButton(0) || Input.anyKey) && endFlag == false)
        {
            this.GetComponent<Animator>().SetTrigger("Skip");
            SoundManager.Instance.PlaySE("kasasu");
            nextTime = Time.time + coolTime;
        }

	}

}
