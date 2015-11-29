using UnityEngine;
using System.Collections;

public class Player_Information : MonoBehaviour {

    public float Health;
    public float Speed;
    public string Team;

	// Use this for initialization
	void Start () {

        Health = 100;
        Speed = 10;
        Team = "Blue";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
