using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private GameObject _TimeDisplay;
    [SerializeField] private GameObject _Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float CurrentTime = _Manager.GetComponent<MainGameManager>()._TimeSurvived;

        TimeSpan timeSpanLeft = TimeSpan.FromSeconds(CurrentTime);
        _TimeDisplay.GetComponent<TMP_Text>().text = "Time Survived: " + timeSpanLeft.ToString(@"hh\:mm\:ss");
    }
}
