using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {

	public float Speed;

	// Use this for initialization
	void Start () {

		Speed = 6;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate (new Vector3 (-1, 0, 0) * Speed * Time.deltaTime);
		}
	
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate (new Vector3 (1, 0, 0) * Speed * Time.deltaTime);
		}

		if (Input.GetAxisRaw ("Vertical") < 0) {
			transform.Translate (new Vector3 (0, 0, -1) * Speed * Time.deltaTime);
		}

		if (Input.GetAxisRaw ("Vertical") > 0) {
			transform.Translate (new Vector3 (0, 0, 1) * Speed * Time.deltaTime);


		}
		if (Input.GetAxisRaw ("Mouse X") > 0) {
			transform.Rotate (new Vector3 (0, 1, 0) * Speed*20 * Time.deltaTime);
		}
		if (Input.GetAxisRaw ("Mouse X") < 0) {
			transform.Rotate (new Vector3 (0, -1, 0) * Speed*20 * Time.deltaTime);
		}

		if (Input.GetAxisRaw ("Mouse Y") > 0) {
			transform.Rotate (new Vector3 (1, 0, 0) * Speed*20 * Time.deltaTime);
		}
		if (Input.GetAxisRaw ("Mouse Y") < 0) {
			transform.Rotate (new Vector3 (-1, 0, 0) * Speed*20 * Time.deltaTime);
		}


	}
}
