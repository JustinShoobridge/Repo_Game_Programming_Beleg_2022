using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private List<GameObject> _SpawnLocations;
    [SerializeField] private MainGameManager _MainGameManager;
    
    [SerializeField] private int currentEnemiesSpawning = 1;
    [SerializeField] private int _EnemiesPerWaveIncrease = 1;
    [SerializeField] private int _TimeBetweenSpawns = 5;
    [SerializeField] private int _TimeUntilEnemyAmoutIncreases = 5;

    private void Awake()
    {
        _MainGameManager = GetComponent<MainGameManager>();
        foreach (GameObject spawnLocation in GameObject.FindGameObjectsWithTag("Exit")) { _SpawnLocations.Add(spawnLocation); }

        _MainGameManager._OnGameOver += StopCoroutines;

        StartCoroutine("spawnEnemies");
        StartCoroutine("increaseDifficulty");
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

    IEnumerator spawnEnemies()
    {
        while(true)
        {
            for (int i = 0; i < currentEnemiesSpawning; i++)
            {
                int randomNum = Random.Range(0, _SpawnLocations.Count);
                Instantiate(_Enemy, _SpawnLocations[randomNum].gameObject.transform.position, new Quaternion(0,0,0,0));
            }
            yield return new WaitForSeconds(_TimeBetweenSpawns);
        }
    }

    IEnumerator increaseDifficulty()
    {
        while (true)
        {
            currentEnemiesSpawning += _EnemiesPerWaveIncrease;
            yield return new WaitForSeconds(_TimeUntilEnemyAmoutIncreases);
        }
    }
}
