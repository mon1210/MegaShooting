using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CircularSawController : MonoBehaviour
{
    //弾の進行具合で引数に入れるための変数
    private float timer = 0.0f;

    //ベジェ曲線挙動用
    private Vector2 startPos;       //最初の位置
    private Vector2 relayPos;       //中継地点
    private Vector2 destinationPos; //目標地点
    
    //小数第何位で偶数丸めすればいいかを定める変数
    private int roundingDigits = 3;

    //座標
    private float posX; //X
    private float posY; //Y

    //ラジアン単位での回転を表す変数
    private float rotateRad = 0.0f;
    //限界の回転角度の定数
    const float LIMID_ROTATERAD = 2.0f * Mathf.PI / 180.0f;

    //速度を表す変数
    [SerializeField] private float speed;

    //オブジェクト取得のための変数
    private GameObject player;
    private GameObject bat;
    private GameObject circularSaw;

    //SpriteRendererを取得するための変数
    private SpriteRenderer batRenderer;
    private SpriteRenderer circularSawRenderer;

    //画像サイズの情報を保存する変数
    private float batScaleHalf;
    private float circularSawScaleHalf;

    //行きと帰りの切り替えを判断するフラグ
    private bool isReturning = true;


    void Start()
    {
        //オブジェクト取得
        player = GameObject.FindWithTag("Player");
        bat = GameObject.FindWithTag("Bat");
        circularSaw = GameObject.FindWithTag("CircularSaw");

        //SpriteRendererを取得
        batRenderer = bat.GetComponent<SpriteRenderer>();
        circularSawRenderer = circularSaw.GetComponent<SpriteRenderer>();

        //画像のピクセルサイズを取得(正方形なのでxのみ取得)
        var batWidth = batRenderer.bounds.size.x;
        var circularSawWidth = circularSawRenderer.bounds.size.x;
           

        //初期化処理
        timer = 0.0f;
        speed *= Time.deltaTime;
        startPos = this.transform.position;
        destinationPos = player.transform.position;
        relayPos = this.transform.position + player.transform.position;
        relayPos.y = 1.0f;
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        //半径を求める
        batScaleHalf = batWidth / 2;
        circularSawScaleHalf = circularSawWidth / 2;


    }

    void Update()
    {
        //目標地点に到達していないとき
        if(isReturning)
        {
            //ベジェ曲線を使って目標に向かう
            moveBulletWithBezierCurve();
        }
        //目標地点に到達したとき
        else
        {
            //目標地点から帰ってくる
            bulletReturnMove(batScaleHalf, circularSawScaleHalf);

        }

    }

    //ベジェ曲線を描いて目標へ向かう関数
    private void moveBulletWithBezierCurve()
    {
        //弾の進行具合（Lerpの第三引数に入れる）
        timer += Time.deltaTime;

        //二次ベジェ曲線を使い、座標を変更 -------------------------

        //スタート位置から中継地点の中間ベクトルをtimerの割合で算出
        Vector3 firstVec = Vector3.Lerp(startPos, relayPos, timer);
        //中継地点からターゲットの位置
        Vector3 SecondVec = Vector3.Lerp(relayPos, destinationPos, timer);

        //上の二点をつなぐ中間ベクトルをtimerの割合で算出
        Vector3 vec = Vector3.Lerp(firstVec, SecondVec, timer);

        //座標変更
        this.transform.position = vec;


        //Vector2型の変数を少数第3位まで偶数丸めをする => float型の値が同じにならないから
        Vector2 roundedDestinationPos = roundVector(destinationPos, roundingDigits);
        Vector2 roundedVec = roundVector(vec, roundingDigits);

        //目標地点と弾の位置が一致する場合
        if (roundedDestinationPos == roundedVec)
        {
            //到達フラグを反転
            isReturning = !isReturning;

            //到達した地点をReturn時の初期地点に
            posX = roundedDestinationPos.x;
            posY = roundedDestinationPos.y;

            timer = 0.0f;

        }
        
    }

    //目標にたどり着いた後に、Batを追尾し帰ってくる関数
    private void bulletReturnMove(float getBatScale,float getCircularSawScale)
    {
        //向きベクトルを決める
        float VecX = Mathf.Cos(this.rotateRad);
        float VecY = Mathf.Sin(this.rotateRad);

        //弾→ボスへのベクトルを求める
        float SawtoTargetX = bat.transform.position.x - this.posX;
        float SawtoTargetY = bat.transform.position.y - this.posY;
        //大きさを求める
        float SawtoTargetNorm = Mathf.Sqrt(SawtoTargetX * SawtoTargetX + SawtoTargetY * SawtoTargetY);

        //当たっていなければ(Batに帰ってきていなければ)
        if (SawtoTargetNorm > (getBatScale + getCircularSawScale))
        {
            //単位ベクトル化
            SawtoTargetX /= SawtoTargetNorm;
            SawtoTargetY /= SawtoTargetNorm;

            //実際に回転する角度
            float turnRad = LIMID_ROTATERAD;
            // 内積計算
            //必要な回転角
            float costheta = VecX * SawtoTargetX + VecY * SawtoTargetY;

            //最大旋回角度より外側にいるとき
            if (costheta > Mathf.Cos(LIMID_ROTATERAD))
            {
                if (costheta <= 1.0f)
                    turnRad = Mathf.Acos(costheta);
                else
                    turnRad = 0.0f;
            }

            //外積の計算
            float crossZ = VecX * SawtoTargetY - SawtoTargetX * VecY;
            //外積がマイナス(向いて左側に目標がある)時
            if (crossZ < 0.0f)
            {
                //左に回転
                turnRad *= -1;
            }
            //回転角の加算(正面が0)
            this.rotateRad += turnRad;


        }
        //Batに当たれば消える
        else
        {
            Destroy(gameObject);
        }

        //座標の計算
        this.posX += Mathf.Cos(this.rotateRad) * speed;
        this.posY += Mathf.Sin(this.rotateRad) * speed;

        //座標の更新
        Vector3 pos = this.transform.position;
        pos.x = this.posX;
        pos.y = this.posY;

        this.transform.position = pos;

    }

    //Vector2型の変数を指定した少数点で偶数丸めして返す関数 => float型の値が同じにならないから
    private Vector2 roundVector(Vector2 vector,int decimalPlaces)
    {
        //10の decimalPlaces 乗を計算
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        //指定した小数桁数に合わせて偶数丸めするために、(vector.x(y) * multiplier) / multiplier)を計算
        return new Vector2(Mathf.Round(vector.x * multiplier) / multiplier, Mathf.Round(vector.y * multiplier) / multiplier);
    }
}
