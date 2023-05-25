using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�v���C���[�̓����֘A�̏���</summary>
public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̓����̃p�����[�^�[")]
    [SerializeField, Header("�v���C���[�̍��E�ړ��̃X�s�[�h")] private float _moveSpeed = 3f;

    [Header("�v���C���[�̕��V�̃p�����[�^�[")]
    [SerializeField, Header("���̃X�^�~�i")] private float _stamina = 100f;
    public float Stamina => _stamina;
    [SerializeField, Header("�X�^�~�i�̍ő�l")] private float _maxStamina = 100f;
    public float MaxStamina => _maxStamina;
    [SerializeField, Header("���V�E���~�̃X�s�[�h")] private float _flyPower = 4;

    [Header("�v���C���[�̏�Ԃ̃p�����[�^�[")]
    [SerializeField, Header("���ł��邩�ǂ���")] private bool _isFlying = false;
    [SerializeField, Header("�n�ʂɂ��Ă��邩�ǂ���")] private bool _isGround = true;

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

        //�X�y�[�X�L�[�ŕ��V�ƕ��V����
        if (Input.GetKeyDown(KeyCode.Space))
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
            velocityY = _flyPower;
        }
        else //���łȂ�������X�^�~�i�񕜂��ė����Ă���
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

    /// <summary>�����̓����蔻��</summary>
    /// <param name="collision">���������I�u�W�F�N�g</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(TryGetComponent<IColliderable>(out IColliderable colliderable)) //�G��������|���ĕ��V
        {
            colliderable.OnCollide();
            _stamina += 40f;
            _isFlying = true;
        }
        else //�n�ʂ�������true
        {
            _isGround = true;
        }
       
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

        if (collision.gameObject.TryGetComponent<IColliderable>(out IColliderable colliderable)) //�G��������|���ĕ��V
        {
            Debug.Log($"{colliderable}�ɓ�������");
            colliderable.OnCollide();
        }

    }

}
