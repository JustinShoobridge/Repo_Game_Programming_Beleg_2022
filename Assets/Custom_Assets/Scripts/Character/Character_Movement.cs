using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public event Action _on_Start_Moving;
    public event Action _on_Stop_Moving;


    [Header("Movement")]
    [SerializeField] float _movementMultiplier = 50.0f;
    [SerializeField] private Input_Manager _InputManager;
    private Rigidbody _Rigidbody = null;
    
    public Vector2 _MoveInput { get; private set; } = Vector2.zero;
    Vector3 _playerMoveInput = Vector3.zero;

    [Header("Camera")]
    [SerializeField] private Transform _CameraFollow;
    [SerializeField] private float _CameraClampX = -40.0f;
    [SerializeField] private float _CameraClampY = 40.0f;

    [SerializeField] private float _playerLookInputLerpTime = 0.35f;
    [SerializeField] private float _rotationSpeedMultiplier = 180.0f;
    [SerializeField] private float _PitchSpeedMultiplier = 180.0f;
    
    private Vector3 _PlayerLookInput = Vector3.zero;
    private Vector3 _previousPlayerLookInput = Vector3.zero;
    [SerializeField] float _cameraPitch = 0.0f;
    
    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        _PlayerLookInput = GetLookInput();
        PlayerLook();
        PitchCamera();

        _playerMoveInput = GetMoveInput();
        PlayerMove();

        _Rigidbody.AddRelativeForce(_playerMoveInput, ForceMode.Force);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(_InputManager._MoveInput.x, 0.0f, _InputManager._MoveInput.y);
        if (_playerMoveInput != Vector3.zero) _on_Start_Moving?.Invoke(); else _on_Stop_Moving?.Invoke();

    }

    private void PlayerMove()
    {
        _playerMoveInput = (new Vector3(_playerMoveInput.x * _movementMultiplier * _Rigidbody.mass, _playerMoveInput.y, _playerMoveInput.z * _movementMultiplier * _Rigidbody.mass));
    }

    private Vector3 GetLookInput()
    {
        _previousPlayerLookInput = _PlayerLookInput;
        _PlayerLookInput = new Vector3(_InputManager._LookInput.x, (_InputManager.InvertMouseY ? -_InputManager._LookInput.y : _InputManager._LookInput.y), 0.0f);
        return Vector3.Lerp(_previousPlayerLookInput, _PlayerLookInput * Time.deltaTime, _playerLookInputLerpTime);
    }
    private void PlayerLook()
    {
        _Rigidbody.rotation = Quaternion.Euler(0.0f, _Rigidbody.rotation.eulerAngles.y + (_PlayerLookInput.x * _rotationSpeedMultiplier), 0.0f);
    }
    private void PitchCamera()
    {
        _cameraPitch += _PlayerLookInput.y * _PitchSpeedMultiplier;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _CameraClampX, _CameraClampY);

        _CameraFollow.rotation = Quaternion.Euler(_cameraPitch, _CameraFollow.rotation.eulerAngles.y, _CameraFollow.rotation.eulerAngles.z);
    }

    private void Start()
    {
        _cameraPitch = 0.0f;
    }


}
