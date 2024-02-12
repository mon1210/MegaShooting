using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    //bat�̏����擾
    [SerializeField] private GameObject bat;
    //Bat���S����SE���擾
    [SerializeField] private AudioClip se_BossExplosion;

    //�N���X�^���̃A�N�e�B�u��Ԃ�ύX����֐�
    public void ChangeActive(bool isActive)
    {
        //�N���X�^���̃A�N�e�B�u��Ԃ������̏�ԂɃZ�b�g
        this.gameObject.SetActive(isActive);

        //�{�X���SSE
        SoundFactoryController.instance.PlaySE(se_BossExplosion);

        //Bat�̈ʒu�ɏo��
        transform.position = bat.transform.position;
    }

}
