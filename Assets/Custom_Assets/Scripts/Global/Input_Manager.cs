using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public Vector3 _LookInput { get; private set; } = Vector2.zero;
    public Vector2 _MoveInput { get; private set; } = Vector2.zero;
    public Character_Controls _Character_Controls;
    public bool _Move_Is_Pressed;
    public bool InvertMouseY { get; private set; } = true;

    private void OnEnable()
    {
        _Character_Controls = new Character_Controls();
        _Character_Controls.NormalMovement.Enable();

        _Character_Controls.NormalMovement.Move.performed += SetMove;
        _Character_Controls.NormalMovement.Move.canceled += SetMove;

        _Character_Controls.NormalMovement.Look.performed += SetLook;
        _Character_Controls.NormalMovement.Look.canceled += SetLook;
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

    

    

}
