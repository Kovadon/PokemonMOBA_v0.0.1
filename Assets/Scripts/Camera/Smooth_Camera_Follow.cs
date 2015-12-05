using UnityEngine;
using System.Collections;

public class Smooth_Camera_Follow : MonoBehaviour {

    public GameObject Target;
    public Vector3 Old_Position;
    public Vector3 New_Position;
    public float Camera_Height;

	// Use this for initialization
	void Start () {

        Camera_Height = 20;
        Target.transform.position = Old_Position;
        InvokeRepeating("Check_For_Changes", 0f, .01f);
        InvokeRepeating("Follow", 0f, .01f);

    }

    public void Follow()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(New_Position.x, Camera_Height, New_Position.z), 10);

    }
	
    public void Check_For_Changes()
    {
        New_Position = Target.transform.position;

        if (New_Position != Old_Position)
        {
            Old_Position = New_Position;
        }
    }
}
