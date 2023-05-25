using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>プレイヤーの動き関連の処理</summary>
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _sr = default;

    [Header("プレイヤーの動きのパラメーター")]
    [SerializeField, Header("プレイヤーの左右移動のスピード")] private float _moveSpeed = 3f;

    [Header("プレイヤーの浮遊のパラメーター")]
    [SerializeField, Header("現在のスタミナ値")] private float _stamina = 100f;
    /// <summary>現在のスタミナ値</summary>
    public float Stamina => _stamina;
    [SerializeField, Header("スタミナの最大値")] private float _maxStamina = 100f;
    /// <summary>スタミナの最大値</summary>
    public float MaxStamina => _maxStamina;
    [SerializeField, Header("浮遊・下降のスピード")] private float _flyPower = 4;
    [SerializeField, Header("敵を踏んだときに回復するスタミナの量")] private float _recoveryPoint = 30f;

    [Header("プレイヤーの状態のパラメーター")]
    [SerializeField, Header("飛んでいるかどうか")] private bool _isFlying = false;
    [SerializeField, Header("地面についているかどうか")] private bool _isGround = true;
    private bool _isTired = false;
    private float _timer = 0;
    [SerializeField, Header("浮遊を解除してから何秒後にまた浮遊できるか")] private float _flyInterval = 5;

    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField, Header("飛んだ時の音")] private AudioClip _jump = default;
    [SerializeField, Header("敵を踏んだ時の音")] private AudioClip _fumu = default;
    [SerializeField, Header("なにかに当たった時の音")] private AudioClip _do = default;

    private Rigidbody2D _rb = default;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.instance.State != GameState.Game) return;

        float velocityY = _rb.velocity.y;
        float velocityX = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        if(_isTired)
        {
            _timer += Time.deltaTime;
            
            if(_timer > _flyInterval)
            {
                _isTired = false;
                _timer = 0;
            }

        }

        //スペースキーで浮遊と浮遊解除
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (_isFlying)
            {
                _isFlying = false;
                _isTired = true;
            }
            else if(!_isTired)
            {
                _isFlying = true;
            }

        }

        if (_stamina <= 0) //スタミナがなかったら飛べない
        {
            _isFlying = false;
        }

        if (_isFlying) //飛んでたらスタミナ減らして飛んでく
        {
            _stamina -= Time.deltaTime;
            velocityY = _flyPower;
        }
        else //飛んでなかったらスタミナ回復して落ちていく
        {
            _stamina += Time.deltaTime;

            if (_stamina > _maxStamina)
            {
                _stamina = _maxStamina;
            }

            if (_isGround)
            {
                velocityY = 0;
            }
            else
            {
                velocityY = -_flyPower;
            }

        }

        _rb.velocity = new Vector2(velocityX, velocityY);

        if (velocityX < 0) _sr.flipX = true;
        else _sr.flipX = false;
    }

    /// <summary>足元の当たり判定</summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) //敵だったら倒して浮遊
        {
            enemy.Death();
            _audioSource.PlayOneShot(_fumu);
            _stamina += _recoveryPoint;
            _isFlying = true;
        }
        else if (collision.gameObject.TryGetComponent<Hole>(out Hole hole)) //地面だったら死ぬ
        {
            hole.OnCollide();
        }
        else //地面だったらtrue
        {
            _isGround = true;
            _isTired = false;
            _timer = 0;
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

        if (collision.gameObject.TryGetComponent<IColliderable>(out IColliderable colliderable))
        {
            Debug.Log($"{colliderable}に当たった");
            colliderable.OnCollide();
            _audioSource.PlayOneShot(_do);
        }

    }

}
