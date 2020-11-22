using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerRemoveWall))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedDown;
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private RuntimeAnimatorController[] _animators;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _whatIsWall;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Touch _touch;
    private PlayerRemoveWall _playerRemoveWall;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerRemoveWall = GetComponent<PlayerRemoveWall>();
        _startPosition = transform.position;
    }

    public void Restart()
    {
        transform.position = _startPosition;
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity += Vector3.back * _speedDown;

        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(i).tapCount == 2)
                    {
                        _playerRemoveWall.TryRemoveWall();
                    }
                }
            }

            _touch = Input.GetTouch(0);
            int touchX = Screen.width / 2;

            if (_touch.phase == TouchPhase.Stationary && touchX > _touch.position.x)
            {
                _animator.runtimeAnimatorController = _animators[2];
                _rigidbody.velocity += Vector3.left * _speedHorizontal;
            }
            else if (_touch.phase == TouchPhase.Stationary && touchX < _touch.position.x)
            {
                _animator.runtimeAnimatorController = _animators[1];
                _rigidbody.velocity += Vector3.right * _speedHorizontal;
            }
        }
        else
        {
            bool isCloseToWall = Physics.Raycast(transform.position, Vector3.back, 2f, _whatIsWall);

            if (isCloseToWall)
                _animator.runtimeAnimatorController = _animators[3];
            else
                _animator.runtimeAnimatorController = _animators[0];
        }
    }
}
