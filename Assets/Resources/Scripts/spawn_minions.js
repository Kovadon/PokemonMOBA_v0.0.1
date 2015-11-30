//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  jesse_etzler@yahoo.com   ******

var node1 : Transform;
var minionPrefab : GameObject;
var spawning : boolean = true;
     
function Start () {
     spawnMinion();
}
     
function Update () {
     
}
     
function spawnMinion () {
     
	while(spawning) {
     		yield WaitForSeconds(0.2);
     		Instantiate(minionPrefab,node1.transform.position,node1.transform.rotation);
     		yield WaitForSeconds(5);
	}
}