using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

    public bool Team_Red;
    public bool Team_Blue;
    public bool Neutral;

    public string Team_Name;

	// Use this for initialization
	public void Start () {

        if (Team_Red == true)
        {
            Team_Name = "Red";
            Neutral = false;
        }

        if (Team_Blue == true)
        {
            Team_Name = "Blue";
            Neutral = false;
        }

        if (Neutral == true)
        {
            Team_Name = "Neutral";
        }

    }
	
}
