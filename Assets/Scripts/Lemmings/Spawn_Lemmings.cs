using UnityEngine;
using System.Collections;

public class Spawn_Lemmings : MonoBehaviour {

    public GameObject[] Lemming_Array = new GameObject[200];
    public GameObject Lemming;

    public int Lemmings_To_Spawn;
    public float Starting_Health;
    int Lemmings_Activated;

    public GameObject Victim;

    public float Spawn_Interval;
    public float Spawn_Time;
    public float Game_Time;



	// Use this for initialization
	void Start () {

        Spawn_Time = Time.time + Spawn_Interval;

        Lemming = Resources.Load<GameObject>("RFLPrefabs/Lemming");

        for (int i=0; i < Lemming_Array.Length; i++)
        {
            Lemming_Array[i] = Instantiate(Lemming, transform.position + transform.forward * 8, transform.rotation) as GameObject;
            Lemming_Array[i].transform.SetParent(gameObject.transform);
            Lemming_Array[i].gameObject.GetComponent<Move_Bitch>().Target = Victim;
            Lemming_Array[i].SetActive(false);

        }
	
	}

    void Activate_Lemmings()
    {
        Lemmings_Activated = 0;

        for (int i = 0; i < Lemming_Array.Length; i++)
        {
            if (Lemming_Array[i].activeInHierarchy == false)
            {
                Lemming_Array[i].transform.position = transform.position + transform.forward * 8;
                Lemming_Array[i].GetComponent<Health_Pool>().Current_Health = Starting_Health;
                Lemming_Array[i].GetComponent<Health_Pool>().Max_Health = Starting_Health;
                Lemming_Array[i].name = "Lemming " + i;
                Lemming_Array[i].SetActive(true);
                Lemming_Array[i].GetComponent<NavMeshAgent>().destination = Victim.transform.position;
                ++Lemmings_Activated;

                if (Lemmings_Activated >= Lemmings_To_Spawn)
                {
                    return;
                }

            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        Game_Time = Time.time;

        if (Game_Time > Spawn_Time)
        {
            Spawn_Time = Game_Time + Spawn_Interval;
            Activate_Lemmings();

        }

    }
}
