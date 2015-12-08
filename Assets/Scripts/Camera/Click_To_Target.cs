using UnityEngine;
using System.Collections;

public class Click_To_Target : MonoBehaviour
{

    public Camera Camera;
    public LayerMask Targeting_Layermask;

    public GameObject Active_Selected_Target;
    public GameObject Previous_Target;
    public GameObject MouseOver_Target;

    public float Distance_From_Target;


    void Start()
    {

    }



    public void GetTarget()
    {
        Ray Target = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit TargetHit = new RaycastHit();
        if (Physics.Raycast(Target, out TargetHit, 1000, Targeting_Layermask))
        {
            if (TargetHit.collider.gameObject != Active_Selected_Target)
            {
                if (Previous_Target != null)
                {
                    Previous_Target = Active_Selected_Target;
                    Previous_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<HealthBar_2>().CancelInvoke("Update_Healthbar");
                    Active_Selected_Target = TargetHit.collider.gameObject;
                    Active_Selected_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = true;
                    Active_Selected_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<HealthBar_2>().InvokeRepeating("Update_Healthbar", 0f, .01f);

                }
                else
                {
                    Active_Selected_Target = TargetHit.collider.gameObject;
                    Previous_Target = Active_Selected_Target;
                    Active_Selected_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = true;
                    Active_Selected_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<HealthBar_2>().InvokeRepeating("Update_Healthbar", 0f, .01f);
                }

            }

        }

    }



    //same as above but happens on mouseover


    public void MouseOverTarget()
    {
        Ray Target = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit TargetHit = new RaycastHit();
        if (Physics.Raycast(Target, out TargetHit, 1000, Targeting_Layermask))
        {
            if (TargetHit.collider.gameObject != Active_Selected_Target)
            {
                MouseOver_Target = TargetHit.collider.gameObject;
                MouseOver_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = true;
                MouseOver_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<HealthBar_2>().Update_Healthbar();
            }
            else
            {

            }
        }
        else
        {
            if (MouseOver_Target != null)
            {
                if (MouseOver_Target != Active_Selected_Target)
                {
                    MouseOver_Target.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = false;
                    MouseOver_Target = null;
                }
            }
        }

    }

}
