using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    //HitPointのUIオブジェクトを取得
    [SerializeField] private GameObject hitPointUI = null;
    //ダメージを受けた際のSEを取得
    [SerializeField] private AudioClip damageSound;
    //SpriteRendererを取得
    [SerializeField] private SpriteRenderer playerRenderer;

    //PlayerControllerスクリプトの情報を保存する変数
    private PlayerController playerControllerScripts;

    //攻撃を受けた時の点滅回数を定義・初期化
    const int FLASH_COUNT = 5;
    //攻撃を受けた時の点滅の間隔を定義・初期化
    const float FLASH_INTERVAL = 0.05f;

    //点滅中かどうかを判断するフラグを用意
    private bool isBlinking;


    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {
        //PlayerControllerスクリプトを取得
        playerControllerScripts = GetComponent<PlayerController>();

        //床に触れているかを判断
        if (other.gameObject.CompareTag("Tile"))
        {
            //床に触れたときジャンプを終了
            playerControllerScripts.SetisJumping(false);
        }

        //Bat or Batの弾 に当たった時
        if (other.gameObject.CompareTag("Bat") || other.gameObject.CompareTag("CircularSaw")) 
        {
            //プレイヤーのHpを取得、-1をして代入
            playerControllerScripts.SetHitPoint(playerControllerScripts.GetHitPoint() - 1);

            //UI更新
            hitPointUI.GetComponent<HitpointController>().UpdatePlayerHpUI(playerControllerScripts.GetHitPoint());

            //SE再生
            SoundFactoryController.instance.PlaySE(damageSound);

            //Hit中は点滅コルーチンの呼び出しを行わないように
            if (!isBlinking)
            {
                //点滅コルーチンの呼び出し
                StartCoroutine(blinkPlayer());
            }

        }

        //クリスタルに触れたかを判断
        if (other.gameObject.CompareTag("Crystal"))
        {
            //クリスタルに触れれば勝利
            playerControllerScripts.SetisWin(true);
           
        }

    }

    //プレイヤーがダメージを受けた際の点滅処理
    private IEnumerator blinkPlayer()
    {
        //点滅フラグをtrueに
        isBlinking = true;

        //点滅のループ処理
        for (int i = 0; i < FLASH_COUNT; i++)
        {
            //FLASH_INTERVAL待つ
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //プレイヤーの表示をオフに
            playerRenderer.enabled = false;

            //FLASH_INTERVAL待つ
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //プレイヤーの表示をオンに
            playerRenderer.enabled = true;
        }

        //点滅ループを抜けてフラグをfalseに
        isBlinking = false;

    }
}
