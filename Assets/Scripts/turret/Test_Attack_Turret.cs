using UnityEngine;
using System.Collections;

public class Test_Attack_Turret : MonoBehaviour
{

    public GameObject Target;

    public Vector3 Attack_Vector;

    public float Range;
    public float Damage;
    public float Attack_Interval;

    float Time1;
    float repeat_time;

    // Use this for initialization
    void Start()
    {
        Range = 100f;
        Damage = 10;
        Attack_Interval = .1f;

        repeat_time = Time.time + Attack_Interval;


    }

    public void Attack()
    {

        Target = gameObject.GetComponent<Turret_Trigger_Detection>().Target;

        RaycastHit Ray;
        if (Target != null)
        {
            Attack_Vector = Target.transform.position - transform.position;
        }

        if (Physics.Raycast(transform.position, Attack_Vector, out Ray, Range))
        {
            Debug.DrawLine(transform.position, Ray.point, Color.red);

            if (Time.time > repeat_time)
            {
                if (Target.GetComponent<Health_Pool>().Link_With_External_Object == false)
                {
                    Target.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Current_Health - Damage;
                    Target.GetComponent<Health_Pool>().Change_Health();
                    repeat_time = Time.time + Attack_Interval;


                    if (Target.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                        // Debug.Log("Dead");

                        Target.gameObject.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = false;
                        Target.gameObject.SetActive(false);
                        gameObject.GetComponent<Turret_Trigger_Detection>().Kills = gameObject.GetComponent<Turret_Trigger_Detection>().Kills + 1;
                    }
                }
                else
                {
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health - Damage;
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Change_Health();
                    repeat_time = Time.time + Attack_Interval;

                    if (Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                        // Debug.Log("Dead");

                        Target.gameObject.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = false;
                        Target.gameObject.SetActive(false);
                        gameObject.GetComponent<Turret_Trigger_Detection>().Kills = gameObject.GetComponent<Turret_Trigger_Detection>().Kills + 1;
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
