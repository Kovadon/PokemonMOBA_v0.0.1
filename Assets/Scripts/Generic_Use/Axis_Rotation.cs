using UnityEngine;
using System.Collections;

public class Axis_Rotation : MonoBehaviour {

    public bool Up;
    public bool Forward;
    public bool Right;
    public bool Negative;
    public float Speed;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Negative != true)
        {
            if (Up == true)
            {
                transform.RotateAround(transform.position, transform.up, Time.deltaTime * Speed);
            }

            if (Forward == true)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * Speed);
            }

            if (Right == true)
            {
                transform.RotateAround(transform.position, transform.right, Time.deltaTime * Speed);
            }
        }

        if (Negative == true)
        {
            if (Up == true)
            {
                transform.RotateAround(transform.position, -transform.up, Time.deltaTime * Speed);
            }

            if (Forward == true)
            {
                transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * Speed);
            }

            if (Right == true)
            {
                transform.RotateAround(transform.position, -transform.right, Time.deltaTime * Speed);
            }
        }



    }
}
