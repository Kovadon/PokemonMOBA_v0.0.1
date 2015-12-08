using UnityEngine;
using System.Collections;

public class Test_Attack_Turret : MonoBehaviour
{

    public GameObject Target;

    public Vector3 Attack_Vector;

    float Time1;
    float repeat_time;

    // Use this for initialization
    void Start()
    {
        repeat_time = Time.time + 2;
    }

    public void Attack()
    {

        Target = gameObject.GetComponent<Turret_Trigger_Detection>().Target;

        RaycastHit Ray;
        if (Target != null)
        {
            Attack_Vector = Target.transform.position - transform.position;
        }

        if (Physics.Raycast(transform.position, Attack_Vector, out Ray, 100))
        {
            Debug.DrawLine(transform.position, Ray.point, Color.red);

            if (Time.time > repeat_time)
            {
                if (Target.GetComponent<Health_Pool>().Link_With_External_Object == false)
                {
                    Target.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Current_Health - 5;
                    Target.GetComponent<Health_Pool>().Change_Health();
                    repeat_time = Time.time + .05f;


                    if (Target.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                        // Debug.Log("Dead");

                        Target.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health - 5f;
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Change_Health();
                    repeat_time = Time.time + .05f;

                    if (Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                        // Debug.Log("Dead");

                        Target.gameObject.SetActive(false);
                    }
                }
            }

           
        }
        else
        {
            Debug.DrawLine(transform.position, Ray.point, Color.red);
        }
    }
}
