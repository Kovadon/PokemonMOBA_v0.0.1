using UnityEngine;
using System.Collections;

public class Mob_Movement : MonoBehaviour {

    public Quaternion TargetRotation;
    public GameObject WayPoint;
    public GameObject Front_Door;
    public int WayPoint_Number;
    public GameObject Nest;
    public GameObject Mob;
    public Rigidbody rb;

    public Vector3 Spawn_Position;
    public Quaternion Spawn_Rotation;

    public float WayPoint_Spread;

    public float Speed;
    public bool Aggro;

    public bool Respawn_WaypPint_Post_Collision;
    public bool Gatherer_Behavior;
    public bool Wrong_Neighborhood;
    public bool Returning_To_Front_Door;
    public bool Returning_To_Nest;
    public bool Just_Spawned;

    void Start()
    {

        Speed = 1;

        FindWayPoint();

        if (Wrong_Neighborhood == false)
        {
            InvokeRepeating("LeaveNest", 0f, .01f);
        }


        Mob = gameObject;
        rb = Mob.GetComponent<Rigidbody>();


    }

    public void FindWayPoint()
    {
        WayPoint_Number = Random.Range(0, 9);
        WayPoint = Nest.GetComponent<Spawn_WayPoints>().WayPoints[WayPoint_Number].gameObject;
    }

    public void LeaveNest()
    {
        Just_Spawned = true;
        
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Front_Door.transform.position, Time.deltaTime * Speed);


    }

    public void ReturnToNest()
    {

        if (gameObject.transform.position != Front_Door.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Front_Door.transform.position, Time.deltaTime * Speed);
            TargetRotation = Quaternion.LookRotation(Front_Door.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, (Speed * 3) * Time.deltaTime);
        }
    }

    public void EnterNest()
    {

            if (gameObject.transform.position != Nest.transform.position)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Nest.transform.position, Time.deltaTime * Speed);
                TargetRotation = Quaternion.LookRotation(Nest.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, (Speed * 3) * Time.deltaTime);
            }
       } 

    public void EnableColliders()
    {
        Mob.GetComponent<BoxCollider>().isTrigger = false;
        rb.isKinematic = false;
    }

    public void DisableColliders()
    {
        Mob.GetComponent<BoxCollider>().isTrigger = true;
        rb.isKinematic = true;
    }

    public void MoveToWayPoint()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, WayPoint.transform.position, Time.deltaTime * Speed);
        TargetRotation = Quaternion.LookRotation(WayPoint.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, (Speed * 3) * Time.deltaTime);
    }
    
    public void UnStuck()
    {
        DisableColliders();
        Invoke("EnableColliders", 2f);
    }

    public void ResetMob()
    {
        CancelInvoke("EnterNest");
        CancelInvoke("FindWayPoint");
        CancelInvoke("ReturnToNest");
        CancelInvoke("MoveToWayPoint");
        Returning_To_Nest = false;

        if (Gatherer_Behavior == true)
        {
            ++Nest.GetComponent<Nest_Stats>().Resources;
        }

        DisableColliders();
        gameObject.transform.position = new Vector3(Nest.transform.position.x, .3f, Nest.transform.position.z);
        FindWayPoint();

        if (Wrong_Neighborhood == false)
        {
            InvokeRepeating("LeaveNest", .5f, .01f);
        }
    }

    public void WayPoint_Collision_Behavior()
    {
        CancelInvoke("MoveToWayPoint");

        if (Respawn_WaypPint_Post_Collision == true)
        {
            WayPoint.transform.position = new Vector3(Nest.transform.position.x + Random.Range(WayPoint_Spread, 1), .15f, Nest.transform.position.z + Random.Range(WayPoint_Spread, 1));
        }

        if (Gatherer_Behavior == true)
        {
            InvokeRepeating("ReturnToNest", 1f, .01f);
            Returning_To_Front_Door = true;
        }
        else
        {
            FindWayPoint();
            InvokeRepeating("MoveToWayPoint", 1f, .01f);
        }
    }

    void OnCollisionEnter(Collision Col)
    {

        if (Col.gameObject.name == Nest.gameObject.name && Returning_To_Nest == true && Returning_To_Front_Door == false)
        {
            ResetMob();
        }

        if (Col.gameObject.name == Nest.gameObject.name)
        {
            UnStuck();
        }






        if (Col.gameObject.name == WayPoint.gameObject.name && Aggro == false)
            {
            WayPoint_Collision_Behavior();

            }
        
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.name == "Front_Door" && Returning_To_Front_Door == true)
        {
            Returning_To_Nest = true;
            Returning_To_Front_Door = false;
            CancelInvoke("ReturnToNest");
            InvokeRepeating("EnterNest", .2f, .01f);
        }

        if (Col.gameObject.name == "Front_Door" && Just_Spawned == true)
        {

            Debug.Log("enter trigger");
            CancelInvoke("LeaveNest");
            FindWayPoint();
            InvokeRepeating("MoveToWayPoint", 0f, .01f);
            Invoke("EnableColliders", .5f);
            Just_Spawned = false;
        }
    }

}
