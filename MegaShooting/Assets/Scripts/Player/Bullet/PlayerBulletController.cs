using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    //速度の変数
    [SerializeField] private float speed = 1.0f;
    //時間を図る変数
    private float timer = 0.0f;
    //破壊までの時間の定数
    private const float DESTROY_TIME = 1.0f;
    
    //プレイヤーの情報を取得するための変数
    private GameObject player;
    //プレイヤーの向きを取得するための変数
    private SpriteRenderer playerDirection;
    //プレイヤーの向きを判断するためのフラグを用意
    private bool determineDirection;
    //弾の向きを取得
    [SerializeField] private SpriteRenderer direction;


    // Start is called before the first frame update
    void Start()
    {
        //SPEEDを秒速に
        speed *= Time.deltaTime;

        //プレイヤーの情報を取得
        player = GameObject.FindWithTag("Player");
        //プレイヤーのSpriteRendererを取得
        playerDirection = player.GetComponent<SpriteRenderer>();

        //プレイヤ―の向きと弾の向きを同じに
        changeBulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //timerを加算
        timer += Time.deltaTime;

        //timerがDESTROY_TIMEを超えているかを判断
        if (timer >= DESTROY_TIME)
        {
            //弾をDestroyする
            Destroy(gameObject);

            //タイマーリセット
            timer = 0.0f;
        }

        //弾の向きによって移動方向を変更
        setPlayerBulletVelocityFromDirection();

    }

    //プレイヤーの向きに合わせて弾の向きを変える関数
    private void changeBulletDirection()
    { 
        //プレイヤーが右向きの場合
        if (!playerDirection.flipX)
        {
            //プレイヤーの向きフラグに右向き(true)をいれる
            determineDirection = true;
        }
        //プレイヤーが左向きの場合
        else
        {
            //プレイヤーの向きフラグに左向き(false)をいれる
            determineDirection = false;

            //弾も左向きに
            direction.flipX = false;
        }
    }

    //弾の向きに合わせて移動方向を変更する関数
    private void setPlayerBulletVelocityFromDirection()
    {
        //プレイヤーが右向きの時
        if (determineDirection)
        {
            //正の方向すすむ
            transform.Translate(speed, 0.0f, 0.0f);
        }
        //プレイヤーが左向きの時
        else
        {
            //負の方向にすすむ
            transform.Translate(-speed, 0.0f, 0.0f);
        }
    }

}

