using UnityEngine;
using System.Collections;

public class Health_Bar_1 : MonoBehaviour {

    public Renderer HealthBar;

    public float Starting_Health;
    public float Current_Health;
    public float Old_Health;
    public Vector3 Anchor_Position;

    public Vector3 Position;

    public float Scale;

    public float Control_Number;

    public Color Target_Color;
    public Color Current_Color;


    // Use this for initialization
   public void Start () {

        HealthBar = gameObject.GetComponent<Renderer>();
        Position = gameObject.transform.position;
        Anchor_Position = transform.position;
        Control_Number = 1f;


        Current_Color.r = 1 - (1 * Control_Number);
        Current_Color.g = Control_Number;
        Current_Color.b = Control_Number * .5f;
        HealthBar.material.SetColor("_Color", new Color(Current_Color.r, Current_Color.g, Current_Color.b, Current_Color.a));

    }

   public void DoWork()
    {
        Anchor_Position = new Vector3((-Scale / 2) + ((Scale * Control_Number) / 2), transform.localPosition.y, transform.localPosition.z);
        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, Anchor_Position, Time.deltaTime * 1000);
        transform.localScale = new Vector3(Scale * Control_Number, transform.localScale.y, transform.localScale.z);
        Control_Number = Current_Health / Starting_Health;

        Current_Color.r = 1 - (1 * Control_Number);
        Current_Color.g = Control_Number;
        Current_Color.b = Control_Number * .5f;
        HealthBar.material.SetColor("_Color", new Color(Current_Color.r, Current_Color.g, Current_Color.b, Current_Color.a));

    }
	

	public void Check_Health() {


        if (Control_Number < 0)
        {
            Control_Number = 0;
        }
        
        if (Current_Health != Old_Health)
        {
            Old_Health = Current_Health;
            DoWork();
        }
    }
}
