using UnityEngine;
using System.Collections;

public class Move_Bitch : MonoBehaviour {

    NavMeshAgent MainNavMeshAgent;

    public GameObject Target;

    public Vector3 Current_Steering_Target;
    public Vector3 Old_Steering_Target;

    // Use this for initialization
    void Start () {
        MainNavMeshAgent = GetComponent<NavMeshAgent>();
        //MainNavMeshAgent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y + gameObject.transform.position.y, Target.transform.position.z);
        MainNavMeshAgent.destination = Target.transform.position;
        Old_Steering_Target = MainNavMeshAgent.steeringTarget;
        transform.LookAt(Current_Steering_Target);
    }
	
	// Update is called once per frame
	void Update () {

        Current_Steering_Target = MainNavMeshAgent.steeringTarget;

        if (Current_Steering_Target != Old_Steering_Target)
        {
            Old_Steering_Target = Current_Steering_Target;
            transform.LookAt(Current_Steering_Target);
        }


    }
}
