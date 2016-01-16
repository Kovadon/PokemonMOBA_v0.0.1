using UnityEngine;
using System.Collections;

public class Movement_Speed : MonoBehaviour {
	//read and write values for speed in an easy to find place, needs to get hooked up to the navmesh agent at some point

    public int Starting_Speed;
    public int Old_Speed;
	private int current_speed;
	public int Current_Speed
	{
		get
		{
			if (gameObject.GetComponent<NavMeshAgent>()){
				return (int) (gameObject.GetComponent<NavMeshAgent>().speed);
			} else {
				return current_speed;
			}
		}
		set
		{
			Old_Speed = current_speed;
			current_speed = value;
			if (gameObject.GetComponent<NavMeshAgent> ()) {
				gameObject.GetComponent<NavMeshAgent> ().speed = current_speed;
			}
		}
	}
    
	public void Start() {
		if (gameObject.GetComponent<NavMeshAgent> () != false) {
			gameObject.GetComponent<NavMeshAgent> ().speed = Starting_Speed;
		} else {
			Debug.Log ("Does not contain a Navmesh", gameObject);
		}
        Current_Speed = Starting_Speed;
	}

    void Update()
    {
    }
}
