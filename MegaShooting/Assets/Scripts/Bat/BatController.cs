using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    //�ړ����x�̒萔
    [SerializeField] private float speed;

    //Bat�̒e���擾
    [SerializeField] private GameObject circularSawPrefab = null;

    //Bat��HitPoint���`
    [SerializeField] private int hitPoint;

    //���Ԍv���̂��߂̕ϐ�
    private float timer = 0.0f;

    //�����̊Ԋu�̒萔
    private const float MOVE_INTERVAL = 1.0f;

    //�����_���Ȉړ�����������邽�߂̕ϐ�
    private int randomDistanceX;
    private int randomDistanceY;

    //�����̊m����\���萔
    private const int MOVEMENT_PROBABILITY = 90;
    //�e�𔭎˂���m����\���萔
    private const int SHOT_PROBABILITY = 60;

    //X���ɑ΂���ړ������̍ŏ��l
    const int MIN_DISTANCE_X = -100;
    //X���ɑ΂���ړ������̍ő�l
    const int MAX_DISTANCE_X = 100;
    //Y���ɑ΂���ړ������̍ŏ��l
    const int MIN_DISTANCE_Y = -50;
    //Y���ɑ΂���ړ������̍ő�l
    const int MAX_DISTANCE_Y = 50;

    //�����̍ŏ��l
    const int MIN_VALUE = 1;
    //�����̍ő�l
    const int MAX_VALUE = 100;

    //Rigidbody2D���擾
    [SerializeField] private Rigidbody2D rb;
    //SpriteRenderer���擾
    [SerializeField] private SpriteRenderer direction;


    //Bat��HitPoint�𑼃N���X�Ŏ擾�ł���悤��
    public int GetBatHp() { return hitPoint; }
    //Bat��HitPoint�𑼃N���X�ŕύX�ł���悤��
    public void SetBatHp(int val) { hitPoint = val; }

    void Update()
    {
        //timer�����Z
        timer += Time.deltaTime;

        //MOVE_INTERVAL���Ƃɓ������o��
        if (timer >= MOVE_INTERVAL) 
        {
            //�����̊֐��̌Ăяo��
            moving();
            //�^�C�}�[���Z�b�g
            timer = 0.0f;
        }
        

    }

    //Bat�̋������߂��֐�
    private void moving()
    {
        //�ړ��̋����������_����
        randomDistanceX = Random.Range(MIN_DISTANCE_X, MAX_DISTANCE_X);
        randomDistanceY = Random.Range(MIN_DISTANCE_Y, MAX_DISTANCE_Y);

        //�����̊���
        int value = Random.Range(MIN_VALUE, MAX_VALUE);
        //90%�̊m��
        if (value <= MOVEMENT_PROBABILITY)
        {
            //�����_���ɒ�߂�ꂽ����*speed�ňړ�
            rb.AddForce(new Vector2(randomDistanceX * speed, randomDistanceY * speed));
        }
        //60%�̊m��
        if (value <= SHOT_PROBABILITY)
        {
            //CircularSaw�𔭎˂���
            shootCircularSaw();
        }

        //Bat�̌�����ύX
        changeBatDirection();
    }

    //CircularSaw��ł֐�
    private void shootCircularSaw()
    {
        //Bat�̈ʒu��CircularSaw���o��
        Instantiate(circularSawPrefab, transform.position, Quaternion.identity);
    }

    //Bat�̌�����ς��邽�߂̊֐�
    private void changeBatDirection()
    {
        //��ʂ̒��S��荶���ɂ��鎞
        if (transform.position.x <= 0)
        {
            if (direction.flipX) 
            {
                //��������
                direction.flipX = false;
            }
           
        }
        //��ʂ̒��S���E���ɂ��鎞
        else
        {
            if (!direction.flipX) 
            {
                //�E������
                direction.flipX = true;
            }
           
        }
    }
}
