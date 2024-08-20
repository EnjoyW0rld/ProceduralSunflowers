using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Rigidbody _characterRB;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _maxZRotation;
    private float _inputMouseX, _inputMouseY;
    private float _horizontalMovement;
    private Vector3 _rotation;
    private float _yOffset;
    private float _zRotation;
    private bool _isMoving;
    private float _time;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (_characterTransform == null) Debug.LogError("Did not specify character transform for CameraMovement");
    }
    private void Update()
    {
        ReadMouseInput();
        ReadKeyboardInput();
        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);
        CameraOffsetHandler();
    }

    private void CameraOffsetHandler()
    {
        if (_isMoving)
        {
            _time += Time.deltaTime;
            _time = _time > 1 ? _time - 1 : _time;
            _yOffset = _animationCurve.Evaluate(_time);
        }
        //_zRotation = _horizontalMovement * -_maxZRotation;
        _zRotation = _zRotation * .9f + _horizontalMovement * -_maxZRotation * .1f;
    }

    private void ReadMouseInput()
    {
        _inputMouseX = Input.GetAxis("Mouse X");
        _inputMouseY = Input.GetAxis("Mouse Y");
        if (_inputMouseX != 0)
        {
            _rotation.y += _inputMouseX;
        }
        if (_inputMouseY != 0)
        {
            _rotation.x -= _inputMouseY;
        }
        if (_rotation.y > 360) _rotation.y -= 360;
    }
    private void ReadKeyboardInput()
    {
        _isMoving = false;
        _horizontalMovement = Input.GetAxis("Horizontal");
        if (_horizontalMovement != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            _isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        //_characterTransform.rotation = Quaternion.Euler(_characterTransform.rotation.x, _rotation.y, _characterTransform.rotation.z);
        _characterRB.rotation = Quaternion.Euler(_characterRB.rotation.x, _rotation.y, _characterRB.rotation.z);

    }
    private void LateUpdate()
    {
        transform.position = _characterTransform.position + new Vector3(0, _yOffset, 0);
        transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, _zRotation);
    }
}
