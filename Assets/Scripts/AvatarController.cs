using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

	public float grid_size;
	public float walking_speed;
	public float rotating_speed;

	public Camera player_camera;
	public Camera main_camera;

	private bool is_walking;
	private Vector3 walking_dest;

	private bool is_rotating;
	private Quaternion rotate_dest;

	// Use this for initialization
	void Start () {
		is_walking = false;
		player_camera.enabled = true;
		main_camera.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (is_walking) {
			if (transform.position == walking_dest) {
				is_walking = false;
				return;
			}
			transform.position = Vector3.MoveTowards(transform.position, walking_dest, walking_speed * Time.deltaTime);
			return;
		}
		if (is_rotating) {
			if (transform.rotation == rotate_dest) {
				is_rotating = false;
				return;
			}
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate_dest, rotating_speed * Time.deltaTime);
			return;
		}

		if (!is_walking && Input.GetKey ("space")) {
			is_walking = true;
			walking_dest = transform.position + transform.rotation * Vector3.right * grid_size;
			return;
		}

		if (!is_rotating && Input.GetKeyDown (KeyCode.RightArrow)) {
			is_rotating = true;
			rotate_dest = transform.rotation * Quaternion.Euler (0, 90, 0);
			return;
		}
		if (!is_rotating && Input.GetKeyDown (KeyCode.LeftArrow)) {
			is_rotating = true;
			rotate_dest = transform.rotation * Quaternion.Euler (0, -90, 0);
			return;
		}

		if (Input.GetKeyDown ("1")) {
			player_camera.enabled = false;
			main_camera.enabled = true;
		} else if (Input.GetKeyDown ("2")) {
			main_camera.enabled = false;
			player_camera.enabled = true;
		}
	}
}
