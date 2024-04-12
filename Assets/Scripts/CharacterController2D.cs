using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Header("Input settings:")]
    [SerializeField] private float speedMultiplier = 5.0f;
    
    [Space]
    [Header("Character Stats:")]
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed;

    [Space]
    [Header("References:")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movementSpeed = Mathf.Clamp(_moveInput.magnitude, 0.0f, 1.0f);
        _moveInput.Normalize();

        HandleMove();
        HandleAnimation();

        //Sets the idle to the last direction moved
        if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f || Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
        {
            _animator.SetFloat("LastMoveX", Input.GetAxis("Horizontal"));
            _animator.SetFloat("LastMoveY", Input.GetAxis("Vertical"));
        }
    }

    

    private void HandleMove()
    {
        _rb.MovePosition(_rb.position + _moveInput * speedMultiplier * Time.fixedDeltaTime);
    }

    private void HandleAnimation()
    {
        _animator.SetFloat("Horizontal", _moveInput.x);
        _animator.SetFloat("Vertical", _moveInput.y);
        _animator.SetFloat("Speed", _movementSpeed);
    }

    public void Freeze()
    {
        _movementSpeed = 0.0f;
        HandleAnimation();
    }
}
