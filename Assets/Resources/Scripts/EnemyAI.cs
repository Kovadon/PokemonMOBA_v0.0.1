using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform targetPlayer;
    public Transform myEnemy;
    private int moveSpeed = 5;
    private int rotationSpeed = 5;


    void Awake ()
    {
        myEnemy = transform;
    }

	// Use this for initialization
	void Start ()
    {
        targetPlayer = GameObject.FindWithTag("Character").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var lookDir = targetPlayer.position - transform.position;
        lookDir.y = 0;
        myEnemy.rotation = Quaternion.Slerp(myEnemy.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        myEnemy.position += myEnemy.forward * moveSpeed * Time.deltaTime;
	}
}
