using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision : MonoBehaviour
{
    public event Action _OnTreasureCollision;
    public event Action _OnExitCollision;
    public event Action _OnBulletCollision;

    public event Action _OnGroundEnter;
    public event Action _OnGroundExit;

    public Collision _CollisionObject;

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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("Entering Ground");
            _OnGroundEnter?.Invoke();
        }
        else
        {
            Debug.Log("Leaving Ground");
            _OnGroundExit?.Invoke();
        }
    }
}
