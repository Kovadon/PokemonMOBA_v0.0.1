using UnityEngine;
using System.Collections;

public class Check_For_Input : MonoBehaviour {

    /*
    if it needs to get called like an update function would, try to stick it in here
    */

    public bool LoL_Style_Click_Behavior;




	void Start () {

        //control how fast we poll for input, lets us keep things independant of fram rate
        InvokeRepeating("GetInput", 0f, .001f);

    }
	


    // stick your "I need to call this every frame type stuff in here"

	public void GetInput()
    {

        if (LoL_Style_Click_Behavior == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Move>().InvokeRepeating("GetDestination", 0f, .2f);
                gameObject.GetComponent<Move_To_Click>().InvokeRepeating("Update_NavMesh_Destination", 0f, .2f);

            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Move>().CancelInvoke("GetDestination");
                gameObject.GetComponent<Move_To_Click>().CancelInvoke("Update_NavMesh_Destination");

            }


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
               gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Target>().GetTarget();
            }
        }



        if (LoL_Style_Click_Behavior == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Target>().InvokeRepeating("GetTarget", 0f, .2f);
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Move>().InvokeRepeating("GetDestination", 0f, .2f);
                gameObject.GetComponent<Move_To_Click>().InvokeRepeating("Update_NavMesh_Destination", 0f, .2f);

            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Target>().CancelInvoke("GetTarget");
                gameObject.GetComponent<Move_To_Click>().Camera.GetComponent<Click_To_Move>().CancelInvoke("GetDestination");
                gameObject.GetComponent<Move_To_Click>().CancelInvoke("Update_NavMesh_Destination");

            }
        }


        /*
			if (Input.GetAxisRaw ("Mouse ScrollWheel") < 0) {
				++gameObject.GetComponent<Move_To_Click> ().Camera.GetComponent<Smooth_Camera_Follow> ().Camera_Height;
			}

			if (Input.GetAxisRaw ("Mouse ScrollWheel") > 0) {
				--gameObject.GetComponent<Move_To_Click> ().Camera.GetComponent<Smooth_Camera_Follow> ().Camera_Height;
			}
        */



			//gameObject.GetComponent<Move_To_Click> ().Camera.GetComponent<Click_To_Target> ().MouseOverTarget ();

		
	}
}
