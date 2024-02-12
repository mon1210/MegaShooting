using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameOverText���擾
    [SerializeField] private GameObject gameOverText;
    //GameClearText���擾
    [SerializeField] private GameObject gameClearText;
    //RetrayButton���擾
    [SerializeField] private GameObject retrayButton;
    //TitleButton���擾
    [SerializeField] private GameObject titleButton;

    //BGM���擾
    [SerializeField] private AudioClip bgmClip;
    //GameOver����SE���擾
    [SerializeField] private AudioClip se_GameOver;
    //GameClear��SE���擾
    [SerializeField] private AudioClip se_GameClear;

    //PlayerController�X�N���v�g�̏����擾���邽�߂̕ϐ�
    private PlayerController playerControllerScripts;

    void Start()
    {
        //PlayerController�X�N���v�g���擾
        playerControllerScripts = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //BGM���Đ�
        SoundFactoryController.instance.PlayBGM(bgmClip);

    }

    void Update()
    {
        //�v���C���[��HP��0�ȉ����𔻒f
        if (playerControllerScripts.GetHitPoint() <= 0)
        {
            //�Q�[���I�[�o�[�֐��̌Ăяo��
            handleGameOver();

        }
        //�������ǂ����𔻒f
        else if (playerControllerScripts.GetisWin())
        {
            //�Q�[���N���A�֐��̌Ăяo��
            handleGameClear();
        }

    }

    //�Q�[���I�[�o�[���̏���������֐�
    private void handleGameOver()
    {
        //�e�L�X�g�E���g���C�{�^����\���̎�
        if (!gameOverText.activeSelf && !retrayButton.activeSelf && !titleButton.activeSelf)
        {
            //�Q�[���I�[�o�[�e�L�X�g��\��
            gameOverText.SetActive(true);
            //���X�^�[�g�{�^����\��
            retrayButton.SetActive(true);
            //�^�C�g���{�^����\��
            titleButton.SetActive(true);

            //BGM���~
            SoundFactoryController.instance.StopBGM();
            //GameOverSE���Đ�
            SoundFactoryController.instance.PlaySE(se_GameOver);
        }
      
    }

    //�Q�[���N���A���̏���������֐�
    private void handleGameClear()
    {
        //�e�L�X�g�E���g���C�{�^����\���̎�
        if (!gameClearText.activeSelf && !retrayButton.activeSelf && !titleButton.activeSelf) 
        {
            //�Q�[���N���A�e�L�X�g��\��
            gameClearText.SetActive(true);
            //���X�^�[�g�{�^����\��
            retrayButton.SetActive(true);
            //�^�C�g���{�^����\��
            titleButton.SetActive(true);

            //BGM���~
            SoundFactoryController.instance.StopBGM();
            //GameClear��SE���Đ�
            SoundFactoryController.instance.PlaySE(se_GameClear);
        }

    }

}
