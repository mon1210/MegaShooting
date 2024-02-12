using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : MonoBehaviour
{
    //CrystalControllerPrefabをPrefab
    [SerializeField] private GameObject crystalPrefab = null;
    //CrystalControllerスクリプトの取得
    private CrystalController crystalController;
    //Batがダメージを受けた際のSEを取得
    [SerializeField] private AudioClip damageSound;

    //BatControllerスクリプトの情報を取得するための変数
    private BatController batControllerScripts;

    //SpriteRendererを取得
    [SerializeField] private SpriteRenderer batRenderer;

    //攻撃を受けた時の点滅回数を定義・初期化
    const int FLASH_COUNT = 5;
    //攻撃を受けた時の点滅の間隔を定義・初期化
    const float FLASH_INTERVAL = 0.05f;
    
    //点滅中かどうかを判断するフラグを用意
    private bool isBlinking;
    //初期化が完了しているかを判断するフラグ
    private bool initialized;

    void Start()
    {
        //BatControllerスクリプトを取得
        batControllerScripts = GetComponent<BatController>();
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーの弾にあっているかを判断
        if (collision.CompareTag("PlayerBullet"))
        {
            //BatのHpを取得、-1をして代入
            batControllerScripts.SetBatHp(batControllerScripts.GetBatHp() - 1);
            //ダメージ受けた時のSEを再生
            SoundFactoryController.instance.PlaySE(damageSound);

            //hpが0になったかを判断
            if (batControllerScripts.GetBatHp() <= 0) 
            {
                //Bat死亡
                death();
            }

            //Hit中は点滅コルーチンの呼び出しを行わない
            if (!isBlinking)
            {
                //点滅コルーチンの呼び出し
                StartCoroutine(blinkingBat());
            }


        }

    }

    private IEnumerator blinkingBat()
    {
        //点滅フラグをtrueに
        isBlinking = true;

        //点滅処理
        for (int i = 0; i < FLASH_COUNT; i++)
        {
            //0.05f待つ
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //Batの表示をオフに
            batRenderer.enabled = false;

            //0.05f待つ
            yield return new WaitForSeconds(FLASH_INTERVAL);
            //オン
            batRenderer.enabled = true;
        }

        //点滅ループを抜けてフラグをfalseに
        isBlinking = false;
    }

    //Batが死亡時の処理
    private void death()
    {
        //死亡時見えなくする
        gameObject.SetActive(false);

        //クリスタルを自分の位置に出現させる
        Instantiate(crystalPrefab, transform.position, Quaternion.identity);

        //初期化が完了していなければ初期化メソッドを呼ぶ
        if(!initialized)
        {
            initializeCrystalController();
        }

        //クリスタルを自分の位置に出現させる
        crystalController.ChangeActive(true);

    }

    //CrystalControllerを初期化する関数
    private void initializeCrystalController()
    {
        //CrystalControllerスクリプトで取得
        crystalController = crystalPrefab.GetComponent<CrystalController>();

        //初期化の完了をフラグで示す
        initialized = true;
    }
}
