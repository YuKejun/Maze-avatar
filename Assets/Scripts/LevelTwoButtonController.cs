using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTwoButtonController : MonoBehaviour {

	public void levelTwoPointerEnter()
	{
		Text label = gameObject.GetComponent<Text> ();
		label.text = "Coming Soon!";
		label.fontSize = 13;
	}
	
	public void levelTwoPointerExit() {
		Text label = gameObject.GetComponent<Text> ();
		label.text = "Level 2";
		label.fontSize = 20;
	}

}
