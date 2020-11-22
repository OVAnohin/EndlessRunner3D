using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedDown;
    [SerializeField] private float _speedHorizontal;

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
            _rigidbody.velocity += Vector3.left * _speedHorizontal;

        if (Input.GetKey(KeyCode.RightArrow)) 
            _rigidbody.velocity += Vector3.right * _speedHorizontal;
    }
}
