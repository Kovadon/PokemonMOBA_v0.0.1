using UnityEngine;
using System.Collections;

public class Proceedural_Decorations : MonoBehaviour {

    public GameObject Prop1;
    public GameObject Prop2;
    public GameObject Prop3;
    public GameObject Prop4;
    public GameObject Prop5;
    public GameObject Prop6;



    public GameObject[] WayPoint_Array = null;


    public int Props_To_Spawn;
    public float Range;
    Vector3 Spawn_Vector;



    // Use this for initialization
    void Start () {

        WayPoint_Array = new GameObject[Props_To_Spawn];

        for (int i = 0; i < WayPoint_Array.Length; i++)
        {
            Spawn_Vector = new Vector3(Random.Range(Range, -Range), transform.localScale.y / 2, Random.Range(Range, -Range));
            WayPoint_Array[i] = Instantiate(Prop1, Spawn_Vector, transform.rotation) as GameObject;
            WayPoint_Array[i].transform.SetParent(gameObject.transform);
            WayPoint_Array[i].name = "Waypoint_" + i;
        }

        for (int i = 0; i < WayPoint_Array.Length; i++)
        {
            Spawn_Vector = new Vector3(Random.Range(Range, -Range), transform.localScale.y / 2, Random.Range(Range, -Range));
            WayPoint_Array[i] = Instantiate(Prop2, Spawn_Vector, transform.rotation) as GameObject;
            WayPoint_Array[i].transform.SetParent(gameObject.transform);
            WayPoint_Array[i].name = "Waypoint_" + i;
        }

        for (int i = 0; i < WayPoint_Array.Length; i++)
        {
            Spawn_Vector = new Vector3(Random.Range(Range, -Range), transform.localScale.y / 2, Random.Range(Range, -Range));
            WayPoint_Array[i] = Instantiate(Prop3, Spawn_Vector, transform.rotation) as GameObject;
            WayPoint_Array[i].transform.SetParent(gameObject.transform);
            WayPoint_Array[i].name = "Waypoint_" + i;
        }

        for (int i = 0; i < WayPoint_Array.Length; i++)
        {
            Spawn_Vector = new Vector3(Random.Range(Range, -Range), transform.localScale.y / 2, Random.Range(Range, -Range));
            WayPoint_Array[i] = Instantiate(Prop4, Spawn_Vector, transform.rotation) as GameObject;
            WayPoint_Array[i].transform.SetParent(gameObject.transform);
            WayPoint_Array[i].name = "Waypoint_" + i;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
