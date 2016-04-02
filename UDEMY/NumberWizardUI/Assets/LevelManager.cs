using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {

		Debug.Log ("Level request for: " + name);
		SceneManager.LoadScene (name);
	}

	public void QuitRequest() {

		Debug.Log ("I want to quit, NOW!");
		Application.Quit ();
	}
}
