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

        _Character_Controls.NormalMovement.Mouse_Actions.started += startedShooting;
        _Character_Controls.NormalMovement.Mouse_Actions.canceled += stoppedShooting;
    }

    public void movmentStarted(InputAction.CallbackContext ctx) { _Animator.SetBool("IsMoving", true); }
    public void movementStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsMoving", false); }
    public void sprintStarted(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", true); }
    public void sprintStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", false); }

    public void startedShooting(InputAction.CallbackContext ctx) { _Animator.SetBool("IsShooting", true); }
    public void stoppedShooting(InputAction.CallbackContext ctx) { _Animator.SetBool("IsShooting", false); }

    private void OnDisable()
    {
        _Character_Controls.NormalMovement.Disable();
    }
}
