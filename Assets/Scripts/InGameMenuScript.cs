using UnityEngine;
using System.Collections;

public class InGameMenuScript : MonoBehaviour {

	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	public string[] AboutTextLines = new string[0];
	public static int levelToLoad = 1;
	
	private string clicked = "hide", MessageDisplayOnAbout = "About \n ";
	private Rect WindowRect = new Rect ((Screen.width / 2) - 100, Screen.height / 2, 200, 200);
	private Rect FloatButton = new Rect (0, Screen.height - 30, 100, 30);
	private float volume = 1.0f;
	
	private void OnGUI ()
	{
		if (background != null)
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture (new Rect ((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;
		if (clicked == "") {
			WindowRect = GUI.Window (0, WindowRect, menuFunc, "Main Menu");
		}
		if (clicked == "hide") {
			if (GUI.Button (FloatButton, "Menu")) {
				CharactorController.can_action = false;
				clicked = "";
			}
		}
		
	}
	
	private void menuFunc (int id)
	{
		//buttons 
		if (GUILayout.Button ("Resume")) {
			CharactorController.can_action = true;
			clicked = "hide";
		}
		if (GUILayout.Button ("Level 1")) {
			Application.LoadLevel (1);
			clicked = "";
		}
		if (GUILayout.Button ("Level 2")) {
			Application.LoadLevel (2);
			clicked = "";
		}
		if (GUILayout.Button ("Quit Level")) {
			Application.LoadLevel (0);
			clicked = "";
		}
		
	}

	private void update()
	{
		if (clicked == "hide" && Input.GetKey (KeyCode.Escape)) {
			CharactorController.can_action = false;
			clicked = "";
		}
	}
}
