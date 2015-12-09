using UnityEngine;
using System.Collections;

public class Camera_Follow_And_Rotate : MonoBehaviour {

    public GameObject Player;
    public GameObject Camera;
    public Vector3 Camera_Positioning;
    public float Speed;

    Vector3 Above_Player;

	// Use this for initialization
	void Start () {

        Camera_Positioning = new Vector3(0, 20, 20);
        Speed = 10;

    }
	
	// Update is called once per frame
	void Update () {

        Camera.transform.LookAt(Player.transform);
        Above_Player = new Vector3(Player.transform.position.x, Player.transform.position.y + 30, Player.transform.position.z);


        if (Input.GetMouseButtonUp(2))
        {
            Camera_Positioning = Camera.transform.position - Player.transform.position;
        }


        if (Input.GetMouseButton(2))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Speed);
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Above_Player, Input.GetAxis("Mouse Y") * Speed);
        }
        else
        {
            Camera.transform.position = Player.transform.position + Camera_Positioning;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, (Camera.transform.position - Player.transform.position) * 100, 10);
            Camera_Positioning = Camera.transform.position - Player.transform.position;

        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
           Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Player.transform.position, 10);
           Camera_Positioning = Camera.transform.position - Player.transform.position;
        }

    }
}
