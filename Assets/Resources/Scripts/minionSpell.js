//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  jesse_etzler@yahoo.com   ******

var fireAt : Transform;
var spellTransform : Transform;
var rotationSpeed2 = 7;
var moveSpeed2 = 10;

function Update () {

	fireAt = GameObject.FindWithTag("tower1").transform; 

   	spellTransform.rotation = Quaternion.Slerp(spellTransform.rotation,
  	Quaternion.LookRotation(fireAt.position - spellTransform.position), rotationSpeed2*Time.deltaTime); 
   	spellTransform.position += spellTransform.forward * moveSpeed2 * Time.deltaTime;

	if(TowerScript.tower1health <= 0) {

		Destroy(this.gameObject);
	}
}

function OnTriggerEnter (col : Collider) {
 
	if(col.gameObject.tag == "tower1") {
		TowerScript.tower1health -= 10;
		Destroy(this.gameObject);
	}
}