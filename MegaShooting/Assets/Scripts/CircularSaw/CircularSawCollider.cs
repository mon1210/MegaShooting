using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSawCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //�v���C���[�ɓ����������𔻒f
        if(other.gameObject.CompareTag("Player"))
        {
            //CircularSaw���폜
            Destroy(gameObject);
        }
    }
}
