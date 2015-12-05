//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  jesse_etzler@yahoo.com   ******

static var tower1health : int = 500;
static var tower1destroyed : boolean = false;

function Update () {

	if(tower1health <= 0) {

		Destroy(this.gameObject);
	}
}