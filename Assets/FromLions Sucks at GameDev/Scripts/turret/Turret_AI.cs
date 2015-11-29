using UnityEngine;
using System.Collections;

public class Turret_AI : MonoBehaviour {

    public string Team;
    public Quaternion Resting_Rotation;
    public Quaternion Target_Rotation;
    public float Speed;

    public GameObject Target;
    public int layerMask_Int;
    public int LayerMask;

    public float Distance;

    // Use this for initialization
    void Start () {

        Team = gameObject.GetComponent<Turret_Information>().Team;
        Resting_Rotation = gameObject.transform.rotation;
        Speed = 5;


    }


	
	// Update is called once per frame
	void Update () {
        Team = gameObject.GetComponent<Turret_Information>().Team;

    }

    void OnTriggerEnter(Collider Col)
    {
        Debug.Log("triggering intensifies!");
        if (Col.gameObject.GetComponent<Player_Information>().Team != null)
        {
            if (Col.gameObject.GetComponent<Player_Information>().Team != Team)
            {
               Target = Col.gameObject;
               Target.layer = layerMask_Int;
               LayerMask = 1 << layerMask_Int;

               InvokeRepeating("EngageTarget", 0f, 0.01f);

            }
        }

    }

    void OnTriggerExit(Collider Col)
    {
        CancelInvoke("EngageTarget");
        InvokeRepeating("Disengage", 0.01f, .01f);
    }

    void EngageTarget()
    {
        CancelInvoke("Disengage");
        Target_Rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
         //gameObject.transform.LookAt(Target.transform);
         transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);

        RaycastHit Ray;
        if (Physics.Raycast(transform.position, transform.forward, out Ray, 100, LayerMask))
        {
            Distance = Ray.distance;
            Debug.DrawLine(transform.position, Target.transform.position, Color.red);
        }


    }

    void Disengage()
    {
       gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Resting_Rotation, Time.deltaTime * Speed);

        if (gameObject.transform.rotation == Resting_Rotation)
        {
            CancelInvoke("Disengage");
        }
    }
}

