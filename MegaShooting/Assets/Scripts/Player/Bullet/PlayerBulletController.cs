using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    //���x�̕ϐ�
    [SerializeField] private float speed = 1.0f;
    //���Ԃ�}��ϐ�
    private float timer = 0.0f;
    //�j��܂ł̎��Ԃ̒萔
    private const float DESTROY_TIME = 1.0f;
    
    //�v���C���[�̏����擾���邽�߂̕ϐ�
    private GameObject player;
    //�v���C���[�̌������擾���邽�߂̕ϐ�
    private SpriteRenderer playerDirection;
    //�v���C���[�̌����𔻒f���邽�߂̃t���O��p��
    private bool determineDirection;
    //�e�̌������擾
    [SerializeField] private SpriteRenderer direction;


    // Start is called before the first frame update
    void Start()
    {
        //SPEED��b����
        speed *= Time.deltaTime;

        //�v���C���[�̏����擾
        player = GameObject.FindWithTag("Player");
        //�v���C���[��SpriteRenderer���擾
        playerDirection = player.GetComponent<SpriteRenderer>();

        //�v���C���\�̌����ƒe�̌����𓯂���
        changeBulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //timer�����Z
        timer += Time.deltaTime;

        //timer��DESTROY_TIME�𒴂��Ă��邩�𔻒f
        if (timer >= DESTROY_TIME)
        {
            //�e��Destroy����
            Destroy(gameObject);

            //�^�C�}�[���Z�b�g
            timer = 0.0f;
        }

        //�e�̌����ɂ���Ĉړ�������ύX
        setPlayerBulletVelocityFromDirection();

    }

    //�v���C���[�̌����ɍ��킹�Ēe�̌�����ς���֐�
    private void changeBulletDirection()
    { 
        //�v���C���[���E�����̏ꍇ
        if (!playerDirection.flipX)
        {
            //�v���C���[�̌����t���O�ɉE����(true)�������
            determineDirection = true;
        }
        //�v���C���[���������̏ꍇ
        else
        {
            //�v���C���[�̌����t���O�ɍ�����(false)�������
            determineDirection = false;

            //�e����������
            direction.flipX = false;
        }
    }

    //�e�̌����ɍ��킹�Ĉړ�������ύX����֐�
    private void setPlayerBulletVelocityFromDirection()
    {
        //�v���C���[���E�����̎�
        if (determineDirection)
        {
            //���̕���������
            transform.Translate(speed, 0.0f, 0.0f);
        }
        //�v���C���[���������̎�
        else
        {
            //���̕����ɂ�����
            transform.Translate(-speed, 0.0f, 0.0f);
        }
    }

}

