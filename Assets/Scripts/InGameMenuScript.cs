﻿using UnityEngine;
using System.Collections;

public class InGameMenuScript : MonoBehaviour {
	
	public static int levelToLoad = 1;
	
	private string clicked = "hide";
	
	private void OnGUI ()
	{
		if (clicked == "") {
			GUI.Window (0, new Rect ((Screen.width / 2) - 100, Screen.height / 2, 200, 200), menuFunc, "Main Menu");
		}
		if (clicked == "hide") {
			if (GUI.Button (new Rect (0, Screen.height - 30, 100, 30), "Menu")) {
//				CharactorController.can_action = false;
				Time.timeScale = 0;
				clicked = "";
			}
		}
		
	}
	
	private void menuFunc (int id)
	{
		//buttons 
		if (GUILayout.Button ("Resume")) {
			Time.timeScale = 1;
//			CharactorController.can_action = true;
			clicked = "hide";
		}
		if (GUILayout.Button ("Restart")) {
			Time.timeScale = 1;
			Application.LoadLevel (1);
		}
//		if (GUILayout.Button ("Level 2")) {
//			Time.timeScale = 1;
//			Application.LoadLevel (2);
//		}
		if (GUILayout.Button ("Quit Level")) {
			Time.timeScale = 1;
			Application.LoadLevel (0);
		}
		
	}

//	private void update()
//	{
//		if (clicked == "hide" && Input.GetKey (KeyCode.Escape)) {
//			CharactorController.can_action = false;
//			clicked = "";
//		}
//	}
}
