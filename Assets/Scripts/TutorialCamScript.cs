using UnityEngine;
using System.Collections;

public class TutorialCamScript : MonoBehaviour {
	
	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public string[] AboutTextLines = new string[0];
	
	private int levelToPlay = 1;
	private string clicked = "", MessageDisplayOnAbout = "About \n ";

	private void OnGUI()
	{
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;
		
		if (GUI.Button (new Rect ((0), Screen.height - 20, 60, 20), "Skip"))
		{
			Application.LoadLevel(LevelOneScript.levelToLoad);
		}
		
	}
	
}
