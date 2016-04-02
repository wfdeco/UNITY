using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {

		Debug.Log ("Level request: " + name);
		SceneManager.LoadScene (name);
	}
}
