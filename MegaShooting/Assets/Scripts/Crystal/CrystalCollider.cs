using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //�v���C���[�Ɠ����������𔻒f
        if (other.gameObject.CompareTag("Player"))
        {
            //�N���X�^�����폜
            Destroy(gameObject);
        }
    }
}
