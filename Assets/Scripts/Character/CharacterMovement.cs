using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody _rb;
    private float _horizontal, _vertical;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        //_rb.MovePosition(_rb.position + (transform.forward * _vertical + transform.right * _horizontal) * _movementSpeed);
        _rb.velocity = (transform.forward * _vertical + transform.right * _horizontal) * _movementSpeed;
    }
}
