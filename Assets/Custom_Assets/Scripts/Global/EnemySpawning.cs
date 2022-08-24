using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasse zum initialieren der Gegner im Spielverlauf

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private List<GameObject> _SpawnLocations;
    [SerializeField] private MainGameManager _MainGameManager;

    [SerializeField] public int currentDifficulty = 0;
    [SerializeField] private int currentEnemiesSpawning = 1;
    [SerializeField] private int _EnemiesPerWaveIncrease = 1;
    [SerializeField] private int _TimeBetweenSpawns = 5;
    [SerializeField] private int _TimeUntilDifficultyIncreasesAgain = 5;

    private void Awake()
    {
        _MainGameManager = GetComponent<MainGameManager>();
        foreach (GameObject spawnLocation in GameObject.FindGameObjectsWithTag("Exit")) { _SpawnLocations.Add(spawnLocation); } //Augänge positionen zu möglichen Spawn Locations hinzufügen

        _MainGameManager._OnGameOver += StopCoroutines;

        StartCoroutine("spawnEnemies");
        StartCoroutine("increaseDifficulty");
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

    //Sucht sich aus der Liste der Spawn Locations pro Gegner eine zufällige aus und Instantiert dort das Prefab
    IEnumerator spawnEnemies()
    {
        while(true)
        {
            for (int i = 0; i < currentEnemiesSpawning; i++)
            {
                int randomNum = Random.Range(0, _SpawnLocations.Count);
                Instantiate(_Enemy, _SpawnLocations[randomNum].gameObject.transform.position, new Quaternion(0,0,0,0));
            }
            yield return new WaitForSeconds(_TimeBetweenSpawns); //Warten auf die nächste Gegnerwelle
        }
    }

    //Erhöht mit zeitverlauf die Anzahl der Gegener die in jeder Welle Instantiert werden
    IEnumerator increaseDifficulty()
    {
        while (true)
        {
            currentEnemiesSpawning += _EnemiesPerWaveIncrease;
            currentDifficulty++;

            yield return new WaitForSeconds(_TimeUntilDifficultyIncreasesAgain);
        }
    }

    private void OnDisable()
    {
        _MainGameManager._OnGameOver -= StopCoroutines;
    }
}
