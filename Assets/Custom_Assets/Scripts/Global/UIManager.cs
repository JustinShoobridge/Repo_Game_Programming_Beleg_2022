using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    //Klasse um Werte des UI's zu aktualieseren und den Game-Over Screen zu enabeln
    private MainGameManager _MainGameManager;
    private int _lastAmountOfCapturedTreasures = 0;

    [SerializeField] private GameObject _Manager;
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
        if(_lastAmountOfCapturedTreasures < _MainGameManager._CapturedTreasures)
        {
            StartCoroutine(ScaleTextForShortTimeframe(_TreasureCountDisplay));
            _lastAmountOfCapturedTreasures = _MainGameManager._CapturedTreasures;
        }

        String CurrentScore = "Stolen Treasures: \n " + _MainGameManager._CapturedTreasures + " / " + _MainGameManager._TotalTreasures;
        _TreasureCountDisplay.GetComponent<TMP_Text>().text = CurrentScore;
        

        float CurrentTime = _MainGameManager._TimeSurvived;
        TimeSpan timeSpanLeft = TimeSpan.FromSeconds(CurrentTime);
        _TimeDisplay.GetComponent<TMP_Text>().text = "Time Survived: " + timeSpanLeft.ToString(@"hh\:mm\:ss");
    }

    public void enableGameOverScreen()
    {
        _GameOverScreen.SetActive(true);
    }

    //Kurzes Hochskalieren und umcolorieren des Textes um den Spieler auf etwas aufmerksam zu machen
    IEnumerator ScaleTextForShortTimeframe(GameObject textField)
    {
        textField.gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        textField.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        textField.gameObject.transform.localScale += new Vector3(-0.2f, -0.2f, -0.2f);
        textField.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
