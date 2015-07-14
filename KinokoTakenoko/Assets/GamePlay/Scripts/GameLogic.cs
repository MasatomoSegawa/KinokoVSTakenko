using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLogic : MonoBehaviour {

    // Playerきのこ.
    public Kinoko kinoko;

    // 登頂ゲージ.
    public GaugeSlider gaugeSlider;

    // 地形.
    public Ground ground;

    //　ゴールまでの長さ.
    public float GoalDistance = 100;

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

    }

    /// <summary>
    /// ゲームスタート時の処理.
    /// </summary>
    void GameStart()
    {

    }

    /// <summary>
    /// シーン切り替えの処理.
    /// </summary>
    void NextSceneLoad()
    {

        Debug.Log("Load");

    }

    /// <summary>
    /// ゲームクリアー時の処理.
    /// </summary>
    void GameClear()
    {
        
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

        // 地形の更新をやめる.
        ground.FreezeFlag = true;


        Debug.Log("GameOver");

    }

}
