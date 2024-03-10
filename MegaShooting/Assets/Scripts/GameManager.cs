using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameOverTextを取得
    [SerializeField] private GameObject gameOverText;
    //GameClearTextを取得
    [SerializeField] private GameObject gameClearText;
    //RetrayButtonを取得
    [SerializeField] private GameObject retrayButton;
    //TitleButtonを取得
    [SerializeField] private GameObject titleButton;

    //BGMを取得
    [SerializeField] private AudioClip bgmClip;
    //GameOver時のSEを取得
    [SerializeField] private AudioClip se_GameOver;
    //GameClearのSEを取得
    [SerializeField] private AudioClip se_GameClear;

    //PlayerControllerスクリプトの情報を取得するための変数
    private PlayerController playerControllerScripts;

    void Start()
    {
        //PlayerControllerスクリプトを取得
        playerControllerScripts = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //BGMを再生
        SoundFactoryController.instance.PlayBGM(bgmClip);

    }

    void Update()
    {
        //プレイヤーのHPが0以下かを判断
        if (playerControllerScripts.GetHitPoint() <= 0)
        {
            //ゲームオーバー関数の呼び出し
            handleGameOver();

        }
        //勝利かどうかを判断
        else if (playerControllerScripts.GetisWin())
        {
            //ゲームクリア関数の呼び出し
            handleGameClear();
        }

    }

    //ゲームオーバー時の処理をする関数
    private void handleGameOver()
    {
        //テキスト・リトライボタン非表示の時
        if (!gameOverText.activeSelf && !retrayButton.activeSelf && !titleButton.activeSelf)
        {
            //ゲームオーバーテキストを表示
            gameOverText.SetActive(true);
            //リスタートボタンを表示
            retrayButton.SetActive(true);
            //タイトルボタンを表示
            titleButton.SetActive(true);

            //BGMを停止
            SoundFactoryController.instance.StopBGM();
            //GameOverSEを再生
            SoundFactoryController.instance.PlaySE(se_GameOver);
        }
      
    }

    //ゲームクリア時の処理をする関数
    private void handleGameClear()
    {
        //テキスト・リトライボタン非表示の時
        if (!gameClearText.activeSelf && !retrayButton.activeSelf && !titleButton.activeSelf) 
        {
            //ゲームクリアテキストを表示
            gameClearText.SetActive(true);
            //リスタートボタンを表示
            retrayButton.SetActive(true);
            //タイトルボタンを表示
            titleButton.SetActive(true);

            //BGMを停止
            SoundFactoryController.instance.StopBGM();
            //GameClearのSEを再生
            SoundFactoryController.instance.PlaySE(se_GameClear);
        }

    }

}
