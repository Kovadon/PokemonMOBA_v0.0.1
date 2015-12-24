using UnityEngine;
using System.Collections;

public class Spawn_Mobs : MonoBehaviour {

    public GameObject Nest;
    public Rigidbody rb;

    public GameObject[] Mobs = null;
    public int Mobs_Alive;
    public int Mobs_To_Spawn;

    IEnumerator Spawn_A_Mob;



    // Use this for initialization
    void Start()
    {

        Nest = gameObject;

        Mobs_To_Spawn = 5;

        Mobs = new GameObject[Mobs_To_Spawn];



    }



  public IEnumerator Spawn_a_Mob ()
    {
        for (int i = 0; i < Mobs_To_Spawn; i++)
        {
            if (Mobs_Alive < Mobs_To_Spawn)
            {
                Mobs[i] = Instantiate(Resources.Load("Mob"), new Vector3(transform.position.x, .3f, transform.position.z), transform.rotation) as GameObject;
                Mobs[i].GetComponent<Mob_Movement>().Nest = Nest;
                Mobs[i].transform.SetParent(Nest.transform);
                Mobs[i].GetComponent<Mob_Movement>().Spawn_Position = Mobs[i].transform.position;
                Mobs[i].GetComponent<Mob_Movement>().Spawn_Rotation = Mobs[i].transform.rotation;
                Mobs[i].GetComponent<Mob_Movement>().WayPoint_Spread = Nest.GetComponent<Spawn_WayPoints>().WayPoint_Spread;
                Mobs[i].GetComponent<Mob_Movement>().Front_Door = Nest.GetComponent<Spawn_WayPoints>().Front_Door;
                rb = Mobs[i].GetComponent<Rigidbody>();
                rb.isKinematic = true;
                Mobs[i].GetComponent<BoxCollider>().isTrigger = true;
                ++Mobs_Alive;
                yield return new WaitForSeconds(1);


            }
        }



    }




	


}
