using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSawCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //プレイヤーに当たったかを判断
        if(other.gameObject.CompareTag("Player"))
        {
            //CircularSawを削除
            Destroy(gameObject);
        }
    }
}
