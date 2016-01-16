using UnityEngine;
using System.Collections;

public class Click_To_Move : MonoBehaviour {

    public Camera Camera;
    public LayerMask Navigatioin_layerMask;

    public bool Draw_Raycast;

    public Vector3 Destination;


    // gets the destination for the player from raycast

   public void GetDestination()
    {
        Ray Mouse = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit MouseHit = new RaycastHit();
        if (Physics.Raycast(Mouse, out MouseHit, 1000, Navigatioin_layerMask))
        {
            Destination = MouseHit.point;
            if (Draw_Raycast == true)
            {
                Debug.DrawLine(Camera.ScreenToWorldPoint(Input.mousePosition), MouseHit.point, Color.red);
            }
        }
    }





}
