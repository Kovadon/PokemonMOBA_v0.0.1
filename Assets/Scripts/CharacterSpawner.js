#pragma strict
//these are the player prefabs that will be automagically plugged in for us.
var player01Prefab : GameObject;
var player02Prefab : GameObject;
var player03Prefab : GameObject;

//this is where the script placed in the level inputs in this number for the player who was selected
//and saved by playerPrefs
var savedPlayer : int = 0;

//this is called first before the Start function, so make sure it loads everthing needed first.
function Awake() {

	// Let's grab the saved data for each player and grab that integer to use to load that player in the world
	savedPlayer = PlayerPrefs.GetInt("selectedPlayer");
		
	player01Prefab = GameObject.Find("Bulbasaur");
	player02Prefab = GameObject.Find("Charmander");
	player03Prefab = GameObject.Find("#000");
	
	if(savedPlayer == 0) //if we've not selected any player initially lets just use Player 1 
        {  					
		player01Prefab.SetActiveRecursively(true);
		player02Prefab.SetActiveRecursively(false);
		player03Prefab.SetActiveRecursively(false);
		}
	else if(savedPlayer == 1) //if we've set the player to 1 from playerprefs then 
        {  					
		player01Prefab.SetActiveRecursively(true);
		player02Prefab.SetActiveRecursively(false);
		player03Prefab.SetActiveRecursively(false);
        }
	else if(savedPlayer == 2) //if we've set the player to 2 from playerprefs then 
        {  					
		player02Prefab.SetActiveRecursively(true);
		player01Prefab.SetActiveRecursively(false);
		player03Prefab.SetActiveRecursively(false);
        }
    else if(savedPlayer == 3) //if we've set the player to 3 from playerprefs then 
        {  					
		player03Prefab.SetActiveRecursively(true);
		player01Prefab.SetActiveRecursively(false);
		player02Prefab.SetActiveRecursively(false);
		
        }
}


