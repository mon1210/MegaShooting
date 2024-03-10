using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFactoryController : MonoBehaviour
{
    // SEやBGM一括管理のため再生シングルトンインスタンスを作成｛プロパティ化｝
    public static SoundFactoryController instance { get; private set; }
    // SE用のAudioSourceを取得
    [SerializeField] private AudioSource seAudioSource;
    // BGM用のAudioSourceを取得
    [SerializeField] private AudioSource bgmAudioSource;

    void Start()
    {
        //instance変数があるかどうかを判断
        if (instance == null)
        {
            //なければinstanceを代入
            instance = this;
        }
        else
        {
            //あれば削除
            Destroy(gameObject);
        }
    }

    //SEを再生する関数
    public void PlaySE(AudioClip clip)
    {
        seAudioSource.PlayOneShot(clip);
    }

    //BGMを再生する関数
    public void PlayBGM(AudioClip clip)
    {
        bgmAudioSource.clip = clip;

        bgmAudioSource.Play();
    }

    //BGMを停止する関数
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}