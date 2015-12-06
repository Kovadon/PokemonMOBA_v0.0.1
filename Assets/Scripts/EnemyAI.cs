using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

	public Transform targetPlayer;
    //public GameObject myEnemy;
	private NavMeshAgent CachedNavMeshAgent;
    //public int moveSpeed = 5;
    //public int rotationSpeed = 5;


    void Awake ()
    {
        //myEnemy = transform;
		if (! targetPlayer) {
			targetPlayer = GameObject.FindWithTag ("Character").transform;
		}
		CachedNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

	void Start ()
    {

	}
	
	void FixedUpdate ()
    {
		CachedNavMeshAgent.destination = targetPlayer.position;
	}
}
