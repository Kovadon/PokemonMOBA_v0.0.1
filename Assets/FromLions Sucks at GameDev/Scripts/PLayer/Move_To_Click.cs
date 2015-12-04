using UnityEngine;
using System.Collections;

public class Move_To_Click : MonoBehaviour {

    public Vector3 Destination;
    public float Speed;
    public GameObject Camera;

    public Quaternion Target_Rotation;


    // Use this for initialization
    void Start () {

        Speed = gameObject.GetComponent<Player_Information>().Speed;
        Camera.GetComponent<Team>().Team_Blue = gameObject.GetComponent<Team>().Team_Blue;
        Camera.GetComponent<Team>().Team_Red = gameObject.GetComponent<Team>().Team_Red;
        Camera.GetComponent<Team>().Neutral = gameObject.GetComponent<Team>().Neutral;
        Camera.GetComponent<Team>().Start();

    }

    void SmoothMove()
    {
        Speed = gameObject.GetComponent<Player_Information>().Speed;

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, Time.deltaTime * Speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Target_Rotation, Time.deltaTime * Speed);


        if (gameObject.transform.position == Destination)
        {
            CancelInvoke("SmoothMove");
        }

    }



        public void Get_Destination()
        {
            Camera.GetComponent<Click_To_Move>().GetDestination();
            CancelInvoke("SmoothMove");
            Destination = new Vector3(Camera.GetComponent<Click_To_Move>().Destination.x, Camera.GetComponent<Click_To_Move>().Destination.y + gameObject.transform.position.y, Camera.GetComponent<Click_To_Move>().Destination.z);
            Target_Rotation = Quaternion.LookRotation(Destination - transform.position);
            InvokeRepeating("SmoothMove", 0f, .01f);
        }


        public void GetTarget()
        {
            Camera.GetComponent<Click_To_Target>().GetTarget();
        }


        public void Camera_Higher()
        {
            if (Camera.GetComponent<Smooth_Camera_Follow>().Camera_Height < 70)
            {
                ++Camera.GetComponent<Smooth_Camera_Follow>().Camera_Height;
            }
        }


        public void Camera_Lower()
        {
            if (Camera.GetComponent<Smooth_Camera_Follow>().Camera_Height > 5)
            {
                --Camera.GetComponent<Smooth_Camera_Follow>().Camera_Height;
            }
        }



    }
