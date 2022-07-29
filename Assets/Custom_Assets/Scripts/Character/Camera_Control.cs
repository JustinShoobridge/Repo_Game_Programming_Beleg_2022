using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    private Transform _CameraFollow;

    private Vector3 _PlayerLookInput = Vector3.zero;
    private Vector3 _previousPlayerLookInput = Vector3.zero;

    float _cameraPitch = 0.0f;
    [SerializeField] float _playerLookInputLerpTime = 0.35f;

    private void FixedUpdate()
    {
        _PlayerLookInput = GetLookInput();
        PlayerLook();
        PitchCamera();
    }

    

    

    private Vector3 GetLookInput()
    {
        throw new NotImplementedException();
    }

    private void PitchCamera()
    {
        throw new NotImplementedException();
    }

    private void PlayerLook()
    {
        throw new NotImplementedException();
    }
}
