using UnityEngine;
using System.Collections;

public class Nest_Startup : MonoBehaviour {

    //waypoint information
    //waypoints are the objects that the minions will walk around and collect while not fighting
    public GameObject WayPoint;
    public GameObject[] WayPoint_Array = null;
    public int WayPoints_To_Spawn;
    public float Waypoint_Range;
    Vector3 Spawn_Vector;
    public float Waypoint_Resource_Value;



    //Minion information, speaks for itself
    public GameObject Minion;
    public GameObject[] Minion_Array = null;
    public int Minions_To_Spawn;
    public float Minion_Health;
    public int Minion_Speed;
    public int Minions_Alive;
    public bool Wrong_Neighborhood;
    public bool Hunt_Gather;
    public GameObject Destination;



    //nest information
    public GameObject Nest_Aggro_Collider_Object;
    public GameObject Model_To_Spawn_On;
    public float Nest_Health;
    public string Nest_Team;
    public int Gathered_Resources;



    //nest entrance, this is basically telling the mobs where/whatside the entrance is on
    public bool Entrance_Up;
    public bool Entrance_Right;
    public bool Entrance_Forward;
    public bool Opposite_Side;
    public GameObject Entrance;
    Vector3 Entrance_Vector;





    void Start () {


        gameObject.GetComponent<Health_Pool>().Max_Health = Nest_Health;
        gameObject.GetComponent<Health_Pool>().Current_Health = Nest_Health;
        Nest_Team = gameObject.GetComponent<Team>().Team_Name;

        Entrance_Direction();
        Place_Entrance();
        Spawn_Minions();

        if (Hunt_Gather != false)
        {
            Spawn_Waypoints();
            InvokeRepeating("Activate_Minion", 1f, 1f);
        }


	
	}

    public void Entrance_Direction()
    {
        if (Opposite_Side != true)
        {
            if (Entrance_Forward == true)
            {
                Entrance_Vector = Vector3.forward;
            }

            if (Entrance_Right == true)
            {
                Entrance_Vector = Vector3.right;
            }

            if (Entrance_Up == true)
            {
                Entrance_Vector = Vector3.up;
            }
        }
        else
        {
            if (Entrance_Forward == true)
            {
                Entrance_Vector = -Vector3.forward;
            }

            if (Entrance_Right == true)
            {
                Entrance_Vector = -Vector3.right;
            }

            if (Entrance_Up == true)
            {
                Entrance_Vector = -Vector3.up;
            }
        }
    }

    public void Place_Entrance()
    {
        Entrance = Instantiate(WayPoint, new Vector3(transform.position.x + (Entrance_Vector.x * transform.localScale.x * 2), transform.position.y / 2, transform.position.z + (Entrance_Vector.z * transform.localScale.z * 2)),  Model_To_Spawn_On.transform.rotation) as GameObject;
        Entrance.transform.SetParent(gameObject.transform);
        Entrance.name = gameObject.name + "_Entrance";
    }

    public void Spawn_Minions()
    {
        Minion_Array = new GameObject[Minions_To_Spawn];

        for (int i = 0; i < Minion_Array.Length; i++)
        {
            Minion_Array[i] = Instantiate(Minion, Model_To_Spawn_On.transform.position, Model_To_Spawn_On.transform.rotation) as GameObject;
            Minion_Array[i].transform.SetParent(gameObject.transform);
            Minion_Array[i].GetComponent<Nest_Dweller_minion>().Nest = gameObject;
            Minion_Array[i].GetComponent<Team>().Team_Blue = gameObject.GetComponent<Team>().Team_Blue;
            Minion_Array[i].GetComponent<Team>().Team_Red = gameObject.GetComponent<Team>().Team_Red;
            Minion_Array[i].GetComponent<Team>().Neutral = gameObject.GetComponent<Team>().Neutral;
            Minion_Array[i].GetComponent<Team>().Team_Name = gameObject.GetComponent<Team>().Team_Name;
            Minion_Array[i].GetComponent<Health_Pool>().Max_Health = Minion_Health;
            Minion_Array[i].GetComponent<Health_Pool>().Current_Health = Minion_Health;
            Minion_Array[i].GetComponent<Nest_Dweller_minion>().Waypoint_Range = Waypoint_Range;
            Minion_Array[i].SetActive(false);
        }
    }

    public void Spawn_Waypoints()
    {
        WayPoint_Array = new GameObject[WayPoints_To_Spawn];

        for (int i = 0; i < WayPoint_Array.Length; i++)
        {
            Spawn_Vector = new Vector3(Random.Range(Waypoint_Range + transform.position.x, -Waypoint_Range + +transform.position.x), WayPoint.transform.localScale.y / 2, Random.Range(Waypoint_Range + transform.position.z, -Waypoint_Range + transform.position.z));
            WayPoint_Array[i] = Instantiate(WayPoint, Spawn_Vector, transform.rotation) as GameObject;
            WayPoint_Array[i].transform.SetParent(gameObject.transform);
            WayPoint_Array[i].name = "Waypoint_" + i;
        }
    }

    public void Activate_Minion()
    {
        for (int i = 0; i < Minion_Array.Length; i++)
        {
            if (Minions_To_Spawn > Minions_Alive)
            {
                if (Minion_Array[i].activeInHierarchy == false)
                {
                    Minion_Array[i].GetComponent<Team>().Team_Blue = gameObject.GetComponent<Team>().Team_Blue;
                    Minion_Array[i].GetComponent<Team>().Team_Red = gameObject.GetComponent<Team>().Team_Red;
                    Minion_Array[i].GetComponent<Team>().Neutral = gameObject.GetComponent<Team>().Neutral;
                    Minion_Array[i].GetComponent<Team>().Team_Name = gameObject.GetComponent<Team>().Team_Name;
                    Minion_Array[i].GetComponent<Health_Pool>().Max_Health = Minion_Health;
                    Minion_Array[i].GetComponent<Health_Pool>().Current_Health = Minion_Health;
                    Minion_Array[i].SetActive(true);
                    Minion_Array[i].GetComponent<Nest_Dweller_minion>().Destination = Entrance.gameObject;
                    Minion_Array[i].GetComponent<NavMeshAgent>().SetDestination(Entrance.transform.position);
                    Minion_Array[i].GetComponent<Movement_Speed>().Starting_Speed = Minion_Speed;
                    ++Minions_Alive;
                    return;
                }
            }
            else
            {
                CancelInvoke("Activate_Minion");
            }

        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
