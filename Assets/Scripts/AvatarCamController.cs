using UnityEngine;
using System.Collections;

public class AvatarCamController : MonoBehaviour {

	public float bird_height;
	public float bird_angle;
	public float switch_time;  // the time period to complete view switching

	private bool is_switching;
	private bool is_at_bird; // whether the camera is at birds'-eye-view
	private Vector3 dest_pos;
	private Quaternion dest_rotate;   // the target position and rotation of the camera during switching

	// Use this for initialization
	void Start () {
		is_at_bird = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// do transition
		if (is_switching) {
			// if switching is completed
			if (transform.rotation.x == dest_rotate.x) {
				is_switching = false;
				is_at_bird = !is_at_bird;
				return;
			}

			// transit to target camera position
			transform.position = Vector3.MoveTowards(transform.position, dest_pos, bird_height / switch_time * Time.deltaTime);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, dest_rotate, bird_angle / switch_time * Time.deltaTime);
			return;
		}

		// triggers
		else if (Input.GetKeyDown("3")) {
			if (is_at_bird) {
				dest_pos = transform.position - new Vector3(0, bird_height, 0);
				dest_rotate = transform.rotation * Quaternion.Euler (-bird_angle, 0, 0);
			}
			else {
				dest_pos = transform.position + new Vector3(0, bird_height, 0);
				dest_rotate = transform.rotation * Quaternion.Euler (bird_angle, 0, 0);
			}
			is_switching = true;
		}
	}
}
