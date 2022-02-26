using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    private float _horizontal, _vertical;
    Vector3 _direction;

    void Update()
    {
        Move();
    }

    void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(-_horizontal, 0, -_vertical).normalized;

        if (_direction.magnitude > 0.1f)
            _characterController.Move(_direction * _speed * Time.deltaTime);
    }
}
