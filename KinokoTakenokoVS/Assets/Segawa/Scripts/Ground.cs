using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{

	public GameObject SpawnLineA;
	public GameObject SpawnLineB;
	public GameObject Takenoko;
	public GameObject TakenokoBox;
	private float NextTime;
	public float SpawnRateTime = 3.0f;
	public float scrollSpeed = 0.25f;
	private bool FreezeFlag = false;
	public bool GameEndFlag = false;

	// Use this for initialization
	void Awake ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (FreezeFlag == false) {

			//たけのこスポーン調整
			if (NextTime <= Time.time) {
				NextTime = SpawnRateTime + Time.time;
	
				GameObject takenoko = Instantiate (Takenoko)as GameObject;
				takenoko.transform.parent = TakenokoBox.transform;

				Vector3 position = SpawnLineA.transform.position;
				position.x = Random.Range (SpawnLineA.transform.position.x, SpawnLineB.transform.position.x);
				position.y = takenoko.transform.position.y;

				takenoko.transform.position = position;

			}

		}

	}

	public void DeleteTakenoko ()
	{
		Destroy (TakenokoBox);
	}

	void GameEnd ()
	{
		GameEndFlag = true;
	}

	void FixedUpdate ()
	{

		if (FreezeFlag == false) {

			float offset = Time.time * scrollSpeed;

			this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, -offset);

		}
	}

	public void Freeze ()
	{
		FreezeFlag = true;
	}

	public void Reset ()
	{
		FreezeFlag = false;
	}

}
