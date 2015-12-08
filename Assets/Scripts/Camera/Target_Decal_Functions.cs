using UnityEngine;
using System.Collections;

public class Target_Decal_Functions : MonoBehaviour {

    /*
    public string Target_Team;
    public string Player_Team;
    public GameObject Parent;
    public GameObject HealthBar;

    public Renderer Renderer;

    void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }

	//changes the color and size of the decal that spawns under your target based on the targets size, and team affiliation

	public void NewTarget () {

        gameObject.transform.localScale = new Vector3(Parent.transform.localScale.x / 8, .1f, Parent.transform.localScale.z /8);
        HealthBar.transform.localScale = new Vector3(5, .8f, .6f);
        HealthBar.transform.localPosition = new Vector3(0, 14 , 0);

        HealthBar.GetComponent<Health_Bar_1>().Scale = Parent.transform.localScale.x * 2;
        HealthBar.GetComponent<Health_Bar_1>().Starting_Health = Parent.GetComponent<Health_Pool>().Max_Health;
        HealthBar.GetComponent<Health_Bar_1>().Current_Health = Parent.GetComponent<Health_Pool>().Current_Health;

        if (Target_Team != Player_Team && Target_Team != "Neutral")
        {
            Renderer.material.SetColor("_Color", Color.red);
        }

        if (Target_Team == Player_Team && Target_Team != "Neutral")
        {
            Renderer.material.SetColor("_Color", Color.green);
        }

        if (Target_Team == "Neutral")
        {
            Renderer.material.SetColor("_Color", Color.grey);
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        HealthBar.GetComponent<Health_Bar_1>().Current_Health = Parent.GetComponent<Health_Pool>().Current_Health;
        HealthBar.GetComponent<Health_Bar_1>().DoWork();
        HealthBar.GetComponent<Health_Bar_1>().InvokeRepeating("Check_Health", 0, .1f);
    }
    */
}
