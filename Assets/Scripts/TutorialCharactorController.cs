using UnityEngine;
using System.Collections;

public class TutorialCharactorController : MonoBehaviour
{
	
		public float grid_size;
		public float walking_speed;
		public float rotating_speed;
	
		public Camera camera;
	
		public float animationSpeed;
//		public GUIText winText;
	
		private ArrayList animationList;//list of animations
	
		private AvatarCamController camController;
	
		private bool is_walking;
		private bool can_walk;
		private Vector3 walking_origin;
		private Vector3 walking_dest;
	
		private bool is_rotating;
		private Quaternion rotate_dest;
	
		private Rigidbody rigidbody;
	
		// Use this for initialization
		void Start ()
		{
				is_walking = false;
				can_walk = true;
				rigidbody = GetComponent<Rigidbody> ();
				walking_origin = transform.position;
				camera.enabled = true;
				camController = (AvatarCamController)camera.GetComponent ("AvatarCamController");
		
				//initiate animationList
				animationList = GetAnimationList ();
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				if (!camController.isAtBirdEyeView () && !camController.isSwitching ()) {
						if (is_walking && Vector3.Distance (transform.position, walking_dest) <= walking_speed * Time.deltaTime) {
								Debug.Log ("Stop!!!!!!!");
								Stop ();
								walking_origin = walking_dest;
								return;
						}
			
						if (is_rotating) {
								if (transform.rotation == rotate_dest) {
										is_rotating = false;
										camController.enableSwitch (true);
										return;
								}
								transform.rotation = Quaternion.RotateTowards (transform.rotation, rotate_dest, rotating_speed * Time.deltaTime);
								return;
						}
			
						if (can_walk && !is_walking && Input.GetKey ("space")) {
								is_walking = true;
								camController.enableSwitch (false);
								walking_dest = walking_origin + transform.rotation * Vector3.forward * grid_size;
								rigidbody.velocity = transform.rotation * Vector3.forward * walking_speed;
				
								//animation
								animation.CrossFade (animationList [1] as string, 0.01f);
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
		void OnTriggerEnter (Collider other)
		{
				if (other.tag == "Present") {
						Stop ();
						can_walk = false;
						Destroy (other.gameObject);
						camController.finalView (transform.position);
						animation.CrossFade (animationList [0] as string, 0.01f);
//						winText.text = "YOU WIN!";
						return;
				}
				Stop ();
				can_walk = false;
				transform.position = walking_origin;
		}
	
		void OnTriggerExit (Collider other)
		{
				can_walk = true;
		}
	
		private void Stop ()
		{
				is_walking = false;
				camController.enableSwitch (true);
				transform.position = walking_dest;
				rigidbody.velocity = new Vector3 (0, 0, 0);
				animation.CrossFade (animationList [0] as string, 0.01f);
		}
	
		private bool checkAndTurn (KeyCode keycode, int rotation)
		{
				if (!is_rotating && !is_walking && Input.GetKeyDown (keycode)) {
						is_rotating = true;
						rotate_dest = transform.rotation * Quaternion.Euler (0, rotation, 0);
						camController.enableSwitch (false);
						return true;
				}
				return false;
		}
	
		private ArrayList GetAnimationList ()
		{
				ArrayList tmpArray = new ArrayList ();
		
				foreach (AnimationState state in gameObject.animation) {
						tmpArray.Add (state.name);
						print (state.name);
				}
		
				return tmpArray;
		}
}
