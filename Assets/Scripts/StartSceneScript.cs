﻿using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	public string[] AboutTextLines = new string[0];
	
	private bool toggle = false;
	private string clicked = "", MessageDisplayOnAbout = "About \n ";
	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200);
	private float volume = 1.0f;
	
	private void Start()
	{
		for (int x = 0; x < AboutTextLines.Length;x++ )
		{
			MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
		}
		MessageDisplayOnAbout += "Maze By Myo YAY\n";
		MessageDisplayOnAbout += "Press Esc To Go Back\n";

	}
	
	private void OnGUI()
	{
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;
		if (clicked == "")
		{
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu");
		}
		else if (clicked == "options")
		{
			WindowRect = GUI.Window(1, WindowRect, optionsFunc, "Options");
		}
		else if (clicked == "about")
		{
			GUI.Box(new Rect (0,0,Screen.width,Screen.height), MessageDisplayOnAbout);
		}else if (clicked == "play")
		{
			WindowRect = GUI.Window(2, WindowRect, playFunc, "Choose Game Level");
		}else if (clicked == "resolution")
		{
			GUILayout.BeginVertical();
			for (int x = 0; x < Screen.resolutions.Length;x++ )
			{
				if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
				{
					Screen.SetResolution(Screen.resolutions[x].width,Screen.resolutions[x].height,true);
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Back"))
			{
				clicked = "options";
			}
			GUILayout.EndHorizontal();
		}
	}

	private void playFunc(int id)
	{
		if (GUILayout.Button("Level 1"))
		{
			//play game is clicked
			Application.LoadLevel(1);
			clicked = "play";
		}
		if (GUILayout.Button("Level 2"))
		{
			Application.LoadLevel(2);
			clicked = "play";
		}
	}
	
	private void optionsFunc(int id)
	{
		string birdsEyeStatus = "ON";
		if (AvatarCamController.can_switch) 
		{
			birdsEyeStatus = "OFF";
		}
		if (GUILayout.Button("Turn Birds'-Eye-View " + birdsEyeStatus))
		{
			AvatarCamController.can_switch = !AvatarCamController.can_switch;
			clicked = "";
		}
		if (GUILayout.Button("Resolution"))
		{
			clicked = "resolution";
		}
		GUILayout.Box("Volume");
		volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
		AudioListener.volume = volume;
		if (GUILayout.Button("Back"))
		{
			clicked = "";
		}
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if (GUILayout.Button("Play Game"))
		{
			//play game is clicked
//			Application.LoadLevel(2);
			clicked = "play";
		}
		if (GUILayout.Button("Options"))
		{
			clicked = "options";
		}
		if (GUILayout.Button("About"))
		{
			clicked = "about";
		}
		if (GUILayout.Button("Quit Game"))
		{
			Application.Quit();
		}
		if (DragWindow)
			GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
	
	private void Update()
	{
		if (clicked == "about" && Input.GetKey (KeyCode.Escape))
			clicked = "";
	}
}
