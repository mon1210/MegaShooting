using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrayButtonController : MonoBehaviour
{ 
    public void Retray()
    {
        //リトライボタンを押すとゲームの最初から
        SceneManager.LoadScene("GameScene");
    }
}
