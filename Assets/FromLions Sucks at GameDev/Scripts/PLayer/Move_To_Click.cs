using UnityEngine;
using System.Collections;

public class Move_To_Click : MonoBehaviour {

    public Vector3 Location;
    public Vector3 Destination;
    public float Speed;
    public GameObject Camera;

    public Quaternion Target_Rotation;


    // Use this for initialization
    void Start () {

        Speed = gameObject.GetComponent<Player_Information>().Speed;
	
	}

    void SmoothMove()
    {
        gameObject.transform.position = Vector3.MoveTowards(Location, Destination, Time.deltaTime * Speed);

        Target_Rotation = Quaternion.LookRotation(Destination - transform.position);

        if (transform.rotation != Target_Rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);
        }
        

        if (Location == Destination)
        {
            CancelInvoke("SmoothMove");
        }

    }


	// Update is called once per frame
	void Update () {


        Location = gameObject.transform.position;
        Speed = gameObject.GetComponent<Player_Information>().Speed;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CancelInvoke("SmoothMove");
            Destination = new Vector3(Camera.GetComponent<Click_To_Move>().Destination.x, Camera.GetComponent<Click_To_Move>().Destination.y + gameObject.transform.position.y, Camera.GetComponent<Click_To_Move>().Destination.z);
            InvokeRepeating("SmoothMove", 0f, .01f);
        }

       
	
	}
}
