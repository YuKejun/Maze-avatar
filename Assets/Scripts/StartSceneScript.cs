using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

	public Button levelOneButton;
	public Button levelTwoButton;
	public Button birdEyeOn;
	public Button birdEyeOff;

	public void levelOneButtonClicked()
	{
		InGameMenuScript.levelToLoad = 1;
		levelOneButton.GetComponent<Image> ().color = Color.white;
		levelTwoButton.GetComponent<Image> ().color = Color.black;
	}

	public void levelTwoButtonClicked()
	{
		InGameMenuScript.levelToLoad = 2;
		levelOneButton.GetComponent<Image> ().color = Color.black;
		levelTwoButton.GetComponent<Image> ().color = Color.white;
	}

	public void birdEyeOnButtonClicked()
	{
		CharactorController.can_switch = true;
		birdEyeOn.GetComponent<Image> ().color = Color.white;
		birdEyeOff.GetComponent<Image> ().color = Color.black;
	}

	public void birdEyeOffButtonClicked()
	{
		CharactorController.can_switch = false;
		birdEyeOn.GetComponent<Image> ().color = Color.black;
		birdEyeOff.GetComponent<Image> ().color = Color.white;
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
