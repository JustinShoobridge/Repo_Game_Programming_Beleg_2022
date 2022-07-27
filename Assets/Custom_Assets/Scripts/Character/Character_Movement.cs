using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    [SerializeField] private Transform _PlayerCamera;
    [SerializeField] private Rigidbody _Character_Rigidbody;
    [SerializeField] private BoxCollider _BoxCollider;

    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _runSpeed = 10;
    [SerializeField] private float _verticatRotation;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _jump_Cooldown;
    [SerializeField] private bool  _isGrounded = true;
    [SerializeField] private bool  _isSprinting = false;

    private Vector3 _moveDirection;
    private CharacterController _controller;

    void Start()
    {
        _Character_Rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        applyMovement();
        applyCameraMovment();
    }
    private void applyMovement()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * _walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Test");
            MoveVector = transform.TransformDirection(PlayerMovementInput) * _runSpeed;
        }

        _Character_Rigidbody.velocity = new Vector3(MoveVector.x, _Character_Rigidbody.velocity.y, MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _isGrounded = false;
            _Character_Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void applyCameraMovment()
    {
        _verticatRotation -= PlayerMouseInput.y * _sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * _sensitivity, 0f);

        if(_verticatRotation < 20 && _verticatRotation > -20)
        {
            _PlayerCamera.transform.localRotation = Quaternion.Euler(_verticatRotation, 0f, 0f);
        }
        
    }

    private void OnTriggerEnter(Collider other) => _isGrounded = true;
}
