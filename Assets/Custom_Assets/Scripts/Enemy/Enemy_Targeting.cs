using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Targeting : MonoBehaviour
{
    private Transform _Target;
    private Enemy_Target_Manager _EnemyTargetManager;
    private NavMeshAgent _Nav_Mesh_Agent;
    private GameObject _CurrentTreasure = null;

    [SerializeField] private bool hasReachedTreasure = false;
    [SerializeField] private bool hasReachedExit = false;
    

    private void Awake()
    {
        _EnemyTargetManager = GameObject.FindWithTag("Manager").GetComponent<Enemy_Target_Manager>();
        _Nav_Mesh_Agent = GetComponent<NavMeshAgent>();

        StartCoroutine("updateTarget");
    }

    IEnumerator updateTarget()
    {
        while(true)
        {
            if (hasReachedTreasure == false)
            {
                _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Treasure").transform;
                _Nav_Mesh_Agent.destination = _Target.position;
            }

            if (hasReachedExit == false && hasReachedTreasure == true)
            {
                _Target = _EnemyTargetManager.getClosestTarget(this.gameObject, "Exit").transform;
                _Nav_Mesh_Agent.destination = _Target.position;
            }

            if(hasReachedExit && hasReachedTreasure)
            {
                _EnemyTargetManager._Treasures.Remove(_CurrentTreasure);
                Destroy(this.gameObject);
                StopCoroutine("updateTarget"); 
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "NPC_Stealable":
                if (collision.gameObject.GetComponent<TreasureAttributes>().claimed == false && hasReachedTreasure == false)
                {
                    _CurrentTreasure = collision.gameObject;
                    hasReachedTreasure = true;

                    _CurrentTreasure.GetComponent<TreasureAttributes>().claimed = true;
                    _CurrentTreasure.GetComponent<Rigidbody>().isKinematic = true;
                    _CurrentTreasure.transform.SetParent(this.transform);
                    _CurrentTreasure.transform.localPosition = new Vector3(0,3,0);      
                }
                break;

            case "Exit": 
                if (_CurrentTreasure != null)
                {
                    hasReachedExit = true;
                    
                }
                break;
                

            case "Bullet":
                hasReachedTreasure = false;
                if (_CurrentTreasure != null)
                {
                    _CurrentTreasure.GetComponent<Rigidbody>().isKinematic = false;
                    _CurrentTreasure.GetComponent<TreasureAttributes>().claimed = false;
                    _CurrentTreasure.transform.SetParent(null);

                    Physics.IgnoreCollision(_CurrentTreasure.GetComponent<Collider>(), this.GetComponent<Collider>());
                    yield return new WaitForSeconds(0.1f);
                    Physics.IgnoreCollision(_CurrentTreasure.GetComponent<Collider>(), this.GetComponent<Collider>(), false);
                }
                break;
        }
    }
}
