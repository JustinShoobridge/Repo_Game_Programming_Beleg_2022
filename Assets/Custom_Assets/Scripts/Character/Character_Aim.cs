using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Aim : MonoBehaviour
{
    [SerializeField] Camera _player_Camera;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cast_From_Middle_Of_Screen();
        }
    }

    private void cast_From_Middle_Of_Screen()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 500f;


        Ray ray = _player_Camera.ViewportPointToRay(rayOrigin);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            Debug.Log(hit.collider.gameObject.name);
            IIsDamagable damagable = hit.collider.GetComponent<IIsDamagable>();

            if(damagable != null)
            {
                damagable.takeDamage();
            }
        }
    }
}
