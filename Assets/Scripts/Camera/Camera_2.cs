using UnityEngine;
using System.Collections;

public class Camera_2 : MonoBehaviour {


    float radius = 3f, angleX = 0f, angleY = -45f;
    public GameObject target;
    public float Speed;
    public float Camera_Minimum_Height;
    Quaternion Target_Rotation;

    Vector3 Following_Angle;


    // Use this for initialization
    void Start () {

        Camera_Minimum_Height = 10;

        Following_Angle = new Vector3(0, 10, 10);

        radius = Vector3.Distance(transform.position, target.transform.position);
        angleX += Input.GetAxis("Mouse X") * Time.deltaTime * Speed;
        angleY += Input.GetAxis("Mouse Y") * Time.deltaTime * Speed;

        float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
        float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);
        float y = radius * Mathf.Cos(angleY);
        transform.position = new Vector3(x + target.transform.position.x,
                                         y + target.transform.position.y,
                                         z + target.transform.position.z);
        Target_Rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);
        Following_Angle = transform.position - target.transform.position;

    }
	
	// Update is called once per frame
	void LateUpdate () {

        Target_Rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);
        transform.position = Following_Angle + target.transform.position;

        radius -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * Speed * 1000;


        if (Input.GetMouseButton(2))
        {
            angleX += Input.GetAxis("Mouse X") * Time.deltaTime * Speed;
            angleY += Input.GetAxis("Mouse Y") * Time.deltaTime * Speed;
        }

            float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
            float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);
            float y = radius * Mathf.Cos(angleY);
            transform.position = new Vector3(x + target.transform.position.x,
                                             Mathf.Clamp(y + target.transform.position.y, Camera_Minimum_Height, 100),
                                             z + target.transform.position.z);
            
            Target_Rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);
            Following_Angle = transform.position - target.transform.position;

    }
}
