using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//Klasse zum Erkennen was das momentane Ziel des Gegners sein soll, und wie die Collisions darauf einfluss haben

public class Enemy_Targeting : MonoBehaviour
{
    private Transform _Target;
    public GameObject _CurrentTreasure = null;

    private Enemy_Target_Manager _EnemyTargetManager;
    private Enemy_Collision _EnemyCollsision;
    private NavMeshAgent _Nav_Mesh_Agent;
    

    public event Action _OnReachedExitWithTreasure;
    public event Action _OnReachedExitWithoutTreasure;

    [SerializeField] private bool hasReachedTreasure = false;
    [SerializeField] private bool hasReachedExit = false;
    

    private void Awake()
    {
        _EnemyTargetManager = GameObject.FindWithTag("Manager").GetComponent<Enemy_Target_Manager>();
        _EnemyCollsision = GetComponent<Enemy_Collision>();
        _Nav_Mesh_Agent = GetComponent<NavMeshAgent>();

        _EnemyCollsision._OnTreasureCollision += onTreasureCollision;
        _EnemyCollsision._OnExitCollision += onExitCollision;
        _EnemyCollsision._OnBulletCollision += onBulletEnter;

        StartCoroutine("updateTarget");
    }

    //Methode welche jedem Gegner sein momentanes Ziel übermittelt
    IEnumerator updateTarget()
    {
        while(true)
        {
            if (hasReachedTreasure == false) //Trägt der gegner momentan schon einen Schatz
            {
                _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Treasure").transform; //Holt sich auc einer Liste das nächste Gameobject des Typ Treasure
                if (_Target != null && this.GetComponent<NavMeshAgent>().isActiveAndEnabled)
                {
                    _Nav_Mesh_Agent.destination = _Target.position; //Setzen des Ziels

                    if (hasReachedTreasure == false && hasReachedExit == true && _Target.tag == "Exit") //Erkennen das das zurückgegebene Ziel ein augang ist und dieser erreicht wurde
                    {
                        _OnReachedExitWithoutTreasure?.Invoke(); 
                    }
                }
            }

            if (hasReachedExit == false && hasReachedTreasure == true) //erkennen das der Gegner einen Schatz
            {
                _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Exit").transform; //Suchen des nächsten Exits aus der Liste
                _Nav_Mesh_Agent.destination = _Target.position;
            }

            if (hasReachedExit && hasReachedTreasure) //Erkennen das der Gegner das Ziel mit schatz erreicht hat
            {
                _OnReachedExitWithTreasure?.Invoke();
            }
            yield return new WaitForSeconds(0.3f); //Warten um etwas performance zu sparen
        }
    }

    public void onTreasureCollision()
    {
        Collision treasure = _EnemyCollsision._CollisionObject;
        if (treasure.gameObject.GetComponent<TreasureAttributes>().claimed == false && hasReachedTreasure == false) //Erkennen ob der Schatz mit dem kollidiert wurde schon von jemandem getragen wird
        {
            _CurrentTreasure = treasure.gameObject;
            hasReachedTreasure = true;

            _CurrentTreasure.GetComponent<TreasureAttributes>().claimed = true;
            _CurrentTreasure.GetComponent<Rigidbody>().isKinematic = true;
            _CurrentTreasure.transform.SetParent(this.transform);
            _CurrentTreasure.transform.localPosition = new Vector3(0, 3, 0);
        }
    }

    public void onExitCollision()
    {
        if (_CurrentTreasure != null || _Target.tag == "Exit") { hasReachedExit = true; }
    }

    public void onBulletEnter()
    {
        StartCoroutine("onBulletEnterCoroutine");
    }

    IEnumerator onBulletEnterCoroutine()
    {
        GameObject temporaryReference = _CurrentTreasure;

        if (temporaryReference != null)
        {
            _CurrentTreasure = null;
            temporaryReference.transform.SetParent(null);
            hasReachedTreasure = false;
            temporaryReference.GetComponent<Rigidbody>().isKinematic = false;
            temporaryReference.GetComponent<TreasureAttributes>().claimed = false;

            Physics.IgnoreCollision(temporaryReference.GetComponent<Collider>(), this.GetComponent<Collider>()); //Collision zwischen dem gerade verlorenen Schatz und dem Gegner disabeln um zu verhindern das dieser den selben sofort wieder aufhebt
            yield return new WaitForSeconds(2.0f); //Der Zeitraum bis der Gegener das slebe Object wieder aufheben darf
            if(this.gameObject != null && temporaryReference != null)
            {
                Physics.IgnoreCollision(temporaryReference.GetComponent<Collider>(), this.GetComponent<Collider>(), false); //Re-Enabeln der Collision
            }
        }
    }

    private void OnDisable()
    {
        _EnemyCollsision._OnTreasureCollision -= onTreasureCollision;
        _EnemyCollsision._OnExitCollision -= onExitCollision;
        _EnemyCollsision._OnBulletCollision -= onBulletEnter;
    }
}
