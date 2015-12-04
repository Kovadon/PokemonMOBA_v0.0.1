using UnityEngine;
using System.Collections;

public class Click_To_Target : MonoBehaviour
{

    public Camera Camera;
    public LayerMask Targeting_Layermask;

    //public string Target_Name;

    public GameObject Target_Decal;

    void Start()
    {
        Target_Decal.GetComponent<Renderer>().enabled = false;
    }


    public void GetTarget ()
    {
        Ray Target = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit TargetHit = new RaycastHit();
        if (Physics.Raycast(Target, out TargetHit, 1000, Targeting_Layermask))
        {
            //Target_Name = TargetHit.collider.gameObject.name;
            Target_Decal.GetComponent<Renderer>().enabled = true;

            Target_Decal.transform.position = new Vector3(TargetHit.transform.position.x, .02f, TargetHit.transform.position.z);
            Target_Decal.gameObject.transform.SetParent(TargetHit.transform);

            Target_Decal.GetComponent<Target_Decal_Functions>().Parent = TargetHit.collider.gameObject;
            Target_Decal.GetComponent<Target_Decal_Functions>().Target_Team = TargetHit.collider.gameObject.GetComponent<Team>().Team_Name;
            Target_Decal.GetComponent<Target_Decal_Functions>().Player_Team = Camera.GetComponent<Team>().Team_Name;
            Target_Decal.GetComponent<Target_Decal_Functions>().NewTarget();


        }
    }

}
