using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuConnectButton : MonoBehaviour {

	public void ChangeToScene (string sceneToChangeTo) {
        SceneManager.LoadScene(sceneToChangeTo);
	}
}
