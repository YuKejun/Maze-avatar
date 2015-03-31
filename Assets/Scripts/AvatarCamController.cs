using UnityEngine;
using System.Collections;

public class AvatarCamController : MonoBehaviour
{

		public float bird_height = 10.0f;
		public float bird_angle = 70.0f;
		public float switch_time = 1.0f;  // the time period to complete view switching

		public float final_radius = 0.6f;
		public float final_height = 0.6f;
		public float final_rot_speed = 15.0f;
		private bool final_view;
		private Vector3 final_center;

		public static bool can_switch = false;
		private bool is_switching;
		private bool is_at_bird; // whether the camera is at birds'-eye-view
		private Vector3 dest_pos;
		private Quaternion dest_rotate;   // the target position and rotation of the camera during switching

		// Use this for initialization
		void Start ()
		{
				is_at_bird = false;
				final_view = false;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				if (can_switch) {
						// do transition
						if (is_switching) {
								// if switching is completed
								if (transform.rotation == dest_rotate) {
										is_switching = false;
										is_at_bird = !is_at_bird;
										return;
								}

								// transit to target camera position
								transform.position = Vector3.MoveTowards (transform.position, dest_pos, bird_height / switch_time * Time.deltaTime);
								transform.rotation = Quaternion.RotateTowards (transform.rotation, dest_rotate, bird_angle / switch_time * Time.deltaTime);
								return;
						}

						if (final_view) {
								transform.LookAt (final_center);
								transform.RotateAround (final_center, Vector3.up, final_rot_speed * Time.deltaTime);
						}

			// triggers
			else if (Input.GetKeyDown ("3")) {
								if (is_at_bird) {
										dest_pos = transform.position - new Vector3 (0, bird_height, 0);
										dest_rotate = transform.rotation * Quaternion.Euler (-bird_angle, 0, 0);
								} else {
										dest_pos = transform.position + new Vector3 (0, bird_height, 0);
										dest_rotate = transform.rotation * Quaternion.Euler (bird_angle, 0, 0);
								}
								is_switching = true;
						}
				}
		}

		public bool isAtBirdEyeView ()
		{
				return is_at_bird;
		}

		public bool isSwitching ()
		{
				return is_switching;
		}

		public void enableSwitch (bool can_switch_)
		{
				can_switch = can_switch_;
		}

		public void finalView (Vector3 present_loc)
		{
				final_view = true;
				final_center = present_loc + new Vector3 (0, final_height, 0);//transform.position + new Vector3 (0, 0, 0.5f);
				transform.position = final_center + new Vector3 (0, 0, final_radius);
				transform.rotation = transform.rotation * Quaternion.Euler (0, 180, 0);
				transform.LookAt (final_center);
		}
}
