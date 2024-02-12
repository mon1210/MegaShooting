using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //プレイヤーと当たったかを判断
        if (other.gameObject.CompareTag("Player"))
        {
            //クリスタルを削除
            Destroy(gameObject);
        }
    }
}
