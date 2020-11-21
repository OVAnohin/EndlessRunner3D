using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) _rigidbody.velocity += Vector3.left * _speed;
        if (Input.GetKey(KeyCode.RightArrow)) _rigidbody.velocity += Vector3.right * _speed;
        if (Input.GetKey(KeyCode.UpArrow)) _rigidbody.velocity += Vector3.forward * _speed;
        if (Input.GetKey(KeyCode.DownArrow)) _rigidbody.velocity += Vector3.back * _speed;
    }
}
