using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>プレイヤーの動き関連の処理</summary>
public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの動きのパラメーター")]
    [SerializeField, Header("プレイヤーの左右移動のスピード")] private float _moveSpeed = 3f;

    [Header("プレイヤーの浮遊のパラメーター")]
    [SerializeField, Header("今のスタミナ")] private float _stamina = 100f;
    public float Stamina => _stamina;
    [SerializeField, Header("スタミナの最大値")] private float _maxStamina = 100f;
    public float MaxStamina => _maxStamina;
    [SerializeField, Header("浮遊・下降のスピード")] private float _flyPower = 4;

    [Header("プレイヤーの状態のパラメーター")]
    [SerializeField, Header("飛んでいるかどうか")] private bool _isFlying = false;
    [SerializeField, Header("地面についているかどうか")] private bool _isGround = true;

    private Rigidbody2D _rb = default;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.State != GameState.Game) return;
        
        float velocityY = _rb.velocity.y;
        float velocityX = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        //スペースキーで浮遊と浮遊解除
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isFlying = !_isFlying;
        }

        if(_stamina <= 0) //スタミナがなかったら飛べない
        {
            _isFlying = false;
        }

        if(_isFlying) //飛んでたらスタミナ減らして飛んでく
        {
            _stamina -= Time.deltaTime;
            velocityY = _flyPower;
        }
        else //飛んでなかったらスタミナ回復して落ちていく
        {
            _stamina += Time.deltaTime;

            if (_stamina > _maxStamina) _stamina = _maxStamina;

            if(_isGround)
            {
                velocityY = 0;
            }
            else
            {
                velocityY = -_flyPower;
            }

        }

        _rb.velocity = new Vector2(velocityX, velocityY);
    }

    /// <summary>足元の当たり判定</summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(TryGetComponent<IColliderable>(out IColliderable colliderable)) //敵だったら倒して浮遊
        {
            colliderable.OnCollide();
            _stamina += 40f;
            _isFlying = true;
        }
        else //地面だったらtrue
        {
            _isGround = true;
        }
       
    }

    /// <summary>足元の当たり判定</summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGround = false;
    }

    /// <summary>本体の当たり判定</summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<IColliderable>(out IColliderable colliderable)) //敵だったら倒して浮遊
        {
            Debug.Log($"{colliderable}に当たった");
            colliderable.OnCollide();
        }

    }

}
