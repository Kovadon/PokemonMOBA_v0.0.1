using UnityEngine;
using System.Collections;

public class MenuConnectButton : MonoBehaviour {

	public void ChangeToScene (string sceneToChangeTo) {
        Application.LoadLevel(sceneToChangeTo);
	}
}
