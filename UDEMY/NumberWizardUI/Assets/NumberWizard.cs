using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NumberWizard : MonoBehaviour {

	int max = 1000;
	int min = 1;
	int guess;
	int maxGuesses = 10;

	public Text text;

	void Start() {

		NextGuess();
	}

	public void GuessLower() {

		max = guess;
		NextGuess();
	}

	public void GuessHigher() {

		min = guess;
		NextGuess();
	}

	public void NextGuess() {

		maxGuesses -= 1;
		if (maxGuesses <= 0) {
			SceneManager.LoadScene ("Lose");
		}

		guess = Random.Range (min, max + 1);
		text.text = guess.ToString();
	}
}
