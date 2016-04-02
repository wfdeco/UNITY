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

		isFull0 = isFull1 = isFull2 = isFull3 = false;

		for (int i = 0; i < 4; i++) {
			key [i] = Random.Range (0, 10);
			print (key [i]);
		}
	}

	public void NumberPressed(string numstr) {

		num = int.Parse (numstr);
		Debug.Log ("Number pressed: " + num);
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

		print (guess[0]);
		print (guess[1]);
		print (guess[2]);
		print (guess[3]);
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

		int gameover = 0;

		LastTry();

		for (int i = 0; i < 4; i++) {
			if (key [i] == guess [i])
				gameover++;
		}

		if (gameover == 4) {
			Debug.Log ("CONGRATULATIONS! You won!");
			return;
		}

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (key[i] == guess[j]) {
					Debug.Log ("Same number found");
					if (i == j) {
						Debug.Log ("And in the same place");
					}
				}
			}
		}

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
}