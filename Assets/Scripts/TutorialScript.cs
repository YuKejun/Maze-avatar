using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour
{

		public GUIText tutorial;
		private bool finishWalk;
		private bool finishTurnLeft;
		private bool finishTurnRight;
		private bool finishBEV;
		public float waitTime;

		// Use this for initialization
		void Start ()
		{
				finishWalk = false;
				finishTurnLeft = false;
				finishTurnRight = false;
				finishTurnRight = false;
				finishBEV = false;
				tutorial.text = "Fist to walk forward";
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				StartCoroutine (Tutor ());
		}

		IEnumerator Tutor ()
		{
				if (finishBEV == true) {
						if (Input.GetKey ("3")) {
								yield return new WaitForSeconds (waitTime);
								tutorial.text = "Congratulation! You have finished the tutorial!";
						}
				} else if (finishTurnRight == true) {
						//BEV now
						if (Input.GetKey ("3")) {
								yield return new WaitForSeconds (waitTime);
								finishBEV = true;
								tutorial.text = "Spread fingers again to switch back.";
						}
				} else if (finishTurnLeft == true) {
						//turn right now
						if (Input.GetKey ("right")) {
								yield return new WaitForSeconds (waitTime);
								finishTurnRight = true;
								tutorial.text = "Spread fingers to switch to birds'-eye-view.";
						}
				} else if (finishWalk == true) {
						//turn left now
						if (Input.GetKey ("left")) {
								yield return new WaitForSeconds (waitTime);
								finishTurnLeft = true;
								tutorial.text = "Wave right to turn right.";
						}
				} else {
						//walk now
						if (Input.GetKey ("space")) {
								yield return new WaitForSeconds (waitTime);
								finishWalk = true;
								tutorial.text = "Wave left to turn left.";
						}
			
				}
		}
}
