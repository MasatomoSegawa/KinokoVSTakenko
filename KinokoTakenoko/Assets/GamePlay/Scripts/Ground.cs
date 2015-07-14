using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{

    // Spawnの範囲.
	public Transform SpawnLineA;
    public Transform SpawnLineB;

    // たけのこ.
	public GameObject Takenoko;

    // たけのこのスポーン地点.
	public GameObject TakenokoSpawnPoint;

    // たけのこのスポーンクールタイム.
	private float NextTime;
	public float SpawnRateTime = 3.0f;

    // 地面のスクロールスピード.
	public float scrollSpeed = 0.25f;

    private bool _FreezeFlag;
    public bool FreezeFlag
    {
        get 
        {
            return _FreezeFlag;
        }

        set
        {

            _FreezeFlag = value;
            if (_FreezeFlag == true)
            {
                AllTakenokoFreeze();
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
	{

        if (_FreezeFlag == false)
        {

			//たけのこスポーン調整
			if (NextTime <= Time.time) {
				NextTime = SpawnRateTime + Time.time;
	
				GameObject takenoko = Instantiate (Takenoko)as GameObject;
				takenoko.transform.parent = TakenokoSpawnPoint.transform;

				Vector3 position = SpawnLineA.transform.position;
				position.x = Random.Range (SpawnLineA.position.x, SpawnLineB.position.x);
				position.y = takenoko.transform.position.y;

				takenoko.transform.position = position;

			}

		}

	}

    /// <summary>
    /// 全たけのこの処理を止める.
    /// </summary>
    void AllTakenokoFreeze()
    {

        foreach (Takenoko takenoko in TakenokoSpawnPoint.GetComponentsInChildren<Takenoko>())
        {

            takenoko.isFreeze = true;

        }

    }

	void FixedUpdate ()
	{

        if (_FreezeFlag == false)
        {

			float offset = Time.time * scrollSpeed;

			this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, -offset);

		}
	}

}
