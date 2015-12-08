using UnityEngine;
using System.Collections;

public class Attack_1 : MonoBehaviour {

    public GameObject Target;

    public Vector3 Attack_Vector;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {

            Target = Camera.main.GetComponent<Click_To_Target>().Active_Selected_Target;

            RaycastHit Ray;
            Attack_Vector =  Target.transform.position - transform.position;

            if (Physics.Raycast(transform.position, Attack_Vector, out Ray, 100))
            {
                Debug.DrawLine(transform.position, Ray.point, Color.red);



                if (Target.GetComponent<Health_Pool>().Link_With_External_Object == false)
                {
                    Target.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Current_Health - 5;
                    Target.GetComponent<Health_Pool>().Change_Health();


                    if (Target.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                       // Debug.Log("Dead");

                        Target.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health = Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health - .1f;
                    Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Change_Health();

                    if (Target.GetComponent<Health_Pool>().Linked_GameObject.GetComponent<Health_Pool>().Current_Health <= 0)
                    {
                       // Debug.Log("Dead");

                        Target.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.DrawLine(transform.position, Ray.point, Color.red);
            }

        }
    }
	
}
