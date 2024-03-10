using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CircularSawController : MonoBehaviour
{
    //�e�̐i�s��ň����ɓ���邽�߂̕ϐ�
    private float timer = 0.0f;

    //�x�W�F�Ȑ������p
    private Vector2 startPos;       //�ŏ��̈ʒu
    private Vector2 relayPos;       //���p�n�_
    private Vector2 destinationPos; //�ڕW�n�_
    
    //�����扽�ʂŋ����ۂ߂���΂��������߂�ϐ�
    private int roundingDigits = 3;

    //���W
    private float posX; //X
    private float posY; //Y

    //���W�A���P�ʂł̉�]��\���ϐ�
    private float rotateRad = 0.0f;
    //���E�̉�]�p�x�̒萔
    const float LIMID_ROTATERAD = 2.0f * Mathf.PI / 180.0f;

    //���x��\���ϐ�
    [SerializeField] private float speed;

    //�I�u�W�F�N�g�擾�̂��߂̕ϐ�
    private GameObject player;
    private GameObject bat;
    private GameObject circularSaw;

    //SpriteRenderer���擾���邽�߂̕ϐ�
    private SpriteRenderer batRenderer;
    private SpriteRenderer circularSawRenderer;

    //�摜�T�C�Y�̏���ۑ�����ϐ�
    private float batScaleHalf;
    private float circularSawScaleHalf;

    //�s���ƋA��̐؂�ւ��𔻒f����t���O
    private bool isReturning = true;


    void Start()
    {
        //�I�u�W�F�N�g�擾
        player = GameObject.FindWithTag("Player");
        bat = GameObject.FindWithTag("Bat");
        circularSaw = GameObject.FindWithTag("CircularSaw");

        //SpriteRenderer���擾
        batRenderer = bat.GetComponent<SpriteRenderer>();
        circularSawRenderer = circularSaw.GetComponent<SpriteRenderer>();

        //�摜�̃s�N�Z���T�C�Y���擾(�����`�Ȃ̂�x�̂ݎ擾)
        var batWidth = batRenderer.bounds.size.x;
        var circularSawWidth = circularSawRenderer.bounds.size.x;
           

        //����������
        timer = 0.0f;
        speed *= Time.deltaTime;
        startPos = this.transform.position;
        destinationPos = player.transform.position;
        relayPos = this.transform.position + player.transform.position;
        relayPos.y = 1.0f;
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        //���a�����߂�
        batScaleHalf = batWidth / 2;
        circularSawScaleHalf = circularSawWidth / 2;


    }

    void Update()
    {
        //�ڕW�n�_�ɓ��B���Ă��Ȃ��Ƃ�
        if(isReturning)
        {
            //�x�W�F�Ȑ����g���ĖڕW�Ɍ�����
            moveBulletWithBezierCurve();
        }
        //�ڕW�n�_�ɓ��B�����Ƃ�
        else
        {
            //�ڕW�n�_����A���Ă���
            bulletReturnMove(batScaleHalf, circularSawScaleHalf);

        }

    }

    //�x�W�F�Ȑ���`���ĖڕW�֌������֐�
    private void moveBulletWithBezierCurve()
    {
        //�e�̐i�s��iLerp�̑�O�����ɓ����j
        timer += Time.deltaTime;

        //�񎟃x�W�F�Ȑ����g���A���W��ύX -------------------------

        //�X�^�[�g�ʒu���璆�p�n�_�̒��ԃx�N�g����timer�̊����ŎZ�o
        Vector3 firstVec = Vector3.Lerp(startPos, relayPos, timer);
        //���p�n�_����^�[�Q�b�g�̈ʒu
        Vector3 SecondVec = Vector3.Lerp(relayPos, destinationPos, timer);

        //��̓�_���Ȃ����ԃx�N�g����timer�̊����ŎZ�o
        Vector3 vec = Vector3.Lerp(firstVec, SecondVec, timer);

        //���W�ύX
        this.transform.position = vec;


        //Vector2�^�̕ϐ���������3�ʂ܂ŋ����ۂ߂����� => float�^�̒l�������ɂȂ�Ȃ�����
        Vector2 roundedDestinationPos = roundVector(destinationPos, roundingDigits);
        Vector2 roundedVec = roundVector(vec, roundingDigits);

        //�ڕW�n�_�ƒe�̈ʒu����v����ꍇ
        if (roundedDestinationPos == roundedVec)
        {
            //���B�t���O�𔽓]
            isReturning = !isReturning;

            //���B�����n�_��Return���̏����n�_��
            posX = roundedDestinationPos.x;
            posY = roundedDestinationPos.y;

            timer = 0.0f;

        }
        
    }

    //�ڕW�ɂ��ǂ蒅������ɁABat��ǔ����A���Ă���֐�
    private void bulletReturnMove(float getBatScale,float getCircularSawScale)
    {
        //�����x�N�g�������߂�
        float VecX = Mathf.Cos(this.rotateRad);
        float VecY = Mathf.Sin(this.rotateRad);

        //�e���{�X�ւ̃x�N�g�������߂�
        float SawtoTargetX = bat.transform.position.x - this.posX;
        float SawtoTargetY = bat.transform.position.y - this.posY;
        //�傫�������߂�
        float SawtoTargetNorm = Mathf.Sqrt(SawtoTargetX * SawtoTargetX + SawtoTargetY * SawtoTargetY);

        //�������Ă��Ȃ����(Bat�ɋA���Ă��Ă��Ȃ����)
        if (SawtoTargetNorm > (getBatScale + getCircularSawScale))
        {
            //�P�ʃx�N�g����
            SawtoTargetX /= SawtoTargetNorm;
            SawtoTargetY /= SawtoTargetNorm;

            //���ۂɉ�]����p�x
            float turnRad = LIMID_ROTATERAD;
            // ���όv�Z
            //�K�v�ȉ�]�p
            float costheta = VecX * SawtoTargetX + VecY * SawtoTargetY;

            //�ő����p�x���O���ɂ���Ƃ�
            if (costheta > Mathf.Cos(LIMID_ROTATERAD))
            {
                if (costheta <= 1.0f)
                    turnRad = Mathf.Acos(costheta);
                else
                    turnRad = 0.0f;
            }

            //�O�ς̌v�Z
            float crossZ = VecX * SawtoTargetY - SawtoTargetX * VecY;
            //�O�ς��}�C�i�X(�����č����ɖڕW������)��
            if (crossZ < 0.0f)
            {
                //���ɉ�]
                turnRad *= -1;
            }
            //��]�p�̉��Z(���ʂ�0)
            this.rotateRad += turnRad;


        }
        //Bat�ɓ�����Ώ�����
        else
        {
            Destroy(gameObject);
        }

        //���W�̌v�Z
        this.posX += Mathf.Cos(this.rotateRad) * speed;
        this.posY += Mathf.Sin(this.rotateRad) * speed;

        //���W�̍X�V
        Vector3 pos = this.transform.position;
        pos.x = this.posX;
        pos.y = this.posY;

        this.transform.position = pos;

    }

    //Vector2�^�̕ϐ����w�肵�������_�ŋ����ۂ߂��ĕԂ��֐� => float�^�̒l�������ɂȂ�Ȃ�����
    private Vector2 roundVector(Vector2 vector,int decimalPlaces)
    {
        //10�� decimalPlaces ����v�Z
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        //�w�肵�����������ɍ��킹�ċ����ۂ߂��邽�߂ɁA(vector.x(y) * multiplier) / multiplier)���v�Z
        return new Vector2(Mathf.Round(vector.x * multiplier) / multiplier, Mathf.Round(vector.y * multiplier) / multiplier);
    }
}
