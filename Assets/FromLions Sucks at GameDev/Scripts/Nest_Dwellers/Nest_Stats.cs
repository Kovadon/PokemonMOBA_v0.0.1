using UnityEngine;
using System.Collections;

public class Nest_Stats : MonoBehaviour {

    public int WayPoints_To_Spawn;
    public float WayPoint_Spread;
    public string Team;
    public int Health;
    public float Influence;
    public int Resources;

	// Use this for initialization
	void Start () {

        WayPoints_To_Spawn = 5;
        WayPoint_Spread = 5;
        Team = "Red";
	
	}
	
}
