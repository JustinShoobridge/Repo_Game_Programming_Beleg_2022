using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character_Aim : MonoBehaviour
{
    [SerializeField] Camera _player_Camera;

    private Character_Controls _Character_Controls;

    private void Awake() => _Character_Controls = new Character_Controls();
    private void OnEnable()
    {
        _Character_Controls.NormalMovement.Mouse_Actions.performed += cast_From_Middle_Of_Screen;
        _Character_Controls.NormalMovement.Mouse_Actions.Enable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void cast_From_Middle_Of_Screen(InputAction.CallbackContext ctx)
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 500f;


        Ray ray = _player_Camera.ViewportPointToRay(rayOrigin);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            IIsDamagable damagable = hit.collider.GetComponent<IIsDamagable>();

            if(damagable != null)
            {
                damagable.takeDamage();
            }
        }
    }

    private void OnDisable() => _Character_Controls.NormalMovement.Mouse_Actions.Disable();
}
