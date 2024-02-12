using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointController : MonoBehaviour
{
    //HitPointObjectを取得
    [SerializeField] private GameObject[] hitPointObjects;

    //プレイヤーのHitPointUIの更新をする関数
    public void UpdatePlayerHpUI(int current_hp)
    {
        //すべて非アクティブにする
        for (int i = 0; i < hitPointObjects.Length; i++)
        {
            //アクティブ状態の場合
            if (hitPointObjects[i].activeSelf)
            {
                //非アクティブ状態に
                hitPointObjects[i].SetActive(false);
            }

        }
        
        //一部のhitPointだけをアクティブにする
        for (int i = 0; i < current_hp; i++)
        {
            //非アクティブ状態の場合
            if (!hitPointObjects[i].activeSelf) 
            {
                //アクティブ状態に
                hitPointObjects[i].SetActive(true);
            }

        }
    }
}
