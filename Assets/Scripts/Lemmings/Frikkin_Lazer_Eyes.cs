using UnityEngine;
using System.Collections;

public class Frikkin_Lazer_Eyes : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit EyeBeam;
            if (Physics.Raycast(transform.position, transform.position - -transform.up, out EyeBeam, 5))
        {
            Debug.DrawLine(transform.position, transform.position - -transform.up * 5, Color.magenta);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - -transform.up * 5, Color.magenta);
        }

    }
}
