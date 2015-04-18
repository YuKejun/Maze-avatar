using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

	public Button levelOneButton;
	public Button levelTwoButton;
	public Button birdEyeOn;
	public Button birdEyeOff;
	public Button start;
	public Button quit;

	public void levelOneButtonClicked()
	{
		InGameMenuScript.levelToLoad = 1;
		levelOneButton.GetComponent<Image> ().color = Color.grey;
		levelTwoButton.GetComponent<Image> ().color = Color.black;
	}

	public void levelTwoButtonClicked()
	{
		InGameMenuScript.levelToLoad = 2;
		levelOneButton.GetComponent<Image> ().color = Color.black;
		levelTwoButton.GetComponent<Image> ().color = Color.grey;
	}

	public void birdEyeOnButtonClicked()
	{
		CharactorController.can_switch = true;
		birdEyeOn.GetComponent<Image> ().color = Color.grey;
		birdEyeOff.GetComponent<Image> ().color = Color.black;
	}

	public void birdEyeOffButtonClicked()
	{
		CharactorController.can_switch = false;
		birdEyeOn.GetComponent<Image> ().color = Color.black;
		birdEyeOff.GetComponent<Image> ().color = Color.grey;
	}

	public void startButtonClicked()
	{
		start.GetComponent<Image> ().color = Color.grey;
		Application.LoadLevel (3);
	}

	public void quitButtonClicked()
	{
		quit.GetComponent<Image> ().color = Color.grey;
		Application.Quit ();
	}


}
