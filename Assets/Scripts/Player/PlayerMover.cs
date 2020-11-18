using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Move(float horizontal, float vertical)
    {
        transform.Translate(horizontal * _speed * Time.deltaTime, vertical * _speed * Time.deltaTime, 0);
    }
}
