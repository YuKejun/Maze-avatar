using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour
{

		public GUIText tutorial;
		private bool finishWalk;
		private bool finishTurnLeft;
		private bool finishTurnRight;
		private bool finishBEV;
		public float waitTime;
		public float finalWaitTime;
		public RawImage iconFist;
		public RawImage iconWaveLeft;
		public RawImage iconWaveRight;
		public RawImage iconSpread;
		public RawImage iconCong;

		// Use this for initialization
		void Start ()
		{
				finishWalk = false;
				finishTurnLeft = false;
				finishTurnRight = false;
				finishTurnRight = false;
				finishBEV = false;
				tutorial.text = "Fist to walk forward";
				iconFist.enabled = true;
				iconSpread.enabled = false;
				iconWaveLeft.enabled = false;
				iconWaveRight.enabled = false;
				iconCong.enabled = false;
	
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				if (CharactorController.can_switch == true)
						StartCoroutine (Tutor ());
				else
						StartCoroutine (TutorwoBEV ());
		}

		IEnumerator Tutor ()
		{
				if (finishBEV == true) {
						if (Input.GetKey ("3")) {
								yield return new WaitForSeconds (waitTime);
								tutorial.text = "Congratulation!\nYou have finished the tutorial!\nThe game will start now.";
								iconSpread.enabled = false;
								iconCong.enabled = true;
								yield return new WaitForSeconds (finalWaitTime);
								Application.LoadLevel (InGameMenuScript.levelToLoad);
						}
				} else if (finishTurnRight == true) {
						//BEV now
						if (Input.GetKey ("3")) {
								yield return new WaitForSeconds (waitTime);
								finishBEV = true;
								tutorial.text = "Spread fingers again\nto switch back.";
						}
				} else if (finishTurnLeft == true) {
						//turn right now
						if (Input.GetKey ("right")) {
								yield return new WaitForSeconds (waitTime);
								finishTurnRight = true;
								tutorial.text = "Spread fingers to switch to\nbirds'-eye-view.";
								iconWaveRight.enabled = false;
								iconSpread.enabled = true;
						}
				} else if (finishWalk == true) {
						//turn left now
						if (Input.GetKey ("left")) {
								yield return new WaitForSeconds (waitTime);
								finishTurnLeft = true;
								tutorial.text = "Wave right to turn right.";
								iconWaveLeft.enabled = false;
								iconWaveRight.enabled = true;

						}
				} else {
						//walk now
						if (Input.GetKey ("space")) {
								yield return new WaitForSeconds (waitTime);
								finishWalk = true;
								tutorial.text = "Wave left to turn left.";
								iconFist.enabled = false;
								iconWaveLeft.enabled = true;
						}
			
				}
		}

		IEnumerator TutorwoBEV ()
		{
				if (finishTurnLeft == true) {
						if (Input.GetKey ("right")) {
								yield return new WaitForSeconds (waitTime);
								tutorial.text = "Congratulation!\nYou have finished the tutorial!\nThe game will start now.";
								iconWaveRight.enabled = false;
								iconCong.enabled = true;
								yield return new WaitForSeconds (finalWaitTime);
								Application.LoadLevel (InGameMenuScript.levelToLoad);
						}
				} else if (finishWalk == true) {
						//turn left now
						if (Input.GetKey ("left")) {
								yield return new WaitForSeconds (waitTime);
								finishTurnLeft = true;
								tutorial.text = "Wave right to turn right.";
								iconWaveLeft.enabled = false;
								iconWaveRight.enabled = true;
				
						}
				} else {
						//walk now
						if (Input.GetKey ("space")) {
								yield return new WaitForSeconds (waitTime);
								finishWalk = true;
								tutorial.text = "Wave left to turn left.";
								iconFist.enabled = false;
								iconWaveLeft.enabled = true;
						}
			
				}
		}
	
	
		private void OnGUI ()
		{
				if (GUI.Button (new Rect ((0), Screen.height - 20, 60, 20), "Skip")) {
						Application.LoadLevel (InGameMenuScript.levelToLoad);
				}
		}
}
