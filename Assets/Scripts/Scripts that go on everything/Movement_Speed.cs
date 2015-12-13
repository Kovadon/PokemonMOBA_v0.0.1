﻿using UnityEngine;
using System.Collections;

public class Movement_Speed : MonoBehaviour {

    //read and write values for speed in an easy to find place, needs to get hooked up to the navmesh agent at some point

    public int Starting_Speed;
    public int Old_Speed;
    public int Current_Speed;


    // Use this for initialization
    public void Start () {

        if (gameObject.GetComponent<NavMeshAgent>() != false)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = Starting_Speed;
        }

        Current_Speed = Starting_Speed;
	
	}

    void Update()
    {
        if (Current_Speed != Starting_Speed)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = Current_Speed;
        }
    }
	
}
