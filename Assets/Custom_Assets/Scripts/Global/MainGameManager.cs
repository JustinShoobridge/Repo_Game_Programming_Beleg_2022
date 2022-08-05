using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] public float _TimeSurvived;

    void Start()
    {
        _TimeSurvived = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _TimeSurvived += Time.deltaTime;
    }
}
