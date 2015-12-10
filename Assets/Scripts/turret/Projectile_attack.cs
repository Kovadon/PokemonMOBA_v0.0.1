using UnityEngine;
using System.Collections;

public class Projectile_attack : MonoBehaviour {

    public GameObject Projectile;
    public GameObject[] Projectile_Array = null;

    public GameObject Target;

    public float Range;
    public float Damage;
    public float Attack_Interval;

    float Time1;
    float repeat_time;

    // Use this for initialization
    void Start () {

        Projectile_Array = new GameObject[20];

        Range = 100f;
        Damage = 50;
        Attack_Interval = 5f;

        repeat_time = Time.time + Attack_Interval;

    }

    public void Attack()
    {
        Target = gameObject.GetComponent<Turret_Trigger_Detection>().Target;


    }

    // Update is called once per frame
    void Update () {
	
	}
}
