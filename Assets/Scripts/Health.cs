using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private int currentHealth;

    private int  CurrentHealth
    {
        get { return currentHealth; }
        set {
            currentHealth = value;
            HandleHealth();
        }
    }


    public int maxHealth;
    public Text healthText;
    public Image visualHealth;
    public float coolDown;
    private bool onCD;


	// Use this for initialization
	void Start ()
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = maxHealth;
        onCD = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void HandleHealth()
    {
        healthText.text = "HP: " + currentHealth + " / 100";

        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);

        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth/2) //Above 50% health
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else //Below 50% health
        {
            visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    IEnumerator CoolDownDMG()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Damage")
        {
            if (!onCD && currentHealth > 0)
            {
                StartCoroutine(CoolDownDMG());
                CurrentHealth -= 1;
            }
        }
        if (other.name == "Health")
        {
            if (!onCD && currentHealth < maxHealth)
            {
                StartCoroutine(CoolDownDMG());
                CurrentHealth += 1;
            }
        }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
