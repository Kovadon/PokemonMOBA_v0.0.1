using UnityEngine;
using System.Collections;

public class Team_Emissive_Color : MonoBehaviour {

    public GameObject Emissive_Object;
    public GameObject Object_With_Team_Script;
    public Renderer Emissive_Material;
    string Old_Team;


	// Use this for initialization
	void Start () {

        Emissive_Material = Emissive_Object.GetComponent<Renderer>();
        Update_Color();
        Old_Team = Object_With_Team_Script.GetComponent<Team>().Team_Name;

    }
	
	// Update is called once per frame
	public void Update_Color () {

        if (Object_With_Team_Script.GetComponent<Team>().Team_Blue == true)
        {
            Emissive_Material.material.SetColor("_EmissionColor", Color.cyan);
        }

        if (Object_With_Team_Script.GetComponent<Team>().Team_Red == true)
        {
            Emissive_Material.material.SetColor("_EmissionColor", Color.red);
        }

        if (Object_With_Team_Script.GetComponent<Team>().Neutral == true)
        {
            Emissive_Material.material.SetColor("_EmissionColor", Color.white);
        }

    }

    void Update()
    {
        if (Old_Team != Object_With_Team_Script.GetComponent<Team>().Team_Name)
        {
            Update_Color();
        }
    }
}
