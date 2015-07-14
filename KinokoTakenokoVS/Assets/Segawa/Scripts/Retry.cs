using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SoundManager.Instance.PlayBGM("ending");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.anyKey){
			Application.LoadLevel("Title");
            SoundManager.Instance.StopBGM("ending");
		}

	}
}
