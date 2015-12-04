using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class Move_To_Click : MonoBehaviour {

    public Vector3 Destination;
    public GameObject Camera;
	NavMeshAgent MainNavMeshAgent ;


    // Use this for initialization
    void Start () {
		MainNavMeshAgent = GetComponent<NavMeshAgent>();
		//Debug.Log ("start");
		//Debug.Log(MainNavMeshAgent);
	
	}

	public void Update_NavMesh_Destination ()
    {
            MainNavMeshAgent.destination = new Vector3(Camera.GetComponent<Click_To_Move>().Destination.x, Camera.GetComponent<Click_To_Move>().Destination.y + gameObject.transform.position.y, Camera.GetComponent<Click_To_Move>().Destination.z);
	
	}
}
