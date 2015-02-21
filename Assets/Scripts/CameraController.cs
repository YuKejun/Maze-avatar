using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// for follow cameras, procedural animations, and other states
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
