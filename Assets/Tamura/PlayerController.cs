using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>�v���C���[�̓����֘A�̏���</summary>
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _sr = default;

    [Header("�v���C���[�̓����̃p�����[�^�[")]
    [SerializeField, Header("�v���C���[�̍��E�ړ��̃X�s�[�h")] private float _moveSpeed = 3f;

    [Header("�v���C���[�̕��V�̃p�����[�^�[")]
    [SerializeField, Header("���݂̃X�^�~�i�l")] private float _stamina = 100f;
    /// <summary>���݂̃X�^�~�i�l</summary>
    public float Stamina => _stamina;
    [SerializeField, Header("�X�^�~�i�̍ő�l")] private float _maxStamina = 100f;
    /// <summary>�X�^�~�i�̍ő�l</summary>
    public float MaxStamina => _maxStamina;
    [SerializeField, Header("���V�E���~�̃X�s�[�h")] private float _flyPower = 4;
    [SerializeField, Header("�G�𓥂񂾂Ƃ��ɉ񕜂���X�^�~�i�̗�")] private float _recoveryPoint = 30f;

    [Header("�v���C���[�̏�Ԃ̃p�����[�^�[")]
    [SerializeField, Header("���ł��邩�ǂ���")] private bool _isFlying = false;
    [SerializeField, Header("�n�ʂɂ��Ă��邩�ǂ���")] private bool _isGround = true;
    private bool _isTired = false;
    private float _timer = 0;
    [SerializeField, Header("���V���������Ă��牽�b��ɂ܂����V�ł��邩")] private float _flyInterval = 5;

    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField, Header("��񂾎��̉�")] private AudioClip _jump = default;
    [SerializeField, Header("�G�𓥂񂾎��̉�")] private AudioClip _fumu = default;
    [SerializeField, Header("�Ȃɂ��ɓ����������̉�")] private AudioClip _do = default;

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

        //�X�y�[�X�L�[�ŕ��V�ƕ��V����
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

        if (_stamina <= 0) //�X�^�~�i���Ȃ��������ׂȂ�
        {
            _isFlying = false;
        }

        if (_isFlying) //���ł���X�^�~�i���炵�Ĕ��ł�
        {
            _stamina -= Time.deltaTime;
            velocityY = _flyPower;
        }
        else //���łȂ�������X�^�~�i�񕜂��ė����Ă���
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

    /// <summary>�����̓����蔻��</summary>
    /// <param name="collision">���������I�u�W�F�N�g</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) //�G��������|���ĕ��V
        {
            enemy.Death();
            _audioSource.PlayOneShot(_fumu);
            _stamina += _recoveryPoint;
            _isFlying = true;
        }
        else if (collision.gameObject.TryGetComponent<Hole>(out Hole hole)) //�n�ʂ������玀��
        {
            hole.OnCollide();
        }
        else //�n�ʂ�������true
        {
            _isGround = true;
            _isTired = false;
            _timer = 0;
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

        if (collision.gameObject.TryGetComponent<IColliderable>(out IColliderable colliderable))
        {
            Debug.Log($"{colliderable}�ɓ�������");
            colliderable.OnCollide();
            _audioSource.PlayOneShot(_do);
        }

    }

}
