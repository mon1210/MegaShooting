using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFactoryController : MonoBehaviour
{
    // SE��BGM�ꊇ�Ǘ��̂��ߍĐ��V���O���g���C���X�^���X���쐬�o�v���p�e�B���p
    public static SoundFactoryController instance { get; private set; }
    // SE�p��AudioSource���擾
    [SerializeField] private AudioSource seAudioSource;
    // BGM�p��AudioSource���擾
    [SerializeField] private AudioSource bgmAudioSource;

    void Start()
    {
        //instance�ϐ������邩�ǂ����𔻒f
        if (instance == null)
        {
            //�Ȃ����instance����
            instance = this;
        }
        else
        {
            //����΍폜
            Destroy(gameObject);
        }
    }

    //SE���Đ�����֐�
    public void PlaySE(AudioClip clip)
    {
        seAudioSource.PlayOneShot(clip);
    }

    //BGM���Đ�����֐�
    public void PlayBGM(AudioClip clip)
    {
        bgmAudioSource.clip = clip;

        bgmAudioSource.Play();
    }

    //BGM���~����֐�
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}