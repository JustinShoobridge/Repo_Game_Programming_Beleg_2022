using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Enemy_Target_Manager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _Treasures;
    [SerializeField] public List<GameObject> _Exits;

    private void Awake()
    {
        foreach (GameObject treasure in GameObject.FindGameObjectsWithTag("Treasure")) { _Treasures.Add(treasure); }
        foreach (GameObject exit in GameObject.FindGameObjectsWithTag("Exit")) { _Exits.Add(exit); }
    }

    public GameObject getClosestTarget(GameObject other, string Type)
    {
        List<GameObject> Targets = new List<GameObject>();

        GameObject closestTarget = null;
        float closestDistance = 0;

        switch (Type)
        {
            case "Treasure":
                if(_Treasures.Any())
                {
                    foreach (GameObject target in _Treasures)
                    {
                        if (target.GetComponent<TreasureAttributes>().claimed == false)
                        {
                            Targets.Add(target);
                        }
                    };
                }
                break;

            case "Exit":
                Targets = _Exits;
                break;
        }

        if(Targets.Count <= 0) { Targets = _Exits; };

        foreach (GameObject target in Targets)
        {
            if (Vector3.Distance(target.transform.position, other.transform.position) < closestDistance || closestDistance == 0)
            {
                closestDistance = Vector3.Distance(target.transform.position, other.transform.position);
                closestTarget = target;
            }
        }
        return closestTarget;
    }
}
