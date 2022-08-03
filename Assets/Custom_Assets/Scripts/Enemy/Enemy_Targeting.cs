using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Targeting : MonoBehaviour
{
    private Transform _Target;
    private Enemy_Target_Manager _EnemyTargetManager;
    private NavMeshAgent _Nav_Mesh_Agent;

    [SerializeField] private bool hasReachedTreasure = false;
    [SerializeField] private bool hasReachedExit = false;
    

    private void Awake()
    {
        _EnemyTargetManager = GameObject.FindWithTag("Manager").GetComponent<Enemy_Target_Manager>();
        _Nav_Mesh_Agent = GetComponent<NavMeshAgent>();

        StartCoroutine(updateTarget());
    }

    IEnumerator updateTarget()
    {
        while(hasReachedTreasure == false)
        {
            _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Treasure").transform;
            _Nav_Mesh_Agent.destination = _Target.position;

            yield return null;
        }

        while(hasReachedExit == false && hasReachedTreasure == true)
        {
            _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Exit").transform;
            _Nav_Mesh_Agent.destination = _Target.position;

            yield return null;
        }
        StopCoroutine(updateTarget());
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "NPC_Stealable": hasReachedTreasure = true;
                                  collision.gameObject.GetComponent<TreasureAttributes>().claimed = true;
                                  collision.transform.position.Set(0,10,0);
                                  collision.gameObject.transform.SetParent(this.transform);
                                  break;
            case "Exit": hasReachedTreasure = true; break;
        }
    }
}
