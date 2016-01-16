using UnityEngine;
using System.Collections;

public class Spawn_WayPoints : MonoBehaviour {
    public GameObject Nest;
    public GameObject WayPoint_Prefab;
    public GameObject[] WayPoints =  null;
    public GameObject Front_Door;
    public int WayPoints_To_Spawn;
    int WayPoints_Spawned;
    public float WayPoint_Spread;

    public Vector3 Spawn_Vector;




    void Start()
    {

        Nest = gameObject;
        Spawn_Vector = (transform.position + transform.forward * 2);



        WayPoint_Spread = 10;
        WayPoints_To_Spawn = 10;
        WayPoints = new GameObject[WayPoints_To_Spawn];

        Front_Door = Instantiate(Resources.Load("WayPoint"), new Vector3(Spawn_Vector.x, .25f, Spawn_Vector.z), transform.rotation) as GameObject;
        Front_Door.transform.SetParent(Nest.transform);
        Front_Door.name = "Front_Door";
        Front_Door.GetComponent<BoxCollider>().isTrigger = true;
        Front_Door.GetComponent<Renderer>().enabled = false;

        StartCoroutine("Spawn_Everything");
    }


    public IEnumerator Spawn_Everything()
    {
        if (WayPoints_Spawned < WayPoints_To_Spawn)
        {
            for (int i = 0; i < WayPoints_To_Spawn; i++)
            {
                WayPoints[i] = Instantiate(Resources.Load("WayPoint") as GameObject);
                WayPoints[i].name = Nest.name + "_" + "WayPoint_" + i;
                WayPoints[i].transform.SetParent(Nest.transform);
                WayPoints[i].transform.rotation = Quaternion.FromToRotation(Vector3.forward, transform.forward);
                WayPoints[i].transform.position = new Vector3(Nest.transform.position.x + Random.Range(WayPoint_Spread, 1), .15f, Nest.transform.position.z + Random.Range(WayPoint_Spread, 1));
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(gameObject.GetComponent<Spawn_Mobs>().Spawn_a_Mob());
            gameObject.GetComponent<Aggro_Trigger>().Invoke("StartUp", 5f);
            yield return null;
        }

    }

    public void ScatterWayPoints()
    {
        for (int i = 0; i < WayPoints_To_Spawn; i++)
        {
            WayPoints[i].transform.rotation = Quaternion.FromToRotation(Vector3.forward, transform.forward);
            WayPoints[i].transform.position = new Vector3(Nest.transform.position.x + Random.Range(WayPoint_Spread, 1), .15f, Nest.transform.position.z + Random.Range(WayPoint_Spread, 1));
            WayPoints[i].SetActive(true);
        }

    }

    public void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.name == "WayPoint" || Col.gameObject.name == "WayPoint(Clone)")
        {
            Col.transform.position = new Vector3(Nest.transform.position.x + Random.Range(WayPoint_Spread, 1), .15f, Nest.transform.position.z + Random.Range(WayPoint_Spread, 1));
        }
    }


}
