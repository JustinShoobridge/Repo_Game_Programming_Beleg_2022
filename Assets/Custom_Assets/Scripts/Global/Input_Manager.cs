using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public Vector3 _LookInput { get; private set; } = Vector2.zero;
    public Vector2 _MoveInput { get; private set; } = Vector2.zero;

    public bool InvertMouseY { get; private set; } = true;

    public Character_Controls _Character_Controls;
    public bool _Move_Is_Pressed = false;
    public bool _Sprint_Is_Pressed = false;

    public event Action _OnMouseDown;


    private void OnEnable()
    {
        _Character_Controls = new Character_Controls();
        _Character_Controls.NormalMovement.Enable();

        _Character_Controls.NormalMovement.Move.performed += SetMove;
        _Character_Controls.NormalMovement.Move.canceled += SetMove;

        _Character_Controls.NormalMovement.Look.performed += SetLook;
        _Character_Controls.NormalMovement.Look.canceled += SetLook;

        _Character_Controls.NormalMovement.Sprint.performed += SetSprint;
        _Character_Controls.NormalMovement.Sprint.canceled += SetSprint;

        _Character_Controls.NormalMovement.Mouse_Actions.started += OnMouseDown;
    }

    private void SetSprint(InputAction.CallbackContext ctx)
    {
        if(_Sprint_Is_Pressed == false )
        {
            _Sprint_Is_Pressed = true;
        }
        else
        {
            _Sprint_Is_Pressed = false;
        }
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {  
        _MoveInput = ctx.ReadValue<Vector2>();
        _Move_Is_Pressed = !(_MoveInput == Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx) => _LookInput = ctx.ReadValue<Vector2>();


    private void OnDisable()
    {
        _Character_Controls.NormalMovement.Move.performed -= SetMove;
        _Character_Controls.NormalMovement.Move.canceled -= SetMove;

        _Character_Controls.NormalMovement.Look.performed -= SetLook;
        _Character_Controls.NormalMovement.Look.canceled -= SetLook;

        _Character_Controls.NormalMovement.Disable();
    }

    private void OnMouseDown(InputAction.CallbackContext ctx)
    {
        _OnMouseDown?.Invoke();
    }





}
