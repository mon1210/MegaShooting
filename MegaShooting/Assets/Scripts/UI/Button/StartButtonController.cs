using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    //AudioSource���擾
    [SerializeField] private AudioSource audioSource;

    //�X�^�[�g�{�^����SE���擾
    [SerializeField] private AudioClip startSound;

    public void StartGame()
    {
        //�X�^�[�g�{�^����SE���Đ�
        audioSource.PlayOneShot(startSound);

        //�X�^�[�g�{�^���������ƃQ�[���V�[����
        SceneManager.LoadScene("GameScene");

    }
}
