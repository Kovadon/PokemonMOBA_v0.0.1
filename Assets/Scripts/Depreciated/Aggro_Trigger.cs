using UnityEngine;
using System.Collections;
using System.Threading;

public class Aggro_Trigger : MonoBehaviour
{

    public GameObject Hostile_Target;
    public int Mobs_To_Spawn;
    public GameObject[] Mob = null;

    public void StartUp()
    {
        Mobs_To_Spawn = gameObject.GetComponent<Spawn_Mobs>().Mobs_To_Spawn;
        Mob = new GameObject[Mobs_To_Spawn];
    }

    public void OnTriggerEnter(Collider Col)
    {

        if (Col.gameObject.GetComponent<Team>() != null)
            if (Col.gameObject.GetComponent<Team>().Team_Name != gameObject.GetComponent<Nest_Stats>().Team)
            {
                for (int i = 0; i < Mobs_To_Spawn; i++)
                {
                    Mob[i] = gameObject.GetComponent<Spawn_Mobs>().Mobs[i].gameObject;
                    Mob[i].GetComponent<Mob_Movement>().CancelInvoke("FindWayPoint");
                    Mob[i].GetComponent<Mob_Movement>().CancelInvoke("ReturnToNest");
                    Mob[i].GetComponent<Mob_Movement>().CancelInvoke("EnterNest");

                    if (Mob[i].GetComponent<Mob_Movement>().Wrong_Neighborhood == true && Mob[i].transform.position.y < 0)
                    {
                        Mob[i].GetComponent<Mob_Movement>().Invoke("DisableColliders", 0f);
                        Mob[i].transform.position = Mob[i].GetComponent<Mob_Movement>().Spawn_Position;
                        Mob[i].transform.rotation = Mob[i].GetComponent<Mob_Movement>().Spawn_Rotation;
                    }
                    Mob[i].GetComponent<Mob_Movement>().Aggro = true;
                    Mob[i].GetComponent<Mob_Movement>().Returning_To_Front_Door = false;
                    Mob[i].GetComponent<Mob_Movement>().Returning_To_Nest = false;
                    Mob[i].GetComponent<Mob_Movement>().Speed = Random.Range(2.0f, 1.0f);
                    Mob[i].GetComponent<Mob_Movement>().WayPoint = Col.gameObject;
                    Mob[i].GetComponent<Mob_Movement>().InvokeRepeating("MoveToWayPoint", 0f, .01f);
                    Mob[i].GetComponent<Mob_Movement>().Invoke("EnableColliders", Random.Range(1.5f, .1f));
                }
            }


    }


    public void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.GetComponent<Player_Information>() != null)
        {
            if (Col.gameObject.GetComponent<Team>().Team_Name != gameObject.GetComponent<Nest_Stats>().Team)
            {
                for (int i = 0; i < Mobs_To_Spawn; i++)
                {
                    Mob[i] = gameObject.GetComponent<Spawn_Mobs>().Mobs[i].gameObject;
                    Mob[i].GetComponent<Mob_Movement>().Aggro = false;
                    Mob[i].GetComponent<Mob_Movement>().Speed = 1;
                    Mob[i].GetComponent<Mob_Movement>().CancelInvoke("MoveToWayPoint");

                    if (Mob[i].GetComponent<Mob_Movement>().Wrong_Neighborhood == false)
                    {
                        Mob[i].GetComponent<Mob_Movement>().InvokeRepeating("FindWayPoint", 0f, Random.Range(10f, 5f));
                        Mob[i].GetComponent<Mob_Movement>().InvokeRepeating("MoveToWayPoint", 0f, .01f);
                    }
                    else
                    {
                        Mob[i].GetComponent<Mob_Movement>().Returning_To_Front_Door = true;
                        Mob[i].GetComponent<Mob_Movement>().InvokeRepeating("ReturnToNest", 1f, .01f);
                    }
                }
            }
        }
    }



}
