using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float WalkingSpeed = 1.5f;
    public float RunningSpeed = 3.5f;

    private Vector2 _movementV2 = Vector2.zero;
    private float _facing = 0f; // Facing: 0 - down (0; -1); 1 - left (-1; 0); 2 - up (0; 1); 3 - right (1; 0); default - 0;
    private bool _isRunning = false; // true if currently moving and MovementModifier key pressed 
    private float _speedAspect; // gets value of WalkingSpeed or RunningSpeed depending on isRunning variable

    private Rigidbody2D _playerRigidbody;
    private PlayerInputActions _playerInput;
    private Animator _animator;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();

        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();

        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        _movementV2 = _playerInput.Player.Movement.ReadValue<Vector2>();
        _animator.SetFloat("Speed", _movementV2.sqrMagnitude);

        _isRunning = !(_playerInput.Player.MovementModifier.IsPressed() && _playerInput.Player.Movement.IsPressed());
        _animator.SetBool("IsRunning", _isRunning);

        _speedAspect = _isRunning ? RunningSpeed : WalkingSpeed;
        _animator.speed = _isRunning ? 1.5f : 1f;

        // This block won't be updated with empty (0; 0) movement vector 
        if (_movementV2.SqrMagnitude() != 0f)
        {
            // Facing: 0 - down (0; -1); 1 - left (-1; 0); 2 - up (0; 1); 3 - right (1; 0); default - 0;
            if (_movementV2.x < 0) { _facing = 1; }
            else if (_movementV2.x > 0) { _facing = 3; }
            else if (_movementV2.y < 0) { _facing = 0; }
            else if (_movementV2.y > 0) { _facing = 2; }
            else { _facing = 0; }
            _animator.SetFloat("Facing", _facing);
        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        _playerRigidbody.MovePosition(_playerRigidbody.position + _movementV2 * _speedAspect * Time.fixedDeltaTime);
    }
}
