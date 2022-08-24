using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasse um die von den den gegnern Registrierten Collisions zu verarbeiten und übermitteln
public class Enemy_Collision : MonoBehaviour
{
    public event Action _OnTreasureCollision;
    public event Action _OnExitCollision;
    public event Action _OnBulletCollision;

    public event Action _OnGroundEnter;
    public event Action _OnGroundExit;

    public Collision _CollisionObject;

    //Invoken von Events basierend auf Collision Typ
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        _CollisionObject = collision;
        switch (collision.gameObject.tag)
        {
            case "Treasure":
                _OnTreasureCollision?.Invoke();
                break;
            case "Exit":
                _OnExitCollision?.Invoke();
                break;
            case "Bullet":
                _OnBulletCollision?.Invoke();
                break;
        }
        return null;
    }
}
