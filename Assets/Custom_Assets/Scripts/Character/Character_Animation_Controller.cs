using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Animation_Controller : MonoBehaviour
{
    private Animator _Animator;

    private Character_Controls _Character_Controls;

    [SerializeField] float _transition_Time = 20.0f;

    private void OnEnable()
    {
        _Animator = this.GetComponent<Animator>();

        _Character_Controls = new Character_Controls();
        _Character_Controls.NormalMovement.Enable();

        _Character_Controls.NormalMovement.Move.performed += movmentStarted;
        _Character_Controls.NormalMovement.Move.canceled += movementStopped;
        _Character_Controls.NormalMovement.Sprint.started += sprintStarted;
        _Character_Controls.NormalMovement.Sprint.canceled += sprintStopped;
    }

    public void movmentStarted(InputAction.CallbackContext ctx) { Debug.Log("Test"); _Animator.SetBool("IsMoving", true); }
    public void movementStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsMoving", false); }
    public void sprintStarted(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", true); }
    public void sprintStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", false); }

    private void OnDisable()
    {
        _Character_Controls.NormalMovement.Disable();
    }
}
