using UnityEngine;
using System.Collections;

public class Click_To_Target : MonoBehaviour
{

    public Camera Camera;
    public LayerMask Targeting_Layermask;

    public GameObject Target_Decal;
    public GameObject Mouse_Over_Decal;

    public GameObject Old_Mouse_Over_Target;
    public GameObject Current_Mouse_Over_Target;


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
            Target_Decal.GetComponent<Renderer>().enabled = true;
            Target_Decal.GetComponent<Target_Decal_Functions>().HealthBar.gameObject.GetComponent<Renderer>().enabled = true;

            Target_Decal.GetComponent<Renderer>().enabled = true;

            Target_Decal.transform.position = new Vector3(TargetHit.transform.position.x, .02f, TargetHit.transform.position.z);
            Target_Decal.gameObject.transform.SetParent(TargetHit.transform);

            Target_Decal.GetComponent<Target_Decal_Functions>().Parent = TargetHit.collider.gameObject;
            Target_Decal.GetComponent<Target_Decal_Functions>().Target_Team = TargetHit.collider.gameObject.GetComponent<Team>().Team_Name;
            Target_Decal.GetComponent<Target_Decal_Functions>().Player_Team = Camera.GetComponent<Team>().Team_Name;
            Target_Decal.GetComponent<Target_Decal_Functions>().NewTarget();


        }
        else
        {
            Target_Decal.GetComponent<Renderer>().enabled = true;
            Target_Decal.GetComponent<Target_Decal_Functions>().HealthBar.gameObject.GetComponent<Renderer>().enabled = true;
        }

    }




    public void MouseOverTarget()
    {
        Ray Target = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit TargetHit = new RaycastHit();
        if (Physics.Raycast(Target, out TargetHit, 1000, Targeting_Layermask))
        {
            Current_Mouse_Over_Target = TargetHit.collider.gameObject;

            if (Current_Mouse_Over_Target != null)
            {

                if (Current_Mouse_Over_Target != Old_Mouse_Over_Target)
                {
                    Old_Mouse_Over_Target = Current_Mouse_Over_Target;
                    Mouse_Over_Decal.GetComponent<Renderer>().enabled = true;
                    Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().HealthBar.gameObject.GetComponent<Renderer>().enabled = true;

                    Mouse_Over_Decal.transform.position = new Vector3(TargetHit.transform.position.x, .02f, TargetHit.transform.position.z);
                    Mouse_Over_Decal.gameObject.transform.SetParent(TargetHit.transform);

                    Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().Parent = TargetHit.collider.gameObject;
                    Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().Target_Team = TargetHit.collider.gameObject.GetComponent<Team>().Team_Name;
                    Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().Player_Team = Camera.GetComponent<Team>().Team_Name;
                    Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().NewTarget();
                }
                else
                {
                    if (Mouse_Over_Decal.GetComponent<Renderer>().enabled == false)
                    {
                        Mouse_Over_Decal.GetComponent<Renderer>().enabled = true;
                        Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().HealthBar.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }

        }
        else
        {
            if (Mouse_Over_Decal.GetComponent<Renderer>().enabled != false)
            {
                Mouse_Over_Decal.GetComponent<Renderer>().enabled = false;
                Mouse_Over_Decal.GetComponent<Target_Decal_Functions>().HealthBar.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }

    }

}
