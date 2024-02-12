using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    //AudioSourceを取得
    [SerializeField] private AudioSource audioSource;

    //スタートボタンのSEを取得
    [SerializeField] private AudioClip startSound;

    public void StartGame()
    {
        //スタートボタンのSEを再生
        audioSource.PlayOneShot(startSound);

        //スタートボタンを押すとゲームシーンへ
        SceneManager.LoadScene("GameScene");

    }
}
