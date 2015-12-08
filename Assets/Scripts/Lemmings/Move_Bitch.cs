using UnityEngine;
using System.Collections;

public class Move_Bitch : MonoBehaviour {

    NavMeshAgent MainNavMeshAgent;
    public GameObject Target;

    // Use this for initialization
    void Start () {
        MainNavMeshAgent = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {
        MainNavMeshAgent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y + gameObject.transform.position.y, Target.transform.position.z);
        transform.LookAt(transform.position - -transform.forward * 2);
    }
}
