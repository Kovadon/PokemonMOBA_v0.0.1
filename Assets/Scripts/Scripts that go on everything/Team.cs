using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

    //team affiliation information in an easy to find place

    public bool Team_Red;
    public bool Team_Blue;
    public bool Neutral;

    public string Team_Name;
    string Old_Team;

    public bool Get_From_External_Object;
    public GameObject External_Object;

	// Use this for initialization
	public void Start () {

        if (Get_From_External_Object == true)
        {
            Team_Red = External_Object.GetComponent<Team>().Team_Red;
            Team_Blue = External_Object.GetComponent<Team>().Team_Blue;
            Neutral = External_Object.GetComponent<Team>().Neutral;
        }

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

        Old_Team = Team_Name;
    }

    void Update()
    {

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




        if (Get_From_External_Object == true)
        {
            if (Old_Team != External_Object.GetComponent<Team>().Team_Name)
            {
                Start();
            }
        }
        else
        {
            if (Old_Team != Team_Name)
            {
                Start();
            }
        }
    }
	
}
