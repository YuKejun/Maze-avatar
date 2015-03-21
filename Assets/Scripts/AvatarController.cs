using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	public float grid_size;
	public float walking_speed;
	public float rotating_speed;

	public Camera camera;
	private AvatarCamController camController;

	private bool is_walking;
	private bool can_walk;
	private Vector3 walking_origin;
	private Vector3 walking_dest;

	private bool is_rotating;
	private Quaternion rotate_dest;

	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		is_walking = false;
		can_walk = true;
		rigidbody = GetComponent<Rigidbody>();
		walking_origin = transform.position;
		camera.enabled = true;
		camController = (AvatarCamController) camera.GetComponent ("AvatarCamController");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!camController.isAtBirdEyeView() && !camController.isSwitching()) {
			if (is_walking && Vector3.Distance (transform.position, walking_dest) <= walking_speed * Time.deltaTime) {
				Stop ();
				walking_origin = walking_dest;
				return;
			}

			if (is_rotating) {
				if (transform.rotation == rotate_dest) {
					is_rotating = false;
					camController.enableSwitch(false);
					return;
				}
				transform.rotation = Quaternion.RotateTowards (transform.rotation, rotate_dest, rotating_speed * Time.deltaTime);
				return;
			}

			if (can_walk && !is_walking && Input.GetKey ("space")) {
				Debug.Log ("Walk!!!!!!!!!!!!!!!!");
				is_walking = true;
				camController.enableSwitch(false);
				walking_dest = walking_origin + transform.rotation * Vector3.right * grid_size;
				rigidbody.velocity = transform.rotation * Vector3.right * walking_speed;
				return;
			}

			if (checkAndTurn (KeyCode.RightArrow, 90)) {
				return;
			} else if (checkAndTurn (KeyCode.LeftArrow, -90)) {
				return;
			}
		}
	}

	// When bump into walls
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Bang!!!!!!!!!!!!!!!!");
		Stop ();
		can_walk = false;
		transform.position = walking_origin;
	}

	void OnTriggerExit(Collider other) {
		can_walk = true;
	}

	private void Stop() {
		is_walking = false;
		camController.enableSwitch(true);
		rigidbody.velocity = new Vector3(0, 0, 0);
	}

	private bool checkAndTurn(KeyCode keycode, int rotation) {
		if (!is_rotating && !is_walking && Input.GetKeyDown (keycode)) {
			is_rotating = true;
			rotate_dest = transform.rotation * Quaternion.Euler (0, rotation, 0);
			camController.enableSwitch(false);
			return true;
		}
		return false;
	}
}
