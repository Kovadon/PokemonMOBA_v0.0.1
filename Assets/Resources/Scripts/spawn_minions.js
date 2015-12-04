//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  jesse_etzler@yahoo.com   ******
#pragma strict

var node1 : Transform;
var minionPrefab : GameObject;
var spawning : boolean = true;
var LastInitMinion : GameObject; 
var target : Transform;
function Start () {
     spawnMinion();
}
     
function Update () {
     
}
     
function spawnMinion () {
     
	while(spawning) {
     		yield WaitForSeconds(0.2);
     		LastInitMinion = Instantiate(minionPrefab,node1.transform.position,node1.transform.rotation);
     		Debug.Log("spawn minion rootscript", LastInitMinion);
     		LastInitMinion.GetComponent(AIminions).target=target;
     		yield WaitForSeconds(5);
	}
}


