using UnityEngine;
using System.Collections;

public class Nest_Dweller_minion : MonoBehaviour {

    public GameObject Destination;
    public GameObject Nest;
    public float Waypoint_Range;
    public bool Return_Home;


	// Use this for initialization
	void Start () {

        InvokeRepeating("UnStuck", 0f, 3f);
	
	}

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject == Destination.gameObject)
        {
            if (Col.gameObject.name.Contains("Entrance") == true && Return_Home == false)
            {
                Destination = Nest.GetComponent<Nest_Startup>().WayPoint_Array[Random.Range(Nest.GetComponent<Nest_Startup>().WayPoint_Array.Length - 1, 0)];
                gameObject.GetComponent<NavMeshAgent>().SetDestination(Destination.transform.position);
            }

            if (Col.gameObject.name.Contains("Entrance") == false && Return_Home == false)
            {
                Col.gameObject.transform.position = new Vector3(Random.Range(Waypoint_Range + Nest.gameObject.transform.position.x, -Waypoint_Range + Nest.gameObject.transform.position.x), Col.gameObject.transform.localScale.y / 2, Random.Range(Waypoint_Range + Nest.gameObject.transform.position.z, -Waypoint_Range + Nest.gameObject.transform.position.z));
                Destination = Nest.GetComponent<Nest_Startup>().Entrance;
                gameObject.GetComponent<NavMeshAgent>().SetDestination(Destination.transform.position);
                Return_Home = true;
            }

            if (Col.gameObject.name.Contains("Entrance") == true && Return_Home == true)
            {
                Destination = Nest;
                gameObject.GetComponent<NavMeshAgent>().SetDestination(Destination.transform.position);
            }

            if (Col.gameObject == Nest.gameObject && Return_Home == true)
            {
                Return_Home = false;
                ++Nest.GetComponent<Nest_Startup>().Gathered_Resources;
                Destination = Nest.GetComponent<Nest_Startup>().Entrance;
                gameObject.GetComponent<NavMeshAgent>().SetDestination(Destination.transform.position);

            }

        }
    }
	
    void UnStuck()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(Destination.transform.position);
    }
}
