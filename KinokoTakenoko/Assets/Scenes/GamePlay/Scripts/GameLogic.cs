using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameLogicState
{
    GameStart, GamePlay, GameClear,GameOver,
}

public class GameLogic : MonoBehaviour {

    // ゲームロジックの状態.
    public GameLogicState myState;

    //　ゴールまでの長さ.
    public int GoalDistance = 100;

    #region ゲームオブジェクト

    // StartText
    public Text StartText;

    // EndText
    public Text EndText;

    // Playerきのこ.
    public Kinoko kinoko;

    // 登頂ゲージ.
    public GaugeSlider gaugeSlider;

    // 地形.
    public Ground ground;

    #endregion

    /// <summary>
    /// イベントを登録する。
    /// </summary>
    void Awake()
    {

        // ゲージの初期設定をする.
        this.gaugeSlider.GoalDistance = this.GoalDistance;

        // ゲームオーバー条件.
        this.kinoko.DeathEvent += GameOver;

        // ゲームクリアー条件.
        this.gaugeSlider.ReachGoalEvent += GameClear;

        // シーン切り替えの処理.
        this.kinoko.EndGoalMotionEvent += NextSceneLoad;

        GameStart();

    }

    void Update()
    {

        CheckInput();

    }

    /// <summary>
    /// state毎の入力を監視.
    /// </summary>
    void CheckInput()
    {

        switch (myState)
        {
            case GameLogicState.GameStart:
                CheckInputForGameStart();
                break;

            case GameLogicState.GamePlay:
                break;

            case GameLogicState.GameOver:
                CheckInputForGameEnd();
                break;

            case GameLogicState.GameClear:
                break;
        }

    }

    void CheckInputForGameEnd()
    {

        if (Input.anyKeyDown)
        {
            FadeManager.Instance.LoadLevel("GamePlay", 1.0f);
        }

    }

    /// <summary>
    /// ゲームスタート時の入力を監視.
    /// </summary>
    void CheckInputForGameStart()
    {

        if (Input.anyKeyDown)
        {
            InitGamePlay();
        }

    }

    /// <summary>
    /// ゲームプレイ時の初期化関数.
    /// </summary>
    void InitGamePlay()
    {

        // BGM再生.
        SoundManager.Instance.PlayBGM(0);

        // ゲームロジックの状態を代入.
        myState = GameLogicState.GamePlay;

        // プレイヤーをフリーズ.
        kinoko.FreezeFlag = false;

        // 地形をフリーズ.
        ground.FreezeFlag = false;

        // スライダーをフリーズ.
        gaugeSlider.FreezeFlag = false;

        // スタートテキストをオフ.
        StartText.enabled = false;

    }

    /// <summary>
    /// ゲームスタート時の処理.
    /// </summary>
    void GameStart()
    {

        // ゲームロジックの状態を代入.
        myState = GameLogicState.GameStart;

        // プレイヤーをフリーズ.
        kinoko.FreezeFlag = true;

        // 地形をフリーズ.
        ground.FreezeFlag = true;

        // スライダーをフリーズ.
        gaugeSlider.FreezeFlag = true;

    }

    /// <summary>
    /// シーン切り替えの処理.
    /// </summary>
    void NextSceneLoad()
    {

        FadeManager.Instance.LoadLevel("End", 1.0f);

    }

    /// <summary>
    /// ゲームクリアー時の処理.
    /// </summary>
    void GameClear()
    {

        // BGM停止.
        SoundManager.Instance.StopBGM();

        // SE再生.
        SoundManager.Instance.PlaySE(1);

        myState = GameLogicState.GameClear;
        
        // 地形の更新をやめる.
        ground.FreezeFlag = true;

        // きのこをゴール処理させる.
        kinoko.isGoal = true;        

        Debug.Log("GameClear");

    }

    /// <summary>
    /// ゲーム終了時の処理.
    /// </summary>
    void GameOver()
    {

        // BGM停止.
        SoundManager.Instance.StopBGM();
        
        // SE再生.
        SoundManager.Instance.PlaySE(0);

        myState = GameLogicState.GameOver;

        // 地形の更新をやめる.
        ground.FreezeFlag = true;

        // スライダーをフリーズ.
        gaugeSlider.FreezeFlag = true;

        // EndTextをオン.
        EndText.enabled = true;

        Debug.Log("GameOver");

    }

}
