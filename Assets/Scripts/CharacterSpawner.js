//#pragma strict
//these are the player prefabs that will be automagically plugged in for us.
//CharacterControllers included for network
var CharacterControllers : GameObject[]; 
var playerPrefabs : GameObject[];



//this is where the script placed in the level inputs in this number for the player who was selected
//and saved by playerPrefs
var savedPlayer : int = 0;

//this is called first before the Start function, so make sure it loads everthing needed first.
function Awake() {

	// Let's grab the saved data for each player and grab that integer to use to load that player in the world
	savedPlayer = PlayerPrefs.GetInt("selectedPlayer");

	CharacterControllers = GameObject.FindGameObjectsWithTag("Character");

	playerPrefabs = GameObject.FindGameObjectsWithTag("spawingCharacter");

	for(var i=0;i<playerPrefabs.Length;i++)
	{
		/*var current : GameObject = playerPrefabs[i];
		if ((current).GetComponent(Transform).IsChildOf(CharacterControllers.GetComponent(Transform)))
			//todo: finish and check what is wrong
			Debug.LogError("Not a child"); */
		//set active if !SavedPlayer
		playerPrefabs[i].SetActive(savedPlayer==i);
	} 
}
