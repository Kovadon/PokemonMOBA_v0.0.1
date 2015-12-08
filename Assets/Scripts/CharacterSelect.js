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
			
				if(hit.collider.name == "Charmander")
				SelectedCharacter2();
					
				if(hit.collider.name == "#000")
				    SelectedCharacter3();
        					
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

function SelectedCharacter3() {
	Debug.Log ("Character 3 SELECTED");
	selectedPlayer = 3;
	PlayerPrefs.SetInt("selectedPlayer", (selectedPlayer));
	Application.LoadLevel ("RFL Map");
}