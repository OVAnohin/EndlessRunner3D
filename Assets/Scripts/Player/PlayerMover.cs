using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedDown;
    [SerializeField] private float _speedHorizontal;
    [SerializeField] AnimatorController[] _animators;
    [SerializeField] Animator _animator;
    [SerializeField] private LayerMask _whatIsWall;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _animator.runtimeAnimatorController = _animators[2];
            _rigidbody.velocity += Vector3.left * _speedHorizontal;
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _animator.runtimeAnimatorController = _animators[1];
            _rigidbody.velocity += Vector3.right * _speedHorizontal;
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
