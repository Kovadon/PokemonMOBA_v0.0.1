using UnityEngine;
using System.Collections;

public class Smooth_Camera_Follow : MonoBehaviour {



    public GameObject Target;
    public Vector3 New_Position;
    public float Camera_Height;

	// Use this for initialization
	void Start () {

        Camera_Height = 20;

    }

    void Update()
    {
        New_Position = Target.transform.position;
        gameObject.transform.position = new Vector3(New_Position.x, Camera_Height, New_Position.z);

    }

}
