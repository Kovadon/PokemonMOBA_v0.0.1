using UnityEngine;
using System.Collections;

public class Demo_Stats : MonoBehaviour {

    public float Health;

	// Use this for initialization
	void Start () {

        Health = 100;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Health = Health - 5f;
        }
	
	}
}
