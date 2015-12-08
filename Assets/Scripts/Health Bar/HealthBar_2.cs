using UnityEngine;
using System.Collections;

public class HealthBar_2 : MonoBehaviour
{

    public float Scale;
    public Vector3 Anchor_Position;
    public float Control_Number;

    public Color Target_Color;
    public Color Current_Color;

    public Renderer HealthBar;

    Quaternion Locked_rotation;



    void Start()
    {
        HealthBar = gameObject.GetComponent<Renderer>();

        Control_Number = 1f;
        

        Current_Color.r = 1 - (1 * Control_Number);
        Current_Color.g = Control_Number;
        Current_Color.b = Control_Number * .5f;
        HealthBar.material.SetColor("_Color", new Color(Current_Color.r, Current_Color.g, Current_Color.b, Current_Color.a));

        transform.localScale = new Vector3(3f, .3f, .3f);
        Scale = gameObject.transform.localScale.x;

        Locked_rotation = Quaternion.FromToRotation(new Vector3(0, 0, 90), -Vector3.up);

    }

public void Update_Healthbar()
    {
       // Anchor_Position = new Vector3((-Scale / 2) + ((Scale * Control_Number) / 2), transform.localPosition.y, transform.localPosition.z);
        //gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.position, Anchor_Position, Time.deltaTime * 1000);
        transform.localScale = new Vector3(Scale * Control_Number, transform.localScale.y, transform.localScale.z);


        Current_Color.r = 1 - (1 * Control_Number);
        Current_Color.g = Control_Number;
        Current_Color.b = Control_Number * .5f;
        HealthBar.material.SetColor("_Color", new Color(Current_Color.r, Current_Color.g, Current_Color.b, Current_Color.a));

        gameObject.transform.rotation = Locked_rotation;

        if (Control_Number < 0)
        {
            Control_Number = 0;
        }

    }
}
