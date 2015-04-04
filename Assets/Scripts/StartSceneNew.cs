using UnityEngine;
using System.Collections;

public class StartSceneNew : MonoBehaviour {

	public void levelOneButtonClicked()
	{
		InGameMenuScript.levelToLoad = 1;
	}

	public void levelTwoButtonClicked()
	{
		InGameMenuScript.levelToLoad = 2;
	}

	public void birdEyeOnButtonClicked()
	{
		CharactorController.can_switch = true;
	}

	public void birdEyeOffButtonClicked()
	{
		CharactorController.can_switch = false;
	}

	public void startButtonClicked()
	{
		Application.LoadLevel (3);
	}

	public void quitButtonClicked()
	{
		Application.Quit ();
	}


}
