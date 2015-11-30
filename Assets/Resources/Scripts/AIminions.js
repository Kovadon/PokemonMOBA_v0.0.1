//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  jesse_etzler@yahoo.com   ******

var target : Transform;
var rotationSpeed = 5;
var myTransform : Transform;
var canMove : boolean = true;
var inCombat : boolean = false;
var fireSpell : Transform;
var pathingCooldown : boolean = false;
var atTower1 : boolean = false;
var attackCooldown : boolean = false;

function Awake() {

	myTransform = transform;
}

function Start() {

	target = GameObject.FindWithTag("node2").transform;
}

function Update () {

	if(TowerScript.tower1destroyed == false && atTower1 == true && attackCooldown == false) {

		Instantiate(fireSpell,this.transform.position,Quaternion.identity);
		minionAttackCooldown();
	}

	else if( TowerScript.tower1destroyed == true && atTower1 == true) {

		canMove = true;
	}

	if(canMove == true && inCombat == false ) {
		var moveSpeed = Random.Range(10,150);
		var randomPathing = Random.Range(0,2);
		var randomSpeed = Random.Range(-160,160);

		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
		Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);

        if(randomPathing == 0) {
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	}

        if(randomPathing == 1) {
		myTransform.position += myTransform.right * randomSpeed * Time.deltaTime;
	}
	}
}

function OnTriggerEnter (col : Collider) {

	if(col.gameObject.tag == "tower1trigger") {
		if(TowerScript.tower1destroyed == false) {
			 Debug.Log("entered tower1");
			this.canMove = false;
			this.atTower1 = true;
		}
	}
}

function minionAttackCooldown () {

	attackCooldown = true; 
	yield WaitForSeconds(5);
	attackCooldown = false;
}