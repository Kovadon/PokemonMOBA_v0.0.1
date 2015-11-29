using UnityEngine;
using System.Collections;

public class Click_To_Move : MonoBehaviour {

    public GameObject Cursor;
    public Camera Camera;
    public int layerMask = 1 << 10;
    public string Name;

    public Vector3 Destination;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            Ray Mouse = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit MouseHit = new RaycastHit();
             if (Physics.Raycast(Mouse, out MouseHit, 1000, ~layerMask))
            {
            Cursor.transform.position = MouseHit.point;
            Destination = MouseHit.point;
            Name = MouseHit.collider.gameObject.name;
            Cursor.transform.rotation = Quaternion.FromToRotation(MouseHit.normal, transform.up);
            Debug.DrawLine(Camera.ScreenToWorldPoint(Input.mousePosition), MouseHit.point, Color.red);
            }
                
        }

    
}
