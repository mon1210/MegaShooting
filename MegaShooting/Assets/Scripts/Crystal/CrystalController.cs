using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    //batの情報を取得
    [SerializeField] private GameObject bat;
    //Bat死亡時のSEを取得
    [SerializeField] private AudioClip se_BossExplosion;

    //クリスタルのアクティブ状態を変更する関数
    public void ChangeActive(bool isActive)
    {
        //クリスタルのアクティブ状態を引数の状態にセット
        this.gameObject.SetActive(isActive);

        //ボス死亡SE
        SoundFactoryController.instance.PlaySE(se_BossExplosion);

        //Batの位置に出現
        transform.position = bat.transform.position;
    }

}
