using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    //移動速度の定数
    [SerializeField] private float speed;

    //Batの弾を取得
    [SerializeField] private GameObject circularSawPrefab = null;

    //BatのHitPointを定義
    [SerializeField] private int hitPoint;

    //時間計測のための変数
    private float timer = 0.0f;

    //動きの間隔の定数
    private const float MOVE_INTERVAL = 1.0f;

    //ランダムな移動距離をいれるための変数
    private int randomDistanceX;
    private int randomDistanceY;

    //動きの確率を表す定数
    private const int MOVEMENT_PROBABILITY = 90;
    //弾を発射する確率を表す定数
    private const int SHOT_PROBABILITY = 60;

    //X軸に対する移動距離の最小値
    const int MIN_DISTANCE_X = -100;
    //X軸に対する移動距離の最大値
    const int MAX_DISTANCE_X = 100;
    //Y軸に対する移動距離の最小値
    const int MIN_DISTANCE_Y = -50;
    //Y軸に対する移動距離の最大値
    const int MAX_DISTANCE_Y = 50;

    //割合の最小値
    const int MIN_VALUE = 1;
    //割合の最大値
    const int MAX_VALUE = 100;

    //Rigidbody2Dを取得
    [SerializeField] private Rigidbody2D rb;
    //SpriteRendererを取得
    [SerializeField] private SpriteRenderer direction;


    //BatのHitPointを他クラスで取得できるように
    public int GetBatHp() { return hitPoint; }
    //BatのHitPointを他クラスで変更できるように
    public void SetBatHp(int val) { hitPoint = val; }

    void Update()
    {
        //timerを加算
        timer += Time.deltaTime;

        //MOVE_INTERVALごとに動きを出す
        if (timer >= MOVE_INTERVAL) 
        {
            //挙動の関数の呼び出し
            moving();
            //タイマーリセット
            timer = 0.0f;
        }
        

    }

    //Batの挙動を定めた関数
    private void moving()
    {
        //移動の距離をランダムに
        randomDistanceX = Random.Range(MIN_DISTANCE_X, MAX_DISTANCE_X);
        randomDistanceY = Random.Range(MIN_DISTANCE_Y, MAX_DISTANCE_Y);

        //動きの割合
        int value = Random.Range(MIN_VALUE, MAX_VALUE);
        //90%の確率
        if (value <= MOVEMENT_PROBABILITY)
        {
            //ランダムに定められた距離*speedで移動
            rb.AddForce(new Vector2(randomDistanceX * speed, randomDistanceY * speed));
        }
        //60%の確率
        if (value <= SHOT_PROBABILITY)
        {
            //CircularSawを発射する
            shootCircularSaw();
        }

        //Batの向きを変更
        changeBatDirection();
    }

    //CircularSawを打つ関数
    private void shootCircularSaw()
    {
        //Batの位置にCircularSawが出現
        Instantiate(circularSawPrefab, transform.position, Quaternion.identity);
    }

    //Batの向きを変えるための関数
    private void changeBatDirection()
    {
        //画面の中心より左側にいる時
        if (transform.position.x <= 0)
        {
            if (direction.flipX) 
            {
                //左向きに
                direction.flipX = false;
            }
           
        }
        //画面の中心より右側にいる時
        else
        {
            if (!direction.flipX) 
            {
                //右向きに
                direction.flipX = true;
            }
           
        }
    }
}
