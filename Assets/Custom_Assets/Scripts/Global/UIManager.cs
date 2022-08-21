using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _Manager;
    private MainGameManager _MainGameManager;

    [SerializeField] private Canvas _Canvas;
    [SerializeField] private GameObject _TimeDisplay;
    [SerializeField] private GameObject _TreasureCountDisplay;
    [SerializeField] private GameObject _GameOverScreen;

    private void Start()
    {
        _MainGameManager = _Manager.GetComponent<MainGameManager>();

        _MainGameManager._OnGameOver += enableGameOverScreen;
    }

    void Update()
    {
        String CurrentScore = "Stolen Treasures: \n " + _MainGameManager._CapturedTreasures + " / " + _MainGameManager._TotalTreasures;
        _TreasureCountDisplay.GetComponent<TMP_Text>().text = CurrentScore;

        float CurrentTime = _MainGameManager._TimeSurvived;
        TimeSpan timeSpanLeft = TimeSpan.FromSeconds(CurrentTime);
        _TimeDisplay.GetComponent<TMP_Text>().text = "Time Survived: " + timeSpanLeft.ToString(@"hh\:mm\:ss");
    }

    public void enableGameOverScreen()
    {
        _GameOverScreen.GetComponent<TMP_Text>().enabled = true;
    }
}
