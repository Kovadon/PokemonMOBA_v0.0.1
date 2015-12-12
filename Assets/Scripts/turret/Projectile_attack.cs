using UnityEngine;
using System.Collections;

public class Projectile_attack : MonoBehaviour
{

    public GameObject Projectile;
    public GameObject[] Projectile_Array = null;
    public GameObject Object_To_Emit_From;

    public GameObject Target;


    public float Range;
    public float Damage;
    public float Projectile_Speed;
    public float Attack_Interval;

    public float Repeat_Time;
    public float Game_Time;

    // Use this for initialization
    void Start()
    {

        Projectile_Array = new GameObject[20];
        for (int i = 0; i < Projectile_Array.Length; i++)
        {
            Projectile_Array[i] = Instantiate(Projectile, Object_To_Emit_From.transform.position, Object_To_Emit_From.transform.rotation) as GameObject;
            Projectile_Array[i].transform.SetParent(Object_To_Emit_From.transform);
            if (Projectile_Array[i].GetComponent<Team_Emissive_Color>() != null)
            {
                Projectile_Array[i].GetComponent<Team_Emissive_Color>().Object_With_Team_Script = gameObject;
            }
            Projectile_Array[i].SetActive(false);
        }

        Repeat_Time = Time.time + Attack_Interval;



    }

    public void Fire()
    {

        Target = gameObject.GetComponent<Turret_Trigger_Detection>().Target;

        for (int i = 0; i < Projectile_Array.Length; i++)
        {
            if (Projectile_Array[i].activeInHierarchy == false)
            {
                Projectile_Array[i].transform.position = Object_To_Emit_From.transform.position;
                Projectile_Array[i].SetActive(true);
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Target = Target;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Speed = Projectile_Speed;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Range = Range;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Damage = Damage;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Owner = gameObject;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().Start_Position = Object_To_Emit_From.transform.position;
                Projectile_Array[i].gameObject.GetComponent<Projectile_Behavior>().InvokeRepeating("Move_To_Target", 0f, .01f);
                return;
            }
        }

    }

    public void Attack()
    {
        Game_Time = Time.time;

        if (Game_Time >= Repeat_Time)
        {
            Fire();
            Repeat_Time = Time.time + Attack_Interval;
        }
    }
}