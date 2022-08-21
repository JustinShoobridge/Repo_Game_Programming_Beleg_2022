using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour, IIsDamagable
{
    [SerializeField] float _EnemyHealth;
    
    private GameObject _Manager;
    private GameObject _AudioManager;

    private MainGameManager _MainGameManager;
    private Enemy_Collision _EnemyCollsion;
    private Enemy_Target_Manager _EnemyTargetManager;
    private Enemy_Targeting _EnemyTargeting;

    void Start()
    {
        _Manager = GameObject.FindGameObjectWithTag("Manager");
        _AudioManager = GameObject.FindGameObjectWithTag("AudioManager");

        _EnemyTargetManager = _Manager.GetComponent<Enemy_Target_Manager>();
        _MainGameManager = _Manager.GetComponent<MainGameManager>();

        _EnemyCollsion = GetComponent<Enemy_Collision>();
        _EnemyTargeting = GetComponent<Enemy_Targeting>();

        _EnemyCollsion._OnBulletCollision += takeDamage;
        _EnemyTargeting._OnReachedExitWithTreasure += OnReachedExitWithTreasure;
        _EnemyTargeting._OnReachedExitWithoutTreasure += destroyEnemy;

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
            _AudioManager.GetComponent<AudioManager>().PlaySoundEffect(SoundEffectTypes.ENEMYHIT, this.transform.position);
        }

    }

    public void destroyEnemy()
    {
        _AudioManager.GetComponent<AudioManager>().PlaySoundEffect(SoundEffectTypes.ENEMYDEATH, this.transform.position);
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
        _MainGameManager._CapturedTreasures++;

        _EnemyTargetManager._Treasures.Remove(_EnemyTargeting._CurrentTreasure);
        Destroy(this.gameObject);
    }
}
