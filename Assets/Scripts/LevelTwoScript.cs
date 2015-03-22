using UnityEngine;
using System.Collections;

public class LevelTwoScript : MonoBehaviour {

	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	public string[] AboutTextLines = new string[0];
	
	
	private string clicked = "hide", MessageDisplayOnAbout = "About \n ";
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
		if (clicked == "hide") 
		{
		}
		
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if (GUILayout.Button("Resume"))
		{
			clicked = "hide";
		}
		if (GUILayout.Button("Level 1"))
		{
			Application.LoadLevel(1);
			clicked = "";
		}
		if (GUILayout.Button("Level 2"))
		{
			Application.LoadLevel(2);
			clicked = "";
		}
		if (GUILayout.Button("Quit Level"))
		{
			Application.LoadLevel(0);
			clicked = "";
		}
		
	}
	
	private void Update()
	{
		if (clicked == "hide" && Input.GetKey (KeyCode.Escape))
			clicked = "";
	}
}
