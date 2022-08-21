using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] public float _TimeSurvived;
    [SerializeField] public int _TotalTreasures;
    [SerializeField] public int _CapturedTreasures;
    [SerializeField] public bool _IsGameOver;

    [SerializeField] private GameObject _Manager;
    [SerializeField] private AudioManager _AudioManger;

    private Enemy_Target_Manager _EnemyTargetManager;
    public event Action _OnGameOver;

    void Start()
    {
        _EnemyTargetManager = _Manager.GetComponent<Enemy_Target_Manager>();

        _TotalTreasures = _EnemyTargetManager._Treasures.Count;
        _TimeSurvived = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_IsGameOver == false)
        {
            if (_CapturedTreasures == _TotalTreasures)
            {
                _IsGameOver = true;
                _OnGameOver?.Invoke();
            }
            _TimeSurvived += Time.deltaTime;
        }
        
    }
}
