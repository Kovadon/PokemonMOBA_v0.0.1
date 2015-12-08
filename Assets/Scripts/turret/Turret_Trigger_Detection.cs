using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret_Trigger_Detection : MonoBehaviour {

    //MUCH more straightforward turret script, handles mutiple mob much better and i a lot quicker as well. 
    //hook up your attack scripts in Pick_A_Target()

    //information about the object being rotated by the script
    public GameObject Model_To_Rotate;
    public Quaternion Resting_Rotation;
    public Quaternion Target_Rotation;
    float Rotation_Speed;

    // list of objects that enter the turrets range of effect
    public List<GameObject> ListOfTargets = new List<GameObject>();
    public int Objects_In_Range;

    // information about the target
    public GameObject Target;
    public GameObject Object_Entering_Trigger;
    public GameObject Object_Exiting_Trigger;
    public float Target_Distance;

    // set this in the editor, will determine what layers will trigger the effects of this script
    public LayerMask LayerMask;


    public string Team;

    public bool Draw_RayCast;

    // use this if you cant place the trigger collider on the object you want to rotate
    public bool Use_External_Trigger;
    public GameObject External_Collider_Object;




    void Start()
    {
        Resting_Rotation = Model_To_Rotate.transform.rotation;
        Rotation_Speed = gameObject.GetComponent<Movement_Speed>().Current_Speed;
        Team = gameObject.GetComponent<Team>().Team_Name;
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Use_External_Trigger == false)
        {
            Object_Entering_Trigger = Col.gameObject;

            if (Object_Entering_Trigger.GetComponent<Team>().Team_Name != Team)
            {
                ListOfTargets.Add(Object_Entering_Trigger);

                if (Objects_In_Range < ListOfTargets.Count)
                {
                    ++Objects_In_Range;
                    ListOfTargets[Objects_In_Range - 1] = Object_Entering_Trigger;
                    CancelInvoke("Disengage");
                    Pick_A_Target();
                }

            }
        }
    }

    public void External_Collider_Enter()
    {
        Object_Entering_Trigger = External_Collider_Object.GetComponent<External_Collider>().Object_Entering_Trigger;

        if (Object_Entering_Trigger.GetComponent<Team>().Team_Name != Team)
        {
            ListOfTargets.Add(Object_Entering_Trigger);

            if (Objects_In_Range < ListOfTargets.Count)
            {
                ++Objects_In_Range;
                CancelInvoke("Disengage");
                ListOfTargets[Objects_In_Range - 1] = Object_Entering_Trigger;
                Pick_A_Target();
            }
        }
    }

    public void Pick_A_Target()
    {
        CancelInvoke("EngageTarget");
        //hook up your attacks in here
        gameObject.GetComponent<Test_Attack_Turret>().CancelInvoke("Attack");

        if (Objects_In_Range != 0)
        {
            for (int i = 0; i < Objects_In_Range; i++)
            {
                if (ListOfTargets[i].gameObject.activeSelf == true)
                {
                    Target = ListOfTargets[i].gameObject;
                }
                else
                {
                    ListOfTargets.Remove(ListOfTargets[i].gameObject);
                    Objects_In_Range = Objects_In_Range - 1;
                    Pick_A_Target();
                }

                if (Target.gameObject.activeSelf == true)
                {
                    InvokeRepeating("EngageTarget", 0f, .01f);
                    gameObject.GetComponent<Test_Attack_Turret>().InvokeRepeating("Attack", 0f, .1f);
                }

                return;
            }

        }
        else
        {
            InvokeRepeating("Disengage", 0f, .01f);
        }
    }

    void EngageTarget()
    {
        if (Target.activeInHierarchy == false)
        {
            Pick_A_Target();
        }

        if (Target.activeInHierarchy == true)
        {
            Target_Rotation = Quaternion.LookRotation(Target.transform.position - Model_To_Rotate.transform.position);
            Model_To_Rotate.transform.rotation = Quaternion.Slerp(Model_To_Rotate.transform.rotation, Target_Rotation, Time.deltaTime * Rotation_Speed);

            RaycastHit Ray;
            if (Physics.Raycast(Model_To_Rotate.transform.position, Model_To_Rotate.transform.forward, out Ray, 100, LayerMask))
            {
                Target_Distance = Ray.distance;
                if (Draw_RayCast == true)
                {
                    Debug.DrawLine(Model_To_Rotate.transform.position, Target.transform.position, Color.red);
                }
            }
        }


    }

    void Disengage()
    {
        if (Model_To_Rotate.transform.rotation != Resting_Rotation)
        {
            Model_To_Rotate.transform.rotation = Quaternion.Lerp(Model_To_Rotate.transform.rotation, Resting_Rotation, Time.deltaTime * Rotation_Speed);
        }
    }

    public void External_Collider_Exit()
    {
        --Objects_In_Range;
        Object_Exiting_Trigger = External_Collider_Object.GetComponent<External_Collider>().Object_Entering_Trigger;
        ListOfTargets.Remove(Object_Exiting_Trigger);
        Pick_A_Target();


    }

    void OnTriggerExit(Collider Col)
    {
        if (Use_External_Trigger == false)
        {
            --Objects_In_Range;
            Object_Exiting_Trigger = Col.gameObject;
            ListOfTargets.Remove(Object_Exiting_Trigger);
            Pick_A_Target();
        }
    }





}
