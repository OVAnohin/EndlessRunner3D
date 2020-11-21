using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime = 1f;

    private void Update()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime > 0)
            return;

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
