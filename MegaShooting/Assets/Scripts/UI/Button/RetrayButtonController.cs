using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrayButtonController : MonoBehaviour
{ 
    public void Retray()
    {
        //���g���C�{�^���������ƃQ�[���̍ŏ�����
        SceneManager.LoadScene("GameScene");
    }
}
