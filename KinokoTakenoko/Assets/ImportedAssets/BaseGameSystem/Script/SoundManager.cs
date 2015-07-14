using UnityEngine;
using System.Collections;

/// <summary>
/// Volume設定用の構造体
/// </summary>
[System.Serializable]
public class SoundVolume
{
	public float BGM = 1.0f;
	public float Voice = 1.0f;
	public float SE = 1.0f;
	public bool BGM_Mute = false;
	public bool SE_Mute = false;

	public void Init ()
	{
		BGM = 1.0f;
		Voice = 1.0f;
		SE = 1.0f;
		BGM_Mute = false;
		SE_Mute = false;
	}
}

[System.Serializable]
public class SoundSource
{
	public AudioClip clip;

	[HideInInspector]
	public bool FadeFlag = false;
	public    float m_VolumeBefore = 0.0f;
	// フェード開始前の音量.
	public    float m_VolumeAfter = 0.0f;
	// フェード終了後の音量.
	public    float m_FadeTime = 0.0f;
	// フェード時間.

	public float m_Volume;
}

public class myAudioSource{
	public AudioSource source;
	public int ID;
	public string name;
}

/// <summary>
/// 音源を扱うシングルトン.
/// どこからでも参照出来るのでプロパティを通してインスタンスを得られる.
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
	//BGM再生機
	private AudioSource BGMSource;

	// SE再生機
	private myAudioSource[] SESources;

	//音量
	public SoundVolume volume = new SoundVolume ();

	//BGM音源
	public SoundSource[] BGMs;

	// SE音源
	public SoundSource[] SEs;

	void Awake ()
	{

		BGMSource = gameObject.AddComponent<AudioSource> ();
		BGMSource.loop = true;

		this.SESources = new myAudioSource[10];
		for (int i = 0; i < SEs.Length; i++) {
			myAudioSource seSources = new myAudioSource ();
			this.SESources [i] = seSources;
			SESources [i].source = gameObject.AddComponent<AudioSource> ();
		}


	}

	void Update ()
	{

		UpdateFadeOutBGM ();
		UpdateFadeOutSE ();
	
	}

	/// <summary>
	/// フラグがたったSEをフェードアウトさせる.
	/// </summary>
	void UpdateFadeOutSE(){

		for (var i = 0; i < SEs.Length; i++) {
			if (SEs[i].FadeFlag == true && SEs[i].m_FadeTime >= 0.0f) {

				SEs[i].m_Volume += (SEs[i].m_VolumeAfter - SEs[i].m_VolumeBefore) / SEs[i].m_FadeTime / 60.0f;
				myAudioSource audioSource = this.FindmyAudioSource (i);
				if (audioSource == null) {
					return;
				}

				audioSource.source.volume = SEs[i].m_Volume;

				// フェード処理が完了したらフェード時間を初期化.
				if (SEs[i].m_Volume >= 1.0f) {
					SEs[i].m_Volume = 1.0f;
					SEs[i].m_FadeTime = 0.0f;
				} else if (SEs[i].m_Volume <= 0.0f) {
					SEs[i].m_Volume = 0.0f;
					SEs[i].m_FadeTime = 0.0f;
					SEs [i].FadeFlag = false;
					audioSource.source.Stop ();;
				}
			}

		}

	}

	myAudioSource FindmyAudioSource(int ID){

		foreach (myAudioSource audioSource in SESources) {
			if (audioSource.ID == ID) {
				return audioSource;
			}
		}

		return null;
	}

	/// <summary>
	/// フラグがたったBGMをフェードアウトさせる.
	/// </summary>
	void UpdateFadeOutBGM(){

		foreach (SoundSource ss in BGMs) {
			if (ss.FadeFlag == true && ss.m_FadeTime >= 0.0f) {
				// 音量を調整する.
				ss.m_Volume += (ss.m_VolumeAfter - ss.m_VolumeBefore) / ss.m_FadeTime / 60.0f;
				BGMSource.volume = ss.m_Volume;

				// フェード処理が完了したらフェード時間を初期化.
				if (ss.m_Volume >= 1.0f) {
					ss.m_Volume = 1.0f;
					ss.m_FadeTime = 0.0f;
				} else if (ss.m_Volume <= 0.0f) {
					ss.m_Volume = 0.0f;
					ss.m_FadeTime = 0.0f;
					BGMSource.Stop ();
				}
			}
		}

	}

	public void FadeOutSE(int ID){

		if (0 <= ID && ID < SEs.Length && SEs[ID].clip != null) {
			SEs [ID].FadeFlag = true;
		}

	}
				
	public void PlaySE(int ID){
	
		if (0 <= ID && ID < SEs.Length && SEs[ID].clip != null) {
			foreach (myAudioSource audiosource in SESources) {
				if (audiosource.source.isPlaying == false) {
					audiosource.source.clip = SEs[ID].clip;
					audiosource.source.volume = volume.SE;
					audiosource.source.Play ();
					audiosource.ID = ID;
					SEs [ID].m_Volume = volume.SE;
					return;
				}
			}

		} else {

			Debug.Log ("index error" + "[" + ID + "]");

		}


	}

}