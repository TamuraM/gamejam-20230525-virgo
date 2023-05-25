using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>プレイヤーが浮遊する処理</summary>
public class PlayerFly : MonoBehaviour
{
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private bool _isFlying = false;
    private Rigidbody2D _rb = default;
    [SerializeField] private float _flyPower = 4;
    [SerializeField] private bool _isGround = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        //スペースキーで浮遊と浮遊解除
        if(Input.GetKeyDown(KeyCode.Space))
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
            _rb.velocity = new Vector2(_rb.velocity.x, _flyPower);
        }
        else //飛んでなかったらスタミナ回復して落ちていく
        {
            _stamina += Time.deltaTime;

            if(_isGround)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_flyPower);
            }

        }

    }

    /// <summary>足元の当たり判定</summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGround = true;
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

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

}
