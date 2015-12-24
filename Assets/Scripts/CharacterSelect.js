#pragma strict
//this is the currently selected Player. Also the one that will be saved to PlayerPrefs
var selectedPlayer : int = 0;

function Update() 
{ 
if (Input.GetMouseButtonUp (0)) {
	var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	var hit : RaycastHit;
	
	if (Physics.Raycast (ray, hit, 100))
		{
				// The pink text is where you would put the name of the object you want to click on (has attached collider).
				
	            if(hit.collider.name == "Bulbasaur") 
				SelectedCharacter1(); //Sends this click down to a function called "SelectedCharacter1(). Which is where all of our stuff happens.
			
				else if(hit.collider.name == "Charmander")
				SelectedCharacter2();
					
				else if(hit.collider.name == "#000")
				    SelectedCharacter0();
        					
		} 
		else
		{
		return;               
		}
	} 
}

function SelectedCharacter1() {
	Debug.Log ("Character 1 SELECTED"); //Print out in the Unity console which character was selected.
	selectedPlayer = 1;
	PlayerPrefs.SetInt("selectedPlayer", (selectedPlayer));
	Application.LoadLevel ("RFL Map");
}

function SelectedCharacter2() {
	Debug.Log ("Character 2 SELECTED");
	selectedPlayer = 2;
	PlayerPrefs.SetInt("selectedPlayer", (selectedPlayer));
	Application.LoadLevel ("RFL Map");
}

function SelectedCharacter0() {
	Debug.Log ("Character 0 SELECTED");
	selectedPlayer = 0;
	PlayerPrefs.SetInt("selectedPlayer", (selectedPlayer));
	Application.LoadLevel ("RFL Map");
}