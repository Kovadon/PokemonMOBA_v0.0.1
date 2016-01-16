using UnityEngine;
using System.Collections;

public class Projectile_Behavior : MonoBehaviour {

    public GameObject Owner;
    public GameObject Target;
    public Vector3 Start_Position;
    public float Speed;
    public float Range;
    public float Damage;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Move_To_Target()
    {

        if (Vector3.Distance(Start_Position, Target.transform.position) < Range)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);
        }
        else
        {
            CancelInvoke("Move_To_Target");
            gameObject.SetActive(false);
        }

        if (Target.GetComponent<Health_Pool>().Current_Health <= 0)
        {
            CancelInvoke("Move_To_Target");
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject == Target.gameObject)
        {
            if (Target.GetComponent<Health_Pool>().Current_Health != 0)
            {
                Target.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Current_Health - Damage;
                Target.GetComponent<Health_Pool>().Change_Health();
                CancelInvoke("Move_To_Target");
                gameObject.SetActive(false);

                if (Target.GetComponent<Health_Pool>().Current_Health <= 0)
                {
                    Target.gameObject.GetComponent<Health_Pool>().HealthBar.GetComponent<Renderer>().enabled = false;
                    Target.gameObject.SetActive(false);
                    Owner.GetComponent<Turret_Trigger_Detection>().Kills = Owner.GetComponent<Turret_Trigger_Detection>().Kills + 1;
                    CancelInvoke("Move_To_Target");
                    gameObject.SetActive(false);
                }
            }
        }
    }

	
	
}
