using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonController : MonoBehaviour
{
    public void Title()
    {
        //タイトルボタンを押すとタイトルへ
        SceneManager.LoadScene("TitleScene");
    }
}
