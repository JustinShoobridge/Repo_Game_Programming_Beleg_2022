using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Haupt Controller für jede Instanz der Gegner
public class Enemy_Controller : MonoBehaviour, IIsDamagable
{
    [SerializeField] float _EnemyHealth;
    
    private GameObject _Manager;
    private GameObject _AudioManager;

    private MainGameManager _MainGameManager;
    private Enemy_Collision _EnemyCollsion;
    private Enemy_Target_Manager _EnemyTargetManager;
    private Enemy_Targeting _EnemyTargeting;

    [SerializeField] private float _SpeedIncreasePerDifficulty = 10.0f;


    void Start()
    {
        _Manager = GameObject.FindGameObjectWithTag("Manager"); //Suchen des Globalen Spiel Managers
        _AudioManager = GameObject.FindGameObjectWithTag("AudioManager"); //Suchen des Globalen Audio Managers

        _EnemyTargetManager = _Manager.GetComponent<Enemy_Target_Manager>();
        _MainGameManager = _Manager.GetComponent<MainGameManager>();

        _EnemyCollsion = GetComponent<Enemy_Collision>();
        _EnemyTargeting = GetComponent<Enemy_Targeting>();

        _EnemyCollsion._OnBulletCollision += takeDamage;
        _EnemyTargeting._OnReachedExitWithTreasure += OnReachedExitWithTreasure;
        _EnemyTargeting._OnReachedExitWithoutTreasure += destroyEnemy;

        //Erhöhen der Geschwindigkeit des Gegners basierend auf dem Schierigkeitsgrads im Golbalen Spiel Managers
        for(int i = 0; i < _Manager.GetComponent<EnemySpawning>().currentDifficulty; i++)
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().speed += _SpeedIncreasePerDifficulty;
        }

        _EnemyHealth = 100;
    }

    public void takeDamage()
    {
        _EnemyHealth += -50;
        if(_EnemyHealth <= 0) {
            destroyEnemy(); 
        };
        if(gameObject != null)
        {
            _AudioManager.GetComponent<AudioManager>().PlaySoundEffect(SoundEffectTypes.ENEMYHIT, this.transform.position); //Abspielen des "Getroffen Sound Effekts"
        }

    }

    public void destroyEnemy()
    {
        _AudioManager.GetComponent<AudioManager>().PlaySoundEffect(SoundEffectTypes.ENEMYDEATH, this.transform.position); //Abspielen des "Gestorben Sound Effekts - ausgelagert da es sonst kein Gamobjekt gibt von dem es abspielbar wäre
        StartCoroutine("destroyEnemyCoroutine");
    }

    IEnumerator destroyEnemyCoroutine()
    {
        yield return new WaitForFixedUpdate();

        if(_EnemyTargeting._CurrentTreasure == null)
        {
            Destroy(this.gameObject);
        }  
    }

    public void OnReachedExitWithTreasure()
    {
        _MainGameManager._CapturedTreasures++; //Erhöhen des Gestohlene Schätze-Scores

        _EnemyTargetManager._Treasures.Remove(_EnemyTargeting._CurrentTreasure); //Entfernen des Schatzes von der mögliche Ziele Liste
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        _EnemyCollsion._OnBulletCollision -= takeDamage;
        _EnemyTargeting._OnReachedExitWithTreasure -= OnReachedExitWithTreasure;
        _EnemyTargeting._OnReachedExitWithoutTreasure -= destroyEnemy;
    }
}
