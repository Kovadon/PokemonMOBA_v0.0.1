using UnityEngine;
using System.Collections;

public class Turret_AI : MonoBehaviour {

    public Quaternion Resting_Rotation;
    public Quaternion Target_Rotation;
    public float Speed;

    public GameObject Target;
    public GameObject Model_To_Rotate;

    public float Distance;

    // Use this for initialization
    void Start () {

        Model_To_Rotate = gameObject.GetComponent<Turret_Trigger_Detection>().Model_To_Rotate;
        Resting_Rotation = gameObject.transform.rotation;
        Speed = 5;


    }


    void EngageTarget()
    {
        CancelInvoke("Disengage");
        Target_Rotation = Quaternion.LookRotation(Target.transform.position - Model_To_Rotate.transform.position);
        Model_To_Rotate.transform.rotation = Quaternion.Slerp(Model_To_Rotate.transform.rotation, Target_Rotation, Time.deltaTime * Speed);

        RaycastHit Ray;
        if (Physics.Raycast(Model_To_Rotate.transform.position, Model_To_Rotate.transform.forward, out Ray, 100, gameObject.GetComponent<Turret_Trigger_Detection>().LayerMask))
        {
            Distance = Ray.distance;
            //Debug.DrawLine(Model_To_Rotate.transform.position, Target.transform.position, Color.red);
        }


    }

    void Disengage()
    {
        Model_To_Rotate.transform.rotation = Quaternion.Lerp(Model_To_Rotate.transform.rotation, Resting_Rotation, Time.deltaTime * Speed);

        if (Model_To_Rotate.transform.rotation == Resting_Rotation)
        {
            CancelInvoke("Disengage");
        }
    }
}

