using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	// called before rendering a frame
	// for game code
	void Update() {

	}

	// called before performing physics calculation
	// for physics code
	void FixedUpdate() {
		float moveHorinzontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorinzontal, 0, moveVertical);
		rigidbody.AddForce (movement * speed * Time.deltaTime);
	}
}