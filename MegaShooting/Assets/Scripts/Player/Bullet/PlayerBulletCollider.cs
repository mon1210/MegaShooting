using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Bat�ɓ������Ă��邩�𔻒f
        if (collision.CompareTag("Bat"))
        {
            //�e���폜
            Destroy(gameObject);
        }
    }
}
