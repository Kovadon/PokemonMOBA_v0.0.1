using UnityEngine;
using System.Collections;

public class External_Collider : MonoBehaviour {
    public GameObject Object_Entering_Trigger;
    public GameObject Object_Exiting_Trigger;

    public GameObject Linked_Object;


    // Use this for initialization
    void Start () {
	
	}
	
    void OnTriggerEnter(Collider Col)
    {
        Object_Entering_Trigger = Col.gameObject;
        Linked_Object.GetComponent<Turret_Trigger_Detection>().External_Collider_Enter();
    }

    void OnTriggerExit(Collider Col)
    {
        Object_Exiting_Trigger = Col.gameObject;
        Linked_Object.GetComponent<Turret_Trigger_Detection>().External_Collider_Exit();
    }
}
