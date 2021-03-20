using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    
    private PlayerController _playerController;
    private CharacterController _controller;
    
    void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _controller = transform.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        _animator.SetBool("isRunning", _playerController.IsRunning);
        _animator.SetBool("isJumping", !_playerController.IsGrounded);

        Vector3 direction = _playerController.Direction;
        direction = transform.right * -direction.x;
        
        if (direction.magnitude >= 0.1f)
        {
            _spriteRenderer.flipX = _playerController.Direction.x > 0;
            _controller.Move(direction * (_playerController.Speed * _playerController.RunningSpeed * Time.deltaTime));
        }
    }
}
