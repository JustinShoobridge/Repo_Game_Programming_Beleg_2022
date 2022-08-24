using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Klasse die jeweils eine Liste über die noch verfügbaren Schätze und Ausgänge führt.
public class Enemy_Target_Manager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _Treasures;
    [SerializeField] public List<GameObject> _Exits;

    //Finden der jeweilig passenden ListenObjekte in der Szene
    private void Awake()
    {
        foreach (GameObject treasure in GameObject.FindGameObjectsWithTag("Treasure")) { _Treasures.Add(treasure); }
        foreach (GameObject exit in GameObject.FindGameObjectsWithTag("Exit")) { _Exits.Add(exit); }
    }

    //Erkennt basierend auf dem Objekt welches die Methode aufruft welches dan nächste Objekt vom gewollten Typ zum fragenden Objekt ist
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
                            Targets.Add(target); //Hizufügen aller Schätze zur Ziele Liste falls ein Schatz gesucht wird
                        }
                    };
                }
                break;

            case "Exit":
                Targets = _Exits; //Hizufügen aller Exits zur Ziele Liste falls ein Exit gesucht wird
                break;
        }

        if(Targets.Count <= 0) { Targets = _Exits; }; //Wenn die Liste an Schätzen Leer ist werden die Ausgänge die möglichen Ziele

        foreach (GameObject target in Targets)
        {
            if (Vector3.Distance(target.transform.position, other.transform.position) < closestDistance || closestDistance == 0)
            {
                closestDistance = Vector3.Distance(target.transform.position, other.transform.position); //Erkennen welches das nächste Ziel ist
                closestTarget = target;
            }
        }
        return closestTarget;
    }
}
