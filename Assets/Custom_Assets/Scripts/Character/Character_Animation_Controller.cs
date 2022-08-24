using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Klasse welches durch die vom Input System gegebenen Events den Animation Tree auf die passende Animation setzt

public class Character_Animation_Controller : MonoBehaviour
{
    private Animator _Animator;
    private Character_Controls _Character_Controls;

    private void OnEnable()
    {
        //Initialisierung der "Input Systeme Komponente"
        _Animator = this.GetComponent<Animator>();
        _Character_Controls = new Character_Controls();
        _Character_Controls.NormalMovement.Enable();

        //Basierend auf Input werden dementsprechende C# Events ausgelöst
        _Character_Controls.NormalMovement.Move.performed += movmentStarted;
        _Character_Controls.NormalMovement.Move.canceled += movementStopped;
        _Character_Controls.NormalMovement.Sprint.started += sprintStarted;
        _Character_Controls.NormalMovement.Sprint.canceled += sprintStopped;

        _Character_Controls.NormalMovement.Mouse_Actions.started += startedShooting;
        _Character_Controls.NormalMovement.Mouse_Actions.canceled += stoppedShooting;
    }

    //Bool's in der Animator Komponente werden gesetze damit der Animation Tree in den richtigen Animation State gerät
    public void movmentStarted(InputAction.CallbackContext ctx) { _Animator.SetBool("IsMoving", true); }
    public void movementStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsMoving", false); }
    public void sprintStarted(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", true); }
    public void sprintStopped(InputAction.CallbackContext ctx) { _Animator.SetBool("IsSprinting", false); }

    public void startedShooting(InputAction.CallbackContext ctx) { _Animator.SetBool("IsShooting", true); }
    public void stoppedShooting(InputAction.CallbackContext ctx) { _Animator.SetBool("IsShooting", false); }

    private void OnDisable()
    {
        _Character_Controls.NormalMovement.Move.performed -= movmentStarted;
        _Character_Controls.NormalMovement.Move.canceled -= movementStopped;
        _Character_Controls.NormalMovement.Sprint.started -= sprintStarted;
        _Character_Controls.NormalMovement.Sprint.canceled -= sprintStopped;

        _Character_Controls.NormalMovement.Mouse_Actions.started -= startedShooting;
        _Character_Controls.NormalMovement.Mouse_Actions.canceled -= stoppedShooting;

        _Character_Controls.NormalMovement.Disable();
    }
}
