using UnityEngine;
using System.Collections;

public class Bokeh_Companion : MonoBehaviour {
    public GameObject DOF_Target;
    public Transform target;
    public int layerMask = 1 << 10;


    // Use this for initialization
    void Start () {
        DOF_Target = GameObject.Find("DOF_Target");
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        RaycastHit DOF_Ray;
        if (Physics.Raycast(transform.position, transform.forward, out DOF_Ray, 1000, ~layerMask))
        {
            DOF_Target.transform.position = DOF_Ray.point;
        }
        else
        {
            DOF_Target.transform.position = gameObject.transform.position + transform.forward * 100;
        }
	
	}
}
