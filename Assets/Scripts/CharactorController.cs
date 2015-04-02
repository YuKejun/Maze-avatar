using UnityEngine;
using System.Collections;

public class CharactorController : MonoBehaviour
{
		// global parameters
		public float grid_size = 1.0f;
		public float walking_speed = 1.2f;
		public float rotating_speed = 90.0f;
	
		public Camera camera;

//		public float animationSpeed = 1.0f;
		public GUIText winText;
	
		private ArrayList animationList;//list of animations

//		private AvatarCamController camController;

		public static bool can_action;
		// for walking control
//		private bool is_walking;
		private bool can_walk;
		private Vector3 walking_origin;
		private Vector3 walking_dest;

		// for turning control
//		private bool is_rotating;
		private Quaternion rotate_dest;

		// for camera switching control
		public float bird_height = 10.0f;
		public float bird_angle = 70.0f;
		public float switch_time = 1.0f;  // the time period to complete view switching
		
		public float final_radius = 0.6f;
		public float final_height = 0.6f;
		public float final_rot_speed = 15.0f;
		public double final_time = 8.0f;
		private Vector3 final_center;
		
		public static bool can_switch = true;
		private Vector3 cam_dest_pos;
		private Quaternion cam_dest_rotate;   // the target position and rotation of the camera during switching

		private Rigidbody rigidbody;
	
		// Use this for initialization
		void Start ()
		{
//				is_walking = false;
				can_walk = true;
				can_action = true;
				rigidbody = GetComponent<Rigidbody> ();
				walking_origin = transform.position;
				camera.enabled = true;
//				camController = (AvatarCamController)camera.GetComponent ("AvatarCamController");

				//initiate animationList
				animationList = GetAnimationList ();
		}
	
		// Update is called once per frame
		void Update ()
		{
//				if (!camController.isAtBirdEyeView () && !camController.isSwitching () && can_action) {
				if (can_action) {
//						if (is_walking && Vector3.Distance (transform.position, walking_dest) <= walking_speed * Time.deltaTime) {
//								Debug.Log ("Stop!!!!!!!");
//								Stop ();
//								walking_origin = walking_dest;
//								return;
//						}
//			
//						if (is_rotating) {
//								if (transform.rotation == rotate_dest) {
//										is_rotating = false;
//										camController.enableSwitch (true);
//										return;
//								}
//								transform.rotation = Quaternion.RotateTowards (transform.rotation, rotate_dest, rotating_speed * Time.deltaTime);
//								return;
//						}
//			
//						if (can_walk && !is_walking && Input.GetKey ("space")) {
//								is_walking = true;
//								camController.enableSwitch (false);
//								walking_dest = walking_origin + transform.rotation * Vector3.forward * grid_size;
//								rigidbody.velocity = transform.rotation * Vector3.forward * walking_speed;
//
//								//animation
////								animation.CrossFade (animationList [1] as string, 0.01f);
//								return;
//						}
//			
//						if (checkAndTurn (KeyCode.RightArrow, 90)) {
//								return;
//						} else if (checkAndTurn (KeyCode.LeftArrow, -90)) {
//								return;
//						}

						if (Input.GetKey ("space") && can_walk) {
								StartCoroutine (Walk ());
						} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
								StartCoroutine (Turn (90));
						} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
								StartCoroutine (Turn (-90));
						} else if (Input.GetKeyDown ("3") && can_switch) {
								StartCoroutine (SwitchView ());
						}
						if (clicked == "hide" && Input.GetKey (KeyCode.Escape)) 
						{
							CharactorController.can_action = false;
							clicked = "";
						}
				}
		}

		private IEnumerator Walk ()
		{
				if (can_action) {
						can_action = false;
//				camController.enableSwitch(false);
						walking_dest = walking_origin + transform.rotation * Vector3.forward * grid_size;
						rigidbody.velocity = transform.rotation * Vector3.forward * walking_speed;
						while (Vector3.Distance (transform.position, walking_dest) > walking_speed * Time.deltaTime) {
								if (!can_walk) {
										yield break;
								}
								yield return null;
						}
						Stop ();
						walking_origin = walking_dest;
						can_action = true;
				}
		}

		private IEnumerator Turn (int rotation)
		{
				if (can_action) {
						can_action = false;
						can_walk = true;
						rotate_dest = transform.rotation * Quaternion.Euler (0, rotation, 0);
//				camController.enableSwitch (false);
						while (transform.rotation != rotate_dest) {
								transform.rotation = Quaternion.RotateTowards (transform.rotation, rotate_dest, rotating_speed * Time.deltaTime);
								yield return null;
						}
						can_action = true;
//				camController.enableSwitch (true);
				}
		}

		// When bump into walls
		void OnTriggerEnter (Collider other)
		{
				if (other.tag == "Present") {
						Stop ();
						can_walk = false;
						can_action = false;
						Destroy (other.gameObject);
						StartCoroutine (Congratulation ());
//						camController.finalView (transform.position);
//						animation.CrossFade (animationList [0] as string, 0.01f);
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
//				is_walking = false;
				can_action = true;
//				camController.enableSwitch (true);
				transform.position = walking_dest;
				rigidbody.velocity = new Vector3 (0, 0, 0);
//				animation.CrossFade (animationList [0] as string, 0.01f);
		}

		private IEnumerator SwitchView ()
		{
				can_action = false;
			
				cam_dest_pos = camera.transform.position + new Vector3 (0, bird_height, 0);
				cam_dest_rotate = camera.transform.rotation * Quaternion.Euler (bird_angle, 0, 0);

				while (camera.transform.rotation != cam_dest_rotate) {
						camera.transform.position = Vector3.MoveTowards (camera.transform.position, cam_dest_pos, bird_height / switch_time * Time.deltaTime);
						camera.transform.rotation = Quaternion.RotateTowards (camera.transform.rotation, cam_dest_rotate, bird_angle / switch_time * Time.deltaTime);
						yield return null;
				}
			
				// wait till user switches back
				while (!Input.GetKeyDown("3")) {
						yield return null;
				}
				cam_dest_pos = camera.transform.position - new Vector3 (0, bird_height, 0);
				cam_dest_rotate = camera.transform.rotation * Quaternion.Euler (-bird_angle, 0, 0);
				while (camera.transform.rotation != cam_dest_rotate) {
						camera.transform.position = Vector3.MoveTowards (camera.transform.position, cam_dest_pos, bird_height / switch_time * Time.deltaTime);
						camera.transform.rotation = Quaternion.RotateTowards (camera.transform.rotation, cam_dest_rotate, bird_angle / switch_time * Time.deltaTime);
						yield return null;
				}

				can_action = true;
		}

		private IEnumerator Congratulation ()
		{
				can_action = false;
				final_center = transform.position + new Vector3 (0, final_height, 0);//transform.position + new Vector3 (0, 0, 0.5f);
				camera.transform.position = final_center + new Vector3 (0, 0, final_radius);
				camera.transform.rotation = camera.transform.rotation * Quaternion.Euler (0, 180, 0);
				camera.transform.LookAt (final_center);
				animation.CrossFade (animationList [0] as string, 0.01f);
				winText.text = "YOU WIN!";
				float timer = 0;
				while (timer < final_time) {
//			Debug.Log (transform.position);
						timer += Time.deltaTime;
						can_action = false;
						camera.transform.LookAt (final_center);
						camera.transform.RotateAround (final_center, Vector3.up, final_rot_speed * Time.deltaTime);
						yield return null;
				}
				Application.LoadLevel (CharactorController.levelToLoad);
		}
	
//		private bool checkAndTurn (KeyCode keycode, int rotation)
//		{
//				if (!is_rotating && !is_walking && Input.GetKeyDown (keycode)) {
//						is_rotating = true;
//						rotate_dest = transform.rotation * Quaternion.Euler (0, rotation, 0);
//						camController.enableSwitch (false);
//						return true;
//				}
//				return false;
//		}
	
		private ArrayList GetAnimationList ()
		{
				ArrayList tmpArray = new ArrayList ();
		
				foreach (AnimationState state in gameObject.animation) {
						tmpArray.Add (state.name);
						print (state.name);
				}
		
				return tmpArray;
		}

	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	public string[] AboutTextLines = new string[0];
	public static int levelToLoad = 1;
	
	private string clicked = "hide", MessageDisplayOnAbout = "About \n ";
	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200);
	private Rect FloatButton = new Rect(0, Screen.height - 30, 100, 30);
	private float volume = 1.0f;
	
	private void OnGUI()
	{
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;
		if (clicked == "")
		{
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu");
		}
		if (clicked == "hide") 
		{
			if (GUI.Button(FloatButton, "Menu"))
			{
				CharactorController.can_action = false;
				clicked = "";
			}
		}
		
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if ( GUILayout.Button("Resume"))
		{
			CharactorController.can_action = true;
			clicked = "hide";
		}
		if (GUILayout.Button("Level 1"))
		{
			Application.LoadLevel(1);
			clicked = "";
		}
		if (GUILayout.Button("Level 2"))
		{
			Application.LoadLevel(2);
			clicked = "";
		}
		if (GUILayout.Button("Quit Level"))
		{
			Application.LoadLevel(0);
			clicked = "";
		}
		
	}

}
