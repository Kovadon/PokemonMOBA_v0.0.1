using UnityEngine;
using System.Collections;

public class Turret_Trigger_Detection : MonoBehaviour {

    public GameObject Targetable_Object;
    public GameObject Model_To_Rotate;
    public GameObject Target;
    public LayerMask LayerMask;

    public string Team;


    void OnTriggerEnter(Collider Col)
    {
        Team = Targetable_Object.gameObject.GetComponent<Team>().Team_Name;
        if (Col.gameObject.GetComponent<Team>().Team_Name != null)
        {
            if (Col.gameObject.GetComponent<Team>().Team_Name != Team)
            {
                gameObject.GetComponent<Turret_AI>().Target = Col.gameObject;
                Target = Col.gameObject;
                gameObject.GetComponent<Turret_AI>().InvokeRepeating("EngageTarget", 0f, 0.01f);
            }

        }

    }

    void OnTriggerExit(Collider Col)
    {
        gameObject.GetComponent<Turret_AI>().CancelInvoke("EngageTarget");
        gameObject.GetComponent<Turret_AI>().InvokeRepeating("Disengage", 0.01f, .01f);
    }
}
