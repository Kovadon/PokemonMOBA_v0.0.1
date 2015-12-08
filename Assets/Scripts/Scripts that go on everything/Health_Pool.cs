using UnityEngine;
using System.Collections;

public class Health_Pool : MonoBehaviour
{

    public float Max_Health;
    public float Current_Health;


    public GameObject HealthBar;
    float New_Control_Number;

    public bool Link_With_External_Object;
    public GameObject Linked_GameObject;

    public bool Visible_On_Start;

    void Start()
    {

        if (Link_With_External_Object == false)
        {
            if (gameObject.GetComponent<Renderer>() != null)
            {
                HealthBar = Instantiate(Resources.Load("RFLPrefabs/Healthbar"), new Vector3(transform.position.x, gameObject.GetComponent<Renderer>().bounds.size.y + 1, transform.position.z), transform.rotation) as GameObject;
            }
            else
            {
                HealthBar = Instantiate(Resources.Load("RFLPrefabs/Healthbar"), new Vector3(transform.position.x, gameObject.GetComponent<Collider>().bounds.center.y * 2 + 1, transform.position.z), transform.rotation) as GameObject;
            }
            HealthBar.transform.SetParent(gameObject.transform);

            if (Visible_On_Start != true)
            {
                HealthBar.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                HealthBar.GetComponent<Renderer>().enabled = true;
                HealthBar.gameObject.GetComponent<HealthBar_2>().InvokeRepeating("Update_Healthbar", 0f, .01f);
            }

        }
    }

    void Update()
    {
        if (Link_With_External_Object == true)
        {
            HealthBar = Linked_GameObject.GetComponent<Health_Pool>().HealthBar;
            Max_Health = Linked_GameObject.GetComponent<Health_Pool>().Max_Health;
            Current_Health = Linked_GameObject.GetComponent<Health_Pool>().Current_Health;
        }
    }


    public void Change_Health()
    {
        New_Control_Number = Current_Health / Max_Health;

        HealthBar.GetComponent<HealthBar_2>().Control_Number = New_Control_Number;
        HealthBar.GetComponent<HealthBar_2>().Update_Healthbar();



    }




}
