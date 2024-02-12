using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    //HitPoint��UI�I�u�W�F�N�g���擾
    [SerializeField] private GameObject hitPointUI = null;
    //�_���[�W���󂯂��ۂ�SE���擾
    [SerializeField] private AudioClip damageSound;
    //SpriteRenderer���擾
    [SerializeField] private SpriteRenderer playerRenderer;

    //PlayerController�X�N���v�g�̏���ۑ�����ϐ�
    private PlayerController playerControllerScripts;

    //�U�����󂯂����̓_�ŉ񐔂��`�E������
    const int FLASH_COUNT = 5;
    //�U�����󂯂����̓_�ł̊Ԋu���`�E������
    const float FLASH_INTERVAL = 0.05f;

    //�_�Œ����ǂ����𔻒f����t���O��p��
    private bool isBlinking;


    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {
        //PlayerController�X�N���v�g���擾
        playerControllerScripts = GetComponent<PlayerController>();

        //���ɐG��Ă��邩�𔻒f
        if (other.gameObject.CompareTag("Tile"))
        {
            //���ɐG�ꂽ�Ƃ��W�����v���I��
            playerControllerScripts.SetisJumping(false);
        }

        //Bat or Bat�̒e �ɓ���������
        if (other.gameObject.CompareTag("Bat") || other.gameObject.CompareTag("CircularSaw")) 
        {
            //�v���C���[��Hp���擾�A-1�����đ��
            playerControllerScripts.SetHitPoint(playerControllerScripts.GetHitPoint() - 1);

            //UI�X�V
            hitPointUI.GetComponent<HitpointController>().UpdatePlayerHpUI(playerControllerScripts.GetHitPoint());

            //SE�Đ�
            SoundFactoryController.instance.PlaySE(damageSound);

            //Hit���͓_�ŃR���[�`���̌Ăяo�����s��Ȃ��悤��
            if (!isBlinking)
            {
                //�_�ŃR���[�`���̌Ăяo��
                StartCoroutine(blinkPlayer());
            }

        }

        //�N���X�^���ɐG�ꂽ���𔻒f
        if (other.gameObject.CompareTag("Crystal"))
        {
            //�N���X�^���ɐG���Ώ���
            playerControllerScripts.SetisWin(true);
           
        }

    }

    //�v���C���[���_���[�W���󂯂��ۂ̓_�ŏ���
    private IEnumerator blinkPlayer()
    {
        //�_�Ńt���O��true��
        isBlinking = true;

        //�_�ł̃��[�v����
        for (int i = 0; i < FLASH_COUNT; i++)
        {
            //FLASH_INTERVAL�҂�
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //�v���C���[�̕\�����I�t��
            playerRenderer.enabled = false;

            //FLASH_INTERVAL�҂�
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //�v���C���[�̕\�����I����
            playerRenderer.enabled = true;
        }

        //�_�Ń��[�v�𔲂��ăt���O��false��
        isBlinking = false;

    }
}
