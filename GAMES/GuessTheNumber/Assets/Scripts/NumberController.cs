using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberController : MonoBehaviour {

	int num;

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

	public void NumberPressed(string numstr) {

		// COPY THE PRESSED NUMBERS TO GUESS ARRAY
		num = int.Parse(numstr);
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

		LastTry();
		DidIWon();
		ChosenOnes();

		isFull0 = isFull1 = isFull2 = isFull3 = false;
	}

	public void LastTry() {

		cryptoNum = GameObject.Find("txtLastTry").GetComponent<Text>();

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
				//if (guess[j] == -1)
				//	continue;
				if (guess[i] == key[j]) {
					if (i == j)
						samePlace++;
					else
						sameNumber++;
				}
			}
		}

		for (int i = 0; i < 4-1; i++) {
			for (int j = ++i; j < 4; j++) {
				if (guess[i] == guess[j])
					sameNumber--;
			}
		}

		Debug.Log("Numbers in the same place: " + samePlace);
		Debug.Log("Numbers in the wrong place: " + sameNumber);
	}
}