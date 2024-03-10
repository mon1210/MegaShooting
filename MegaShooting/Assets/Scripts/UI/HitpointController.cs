using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointController : MonoBehaviour
{
    //HitPointObject���擾
    [SerializeField] private GameObject[] hitPointObjects;

    //�v���C���[��HitPointUI�̍X�V������֐�
    public void UpdatePlayerHpUI(int current_hp)
    {
        //���ׂĔ�A�N�e�B�u�ɂ���
        for (int i = 0; i < hitPointObjects.Length; i++)
        {
            //�A�N�e�B�u��Ԃ̏ꍇ
            if (hitPointObjects[i].activeSelf)
            {
                //��A�N�e�B�u��Ԃ�
                hitPointObjects[i].SetActive(false);
            }

        }
        
        //�ꕔ��hitPoint�������A�N�e�B�u�ɂ���
        for (int i = 0; i < current_hp; i++)
        {
            //��A�N�e�B�u��Ԃ̏ꍇ
            if (!hitPointObjects[i].activeSelf) 
            {
                //�A�N�e�B�u��Ԃ�
                hitPointObjects[i].SetActive(true);
            }

        }
    }
}
