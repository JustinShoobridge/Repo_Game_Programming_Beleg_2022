using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy_Target_Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Treasures;
    [SerializeField] private List<GameObject> _Exits;
 
    public GameObject getClosestTarget(GameObject other, string Type)
    {
        List<GameObject> Targets = new List<GameObject>();

        GameObject closestTarget = null;
        float closestDistance = 0;

        switch (Type)
        {
            
            case "Treasure":
                foreach (GameObject target in _Treasures)
                {
                    if (target.GetComponent<TreasureAttributes>().claimed == false)
                    {
                        Targets.Add(target);
                    }
                };
                break;

            case "Exit": Targets = _Exits; break;
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
