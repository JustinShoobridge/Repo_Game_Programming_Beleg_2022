using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private List<GameObject> _SpawnLocations;

    [SerializeField] private bool gameOver;
    [SerializeField] private int currentEnemiesSpawning;

    private void Start()
    {
        StartCoroutine("spawnEnemies");
        StartCoroutine("increaseDifficulty");
    }

    IEnumerator spawnEnemies()
    {
        while(gameOver == false)
        {
            for (int i = 0; i < currentEnemiesSpawning; i++)
            {
                int randomNum = Random.Range(0, _SpawnLocations.Count);
                Instantiate(_Enemy, _SpawnLocations[randomNum].gameObject.transform.position, new Quaternion(0,0,0,0));
            }
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator increaseDifficulty()
    {
        while (gameOver == false)
        {
            currentEnemiesSpawning++;
            yield return new WaitForSeconds(60);
        }
    }
}
