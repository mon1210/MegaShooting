using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonController : MonoBehaviour
{
    public void Title()
    {
        //�^�C�g���{�^���������ƃ^�C�g����
        SceneManager.LoadScene("TitleScene");
    }
}
