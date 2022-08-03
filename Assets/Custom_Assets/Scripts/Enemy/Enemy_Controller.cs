using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour, IIsDamagable
{
    [SerializeField] float _EnemyHealth;

    void Start()
    {
        _EnemyHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        //this.GetComponent<Rigi>
    }
}
