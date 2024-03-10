using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : MonoBehaviour
{
    //CrystalControllerPrefab��Prefab
    [SerializeField] private GameObject crystalPrefab = null;
    //CrystalController�X�N���v�g�̎擾
    private CrystalController crystalController;
    //Bat���_���[�W���󂯂��ۂ�SE���擾
    [SerializeField] private AudioClip damageSound;

    //BatController�X�N���v�g�̏����擾���邽�߂̕ϐ�
    private BatController batControllerScripts;

    //SpriteRenderer���擾
    [SerializeField] private SpriteRenderer batRenderer;

    //�U�����󂯂����̓_�ŉ񐔂��`�E������
    const int FLASH_COUNT = 5;
    //�U�����󂯂����̓_�ł̊Ԋu���`�E������
    const float FLASH_INTERVAL = 0.05f;
    
    //�_�Œ����ǂ����𔻒f����t���O��p��
    private bool isBlinking;
    //���������������Ă��邩�𔻒f����t���O
    private bool initialized;

    void Start()
    {
        //BatController�X�N���v�g���擾
        batControllerScripts = GetComponent<BatController>();
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�̒e�ɂ����Ă��邩�𔻒f
        if (collision.CompareTag("PlayerBullet"))
        {
            //Bat��Hp���擾�A-1�����đ��
            batControllerScripts.SetBatHp(batControllerScripts.GetBatHp() - 1);
            //�_���[�W�󂯂�����SE���Đ�
            SoundFactoryController.instance.PlaySE(damageSound);

            //hp��0�ɂȂ������𔻒f
            if (batControllerScripts.GetBatHp() <= 0) 
            {
                //Bat���S
                death();
            }

            //Hit���͓_�ŃR���[�`���̌Ăяo�����s��Ȃ�
            if (!isBlinking)
            {
                //�_�ŃR���[�`���̌Ăяo��
                StartCoroutine(blinkingBat());
            }


        }

    }

    private IEnumerator blinkingBat()
    {
        //�_�Ńt���O��true��
        isBlinking = true;

        //�_�ŏ���
        for (int i = 0; i < FLASH_COUNT; i++)
        {
            //0.05f�҂�
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //Bat�̕\�����I�t��
            batRenderer.enabled = false;

            //0.05f�҂�
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //�I��
            batRenderer.enabled = true;
        }

        //�_�Ń��[�v�𔲂��ăt���O��false��
        isBlinking = false;
    }

    //Bat�����S���̏���
    private void death()
    {
        //���S�������Ȃ�����
        gameObject.SetActive(false);

        //�N���X�^���������̈ʒu�ɏo��������
        Instantiate(crystalPrefab, transform.position, Quaternion.identity);

        //���������������Ă��Ȃ���Ώ��������\�b�h���Ă�
        if(!initialized)
        {
            initializeCrystalController();
        }

        //�N���X�^���������̈ʒu�ɏo��������
        crystalController.ChangeActive(true);

    }

    //CrystalController������������֐�
    private void initializeCrystalController()
    {
        //CrystalController�X�N���v�g�Ŏ擾
        crystalController = crystalPrefab.GetComponent<CrystalController>();

        //�������̊������t���O�Ŏ���
        initialized = true;
    }
}
