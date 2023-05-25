using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>�v���C���[�����V���鏈��</summary>
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
        
        //�X�y�[�X�L�[�ŕ��V�ƕ��V����
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isFlying = !_isFlying;
        }

        if(_stamina <= 0) //�X�^�~�i���Ȃ��������ׂȂ�
        {
            _isFlying = false;
        }

        if(_isFlying) //���ł���X�^�~�i���炵�Ĕ��ł�
        {
            _stamina -= Time.deltaTime;
            _rb.velocity = new Vector2(_rb.velocity.x, _flyPower);
        }
        else //���łȂ�������X�^�~�i�񕜂��ė����Ă���
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

    /// <summary>�����̓����蔻��</summary>
    /// <param name="collision">���������I�u�W�F�N�g</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGround = true;
    }

    /// <summary>�����̓����蔻��</summary>
    /// <param name="collision">���������I�u�W�F�N�g</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGround = false;
    }

    /// <summary>�{�̂̓����蔻��</summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

}
