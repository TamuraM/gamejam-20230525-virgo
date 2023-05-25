using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�v���C���[�����͂��󂯂č��E�ړ�����</summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Rigidbody2D _rb = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rb.velocity.y); 
    }

}
