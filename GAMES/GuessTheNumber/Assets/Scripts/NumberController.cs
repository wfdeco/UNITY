using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberController : MonoBehaviour {

	int num;
	int unlock;

	int[] key = new int[4];
	int[] guess = new int[4];
	bool isFull0, isFull1, isFull2, isFull3;

	public Text cryptoNum;

	void Start() {

		// SET ALL GUESS NUMBER BOXES TO EMPTY
		isFull0 = isFull1 = isFull2 = isFull3 = false;

		KeyGenerator();

		// PRINT THE KEY NUMBERS IN CONSOLE
		for (int i = 0; i < 4; i++) {
			Debug.Log(key[i]);
		}

		unlock = 0;
	}

	private void KeyGenerator() {

		// GENERATES 4 DIFFERENT NUMBERS TO THE KEY ARRAY
		for (int i = 0; i < 4; i++) {
			if (i == 0) {
				key[i] = Random.Range(0, 10);
			} else if (i == 1) {
				do {
					key[i] = Random.Range(0, 10);
				} while (key[i] == key[i-1]);	
			} else if (i == 2) {
				do {
					key[i] = Random.Range(0, 10);
				} while (key[i] == key[i-1] || key[i] == key[i-2]);
			} else if (i == 3) {
				do {
					key[i] = Random.Range(0, 10);
				} while (key[i] == key[i-1] || key[i] == key[i-2] || key[i] == key[i-3]);
			}
		}
	}

	public void FreeDisabledButton() {

		for (int i = 0; i < 10; i++)
			GameObject.Find("btn" + i.ToString()).GetComponent<Button>().interactable = true;
	}

	public void NumberPressed(string numstr) {

		num = int.Parse(numstr);

		// DISABLE PRESSED NUMBER
		GameObject.Find("btn" + numstr).GetComponent<Button>().interactable = false;

		// COPY THE PRESSED NUMBERS TO GUESS ARRAY
		GuessCode();
	}
	
	public void GuessCode() {

		if (!isFull0) {
			guess[0] = num;
			isFull0 = true;
			ChangeNum ("0", num);
		} else {
			if (!isFull1) {
				guess[1] = num;
				isFull1 = true;
				ChangeNum ("1", num);
			} else {
				if (!isFull2) {
					guess[2] = num;
					isFull2 = true;
					ChangeNum ("2", num);
				} else {
					if (!isFull3) {
						guess[3] = num;
						isFull3 = true;
						ChangeNum ("3", num);
					}
				}
			}
		}
	}

	public void ChangeNum(string z, int x) {

		z = "guess" + z;
		cryptoNum = GameObject.Find(z).GetComponent<Text> ();
		cryptoNum.text = "" + x.ToString ();
	}

	public void EraseChoice() {

		if (isFull3) {
			guess[3] = 0;
			isFull3 = false;
			ChangeNum ("3", 0);
		} else {
			if (isFull2) {
				guess[2] = 0;
				isFull2 = false;
				ChangeNum ("2", 0);
			} else {
				if (isFull1) {
					guess[1] = 0;
					isFull1 = false;
					ChangeNum ("1", 0);
				} else {
					if (isFull0) {
						guess[0] = 0;
						isFull0 = false;
						ChangeNum ("0", 0);
					}
				}
			}
		}
	}

	public void UnlockTry() {

		FreeDisabledButton();
		CopyTry();
		LastTry();
		DidIWon();
		ChosenOnes();

		for (int i = 0; i < 4; i++) {
			EraseChoice();
		}

		if (unlock < 19)
			unlock++;
	}

	private void CopyTry() {

		string objCurrentCopy = "";
		string objDestinationCopy = "";
		string textCopy = "";
		Text currentCopy;
		Text destinationCopy;

		for (int i = unlock; i > 0; i--) {
			if (i < 9) {
				objCurrentCopy = "txtPastCode0" + i.ToString();
				objDestinationCopy = "txtPastCode0" + (i + 1).ToString();
			} else if (i == 9) {
				objCurrentCopy = "txtPastCode0" + i.ToString();
				objDestinationCopy = "txtPastCode" + (i + 1).ToString();
			} else {
				objCurrentCopy = "txtPastCode" + i.ToString();
				objDestinationCopy = "txtPastCode" + (i + 1).ToString();
			}

			currentCopy = GameObject.Find(objCurrentCopy).GetComponent<Text>(); // INSTANTIATE THE GAMEOBJECT TO A VARIABLE
			textCopy = currentCopy.text; // COPY THE GAMEOBJECT TEXT ATRIBUTE TO A VARIABLE

			destinationCopy = GameObject.Find(objDestinationCopy).GetComponent<Text>();
			destinationCopy.text = textCopy;
		}
	}

	public void LastTry() {

		cryptoNum = GameObject.Find("txtPastCode01").GetComponent<Text>();

		string lastTry = "";
		for (int i = 0; i < 4; i++) {
			lastTry = lastTry + guess[i].ToString();
		}
		cryptoNum.text = lastTry;
	}

	private void DidIWon() {

		// FIRST WIN CONDITION
		int gameOver = 0;
		for (int i = 0; i < 4; i++)
			if (key[i] == guess[i])
				gameOver++;
		if (gameOver == 4) {
			Debug.Log ("CONGRATULATIONS! You won!");

			// CALL A METHOD FROM ANOTHER SCRIPT
			LevelManager callWin = GameObject.Find("LevelManager").AddComponent<LevelManager>();
			callWin.LoadLevel("Win");

			return;
		}
	}

	private void ChosenOnes() {

		/* ANALIZAR AS WIN CONDITIONS AQUI */

		int samePlace = 0;
		int sameNumber = 0;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (guess[i] == key[j]) {
					if (i == j)
						samePlace++;
					else
						sameNumber++;
					break;
				}
			}
		}

		//GameObject.Find("btn" + numstr).GetComponent<Button>().interactable = false;
		if (unlock > 0)
			GameObject.Find("txtFound2").GetComponent<Text>().text = GameObject.Find("txtFound1").GetComponent<Text>().text;

		GameObject.Find("txtFound1").GetComponent<Text>().text = sameNumber.ToString() + "\n" + samePlace.ToString();

		Debug.Log("Numbers in the same place: " + samePlace);
		Debug.Log("Numbers in the wrong place: " + sameNumber);
	}
}