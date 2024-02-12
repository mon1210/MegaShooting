using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //アニメーションを取得
    [SerializeField] private Animator animator;

    //移動速度の定数
    [SerializeField] private float speed;
    //ジャンプ力の定数
    [SerializeField] private float jump;

    //プレイヤーの弾を取得
    [SerializeField] private GameObject playerBulletPrefab = null;
    //HitPointのUIオブジェクトを取得
    [SerializeField] private GameObject hitPointUI = null;

    //射撃時のSEを取得
    [SerializeField] private AudioClip se_Beam;
    //ジャンプ時のSEを取得
    [SerializeField] private AudioClip se_Jump;

    // ジャンプフラグ
    [SerializeField] private bool isJumping = false;
    public void SetisJumping(bool val) { this.isJumping = val; }

    // 勝利フラグ
    [SerializeField] private bool isWin = false;
    public bool GetisWin() { return this.isWin; }
    public void SetisWin(bool val) { this.isWin = val;}

    // HP
    [SerializeField] private int hitPoint = 0;
    public int GetHitPoint() { return this.hitPoint; }
    public void SetHitPoint(int hitPoint) { this.hitPoint = hitPoint; }

    //アニメーション切り替え用の変数を定義・初期化
    private bool isMoving;
    private bool isShooting;
    private bool isLose;

    //Rigidbody2Dを取得
    [SerializeField] private Rigidbody2D rb;
    //SpriteRendererを取得
    [SerializeField] private SpriteRenderer direction;

    //Phaseの設定
    private enum PlayerPhase
    {
        Idle,   //idle
        Jump,   //ジャンプ
        LMove,  //左移動
        RMove,  //右移動
        Shoot,  //射撃
        Win,    //勝利
        Lose    //敗北
    }
    //PlayerPhase型の変数phaseを初期化
    private PlayerPhase phase = PlayerPhase.Idle;


    // Start is called before the first frame update
    void Start()
    {
        //hitPointのUIを初期化
        hitPointUI.GetComponent<HitpointController>().UpdatePlayerHpUI(hitPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //phaseを更新
        updatePlayerPhase();

        //各phaseでの処理を場合分け
        processPlayerPhase();

        //bool型の変数にアニメーションを設定
        setAnimationStates();

    }

    //プレイヤーのphaseを更新する関数
    private void updatePlayerPhase()
    {
        /***** ジャンプ処理 *****/
        checkAndPerformJump();

        /***** 移動処理 *****/
        //左移動
        checkAndPerformLeftMove();
        //右移動
        checkAndPerformRightMove();
      
        /***** 射撃処理 *****/
        checkAndPerformShoot();

        /***** 敗北処理 *****/
        checkAndPerformLose();

        /**** 勝利処理 *****/
        checkAndPerformWin();
        
    }

    //各phaseでの処理を場合分けする関数
    private void processPlayerPhase()
    {
        switch (phase)
        {
            //Idle時
            case PlayerPhase.Idle:
                    //Idle状態の処理関数を飛び出し
                    performIdle();
                break;
            //左移動時
            case PlayerPhase.LMove:
                if (!isShooting && !isLose && !isWin)
                {
                    //左移動時の処理関数を呼び出し
                    performLeftMove();
                }
                break;
            //右移動時
            case PlayerPhase.RMove:
                if (!isShooting && !isLose && !isWin)
                {
                    //右移動時の処理関数を呼び出し
                    performRightMove();
                }
                break;
            //ジャンプ時
            case PlayerPhase.Jump:
                if (!isJumping)
                {
                    //ジャンプ時の処理関数を呼び出し
                    performJump();
                }
                break;
            //射撃時
            case PlayerPhase.Shoot:
                if (!isMoving && !isShooting && !isJumping && !isLose && !isWin)
                {
                    //射撃時の処理関数を呼び出し
                    performShoot();
                }
                break;
            //敗北時
            case PlayerPhase.Lose:
                    //敗北時の処理関数を呼び出し
                    performLose();
                break;
            //勝利時
            case PlayerPhase.Win:
                break;

            default: break;
        }
    }

    //bool型のアニメーション変数を一括で設定する関数
    private void setAnimationStates()
    {
        //Bool型の変数にアニメーションをセット
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsShooting", isShooting);
        animator.SetBool("IsWin", isWin);
        animator.SetBool("IsLose", isLose);
    }

    //ジャンプを実行するための条件をチェックする関数
    private void checkAndPerformJump()
    {
        //Spaceキーを押すとJump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            phase = PlayerPhase.Jump;
        }
        //Jumpのアニメーション終了時にIdle状態に
        else if (Input.GetKeyUp(KeyCode.Space) && isJumping) 
        {
            phase = PlayerPhase.Idle;
        }

    }

    //左移動を実行するための条件をチェックする関数
    private void checkAndPerformLeftMove()
    {
        //Aキー押している間左に移動
        if (Input.GetKey(KeyCode.A))
        {
            phase = PlayerPhase.LMove;
        }
        //Aキーを離すとIdle状態に
        else if (Input.GetKeyUp(KeyCode.A) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

        //移動キー左右同時押しするとIdle状態に
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.Idle;
        }

    }

    //右移動を実行するための条件をチェックする関数
    private void checkAndPerformRightMove()
    {
        //Dキー押している間右に移動
        if (Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.RMove;
        }
        //Dキーを離すとIdle状態に
        else if (Input.GetKeyUp(KeyCode.D) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

        //移動キー左右同時押しするとIdle状態に
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            phase = PlayerPhase.Idle;
        }

    }

    //射撃を実行するための条件をチェックする関数
    private void checkAndPerformShoot()
    {
        //左クリックで弾を発射
        if (Input.GetMouseButton(0))
        {
            phase = PlayerPhase.Shoot;
        }
        //クリックを離すとIdle状態に
        else if (Input.GetMouseButtonUp(0) && !isJumping)
        {
            phase = PlayerPhase.Idle;
        }

    }

    //敗北を実行するための条件をチェックする関数
    private void checkAndPerformLose()
    {
        //hitPointが0になると敗北
        if (hitPoint <= 0)
        {
            phase = PlayerPhase.Lose;
        }

    }

    //勝利を実行するための条件をチェックする関数
    private void checkAndPerformWin()
    {
        //isWin = true で勝利
        if (isWin)
        {
            phase = PlayerPhase.Win;
        }

    }

    // 以下 状態処理 =======================================================
    //Idle状態の処理関数
    private void performIdle()
    {
        //アニメーションの停止
        isJumping = false;
        isMoving = false;
        isShooting = false;
    }

    //ジャンプの処理関数
    private void performJump()
    {
        //ジャンプのアニメーションを再生
        isJumping = true;

        //変数jumpを使って飛ぶ力を作成
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

        //ジャンプ時にSEを再生
        SoundFactoryController.instance.PlaySE(se_Jump);
    }

    //左移動の処理関数
    private void performLeftMove()
    {
        //走るアニメーションを再生
        isMoving = true;

        //変数speedを使って左向きに移動
        transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

        //向きを変更していなければ
        if (!direction.flipX) 
        {
            //プレイヤーの向きを左向きに変更
            direction.flipX = true;
        }
        
    }

    //右移動の処理関数
    private void performRightMove()
    {
        //走るアニメーションを再生
        isMoving = true;

        //変数speedを使って右向きに移動
        transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);

        //向きを変更していなければ
        if(direction.flipX)
        {
            //プレイヤーの向きを右向きに変更
            direction.flipX = false;
        }
      
    }

    //射撃の処理関数
    private void performShoot()
    {
        //射撃のアニメーションを再生
        isShooting = true;

        //自分の位置に弾を生成
        Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);

        //射撃時にSEを再生
        SoundFactoryController.instance.PlaySE(se_Beam);
    }

    //敗北の処理関数
    private void performLose()
    {
        //敗北時のアニメーションを再生
        isLose = true;
    }

    // 以上 状態処理 =======================================================
}
