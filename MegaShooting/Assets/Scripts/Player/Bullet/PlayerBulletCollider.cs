using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Batに当たっているかを判断
        if (collision.CompareTag("Bat"))
        {
            //弾を削除
            Destroy(gameObject);
        }
    }
}
