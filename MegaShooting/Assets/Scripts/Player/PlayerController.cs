using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�A�j���[�V�������擾
    [SerializeField] private Animator animator;

    //�ړ����x�̒萔
    [SerializeField] private float speed;
    //�W�����v�͂̒萔
    [SerializeField] private float jump;

    //�v���C���[�̒e���擾
    [SerializeField] private GameObject playerBulletPrefab = null;
    //HitPoint��UI�I�u�W�F�N�g���擾
    [SerializeField] private GameObject hitPointUI = null;

    //�ˌ�����SE���擾
    [SerializeField] private AudioClip se_Beam;
    //�W�����v����SE���擾
    [SerializeField] private AudioClip se_Jump;

    // �W�����v�t���O
    [SerializeField] private bool isJumping = false;
    public void SetisJumping(bool val) { this.isJumping = val; }

    // �����t���O
    [SerializeField] private bool isWin = false;
    public bool GetisWin() { return this.isWin; }
    public void SetisWin(bool val) { this.isWin = val;}

    // HP
    [SerializeField] private int hitPoint = 0;
    public int GetHitPoint() { return this.hitPoint; }
    public void SetHitPoint(int hitPoint) { this.hitPoint = hitPoint; }

    //�A�j���[�V�����؂�ւ��p�̕ϐ����`�E������
    private bool isMoving;
    private bool isShooting;
    private bool isLose;

    //Rigidbody2D���擾
    [SerializeField] private Rigidbody2D rb;
    //SpriteRenderer���擾
    [SerializeField] private SpriteRenderer direction;

    //Phase�̐ݒ�
    private enum PlayerPhase
    {
        Idle,   //idle
        Jump,   //�W�����v
        LMove,  //���ړ�
        RMove,  //�E�ړ�
        Shoot,  //�ˌ�
        Win,    //����
        Lose    //�s�k
    }
    //PlayerPhase�^�̕ϐ�phase��������
    private PlayerPhase phase = PlayerPhase.Idle;


    // Start is called before the first frame update
    void Start()
    {
        //hitPoint��UI��������
        hitPointUI.GetComponent<HitpointController>().UpdatePlayerHpUI(hitPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //phase���X�V
        updatePlayerPhase();

        //�ephase�ł̏������ꍇ����
        processPlayerPhase();

        //bool�^�̕ϐ��ɃA�j���[�V������ݒ�
        setAnimationStates();

    }

    //�v���C���[��phase���X�V����֐�
    private void updatePlayerPhase()
    {
        /***** �W�����v���� *****/
        checkAndPerformJump();

        /***** �ړ����� *****/
        //���ړ�
        checkAndPerformLeftMove();
        //�E�ړ�
        checkAndPerformRightMove();
      
        /***** �ˌ����� *****/
        checkAndPerformShoot();

        /***** �s�k���� *****/
        checkAndPerformLose();

        /**** �������� *****/
        checkAndPerformWin();
        
    }

    //�ephase�ł̏������ꍇ��������֐�
    private void processPlayerPhase()
    {
        switch (phase)
        {
            //Idle��
            case PlayerPhase.Idle:
                    //Idle��Ԃ̏����֐����яo��
                    performIdle();
                break;
            //���ړ���
            case PlayerPhase.LMove:
                if (!isShooting && !isLose && !isWin)
                {
                    //���ړ����̏����֐����Ăяo��
                    performLeftMove();
                }
                break;
            //�E�ړ���
            case PlayerPhase.RMove:
                if (!isShooting && !isLose && !isWin)
                {
                    //�E�ړ����̏����֐����Ăяo��
                    performRightMove();
                }
                break;
            //�W�����v��
            case PlayerPhase.Jump:
                if (!isJumping)
                {
                    //�W�����v���̏����֐����Ăяo��
                    performJump();
                }
                break;
            //�ˌ���
            case PlayerPhase.Shoot:
                if (!isMoving && !isShooting && !isJumping && !isLose && !isWin)
                {
                    //�ˌ����̏����֐����Ăяo��
                    performShoot();
                }
                break;
            //�s�k��
            case PlayerPhase.Lose:
                    //�s�k���̏����֐����Ăяo��
                    performLose();
                break;
            //������
            case PlayerPhase.Win:
                break;

            default: break;
        }
    }

    //bool�^�̃A�j���[�V�����ϐ����ꊇ�Őݒ肷��֐�
    private void setAnimationStates()
    {
        //Bool�^�̕ϐ��ɃA�j���[�V�������Z�b�g
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsShooting", isShooting);
        animator.SetBool("IsWin", isWin);
        animator.SetBool("IsLose", isLose);
    }

    //�W�����v�����s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformJump()
    {
        //Space�L�[��������Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            phase = PlayerPhase.Jump;
        }
        //Jump�̃A�j���[�V�����I������Idle��Ԃ�
        else if (Input.GetKeyUp(KeyCode.Space) && isJumping) 
        {
            phase = PlayerPhase.Idle;
        }

    }

    //���ړ������s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformLeftMove()
    {
        //A�L�[�����Ă���ԍ��Ɉړ�
        if (Input.GetKey(KeyCode.A))
        {
            phase = PlayerPhase.LMove;
        }
        //A�L�[�𗣂���Idle��Ԃ�
        else if (Input.GetKeyUp(KeyCode.A) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

        //�ړ��L�[���E�������������Idle��Ԃ�
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.Idle;
        }

    }

    //�E�ړ������s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformRightMove()
    {
        //D�L�[�����Ă���ԉE�Ɉړ�
        if (Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.RMove;
        }
        //D�L�[�𗣂���Idle��Ԃ�
        else if (Input.GetKeyUp(KeyCode.D) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

        //�ړ��L�[���E�������������Idle��Ԃ�
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.Idle;
        }

    }

    //�ˌ������s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformShoot()
    {
        //���N���b�N�Œe�𔭎�
        if (Input.GetMouseButton(0))
        {
            phase = PlayerPhase.Shoot;
        }
        //�N���b�N�𗣂���Idle��Ԃ�
        else if (Input.GetMouseButtonUp(0) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

    }

    //�s�k�����s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformLose()
    {
        //hitPoint��0�ɂȂ�Ɣs�k
        if (hitPoint <= 0)
        {
            phase = PlayerPhase.Lose;
        }

    }

    //���������s���邽�߂̏������`�F�b�N����֐�
    private void checkAndPerformWin()
    {
        //isWin = true �ŏ���
        if (isWin)
        {
            phase = PlayerPhase.Win;
        }

    }

    // �ȉ� ��ԏ��� =======================================================
    //Idle��Ԃ̏����֐�
    private void performIdle()
    {
        //�A�j���[�V�����̒�~
        isJumping = false;
        isMoving = false;
        isShooting = false;
    }

    //�W�����v�̏����֐�
    private void performJump()
    {
        //�W�����v�̃A�j���[�V�������Đ�
        isJumping = true;

        //�ϐ�jump���g���Ĕ�ԗ͂��쐬
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

        //�W�����v����SE���Đ�
        SoundFactoryController.instance.PlaySE(se_Jump);
    }

    //���ړ��̏����֐�
    private void performLeftMove()
    {
        //����A�j���[�V�������Đ�
        isMoving = true;

        //�ϐ�speed���g���č������Ɉړ�
        transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

        //������ύX���Ă��Ȃ����
        if (!direction.flipX) 
        {
            //�v���C���[�̌������������ɕύX
            direction.flipX = true;
        }
        
    }

    //�E�ړ��̏����֐�
    private void performRightMove()
    {
        //����A�j���[�V�������Đ�
        isMoving = true;

        //�ϐ�speed���g���ĉE�����Ɉړ�
        transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);

        //������ύX���Ă��Ȃ����
        if(direction.flipX)
        {
            //�v���C���[�̌������E�����ɕύX
            direction.flipX = false;
        }
      
    }

    //�ˌ��̏����֐�
    private void performShoot()
    {
        //�ˌ��̃A�j���[�V�������Đ�
        isShooting = true;

        //�����̈ʒu�ɒe�𐶐�
        Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);

        //�ˌ�����SE���Đ�
        SoundFactoryController.instance.PlaySE(se_Beam);
    }

    //�s�k�̏����֐�
    private void performLose()
    {
        //�s�k���̃A�j���[�V�������Đ�
        isLose = true;
    }

    // �ȏ� ��ԏ��� =======================================================
}
