using UnityEngine;
using System.Collections;

public class DuskLightController : MonoBehaviour {

	public float duration = 1.0f;
	public float targetIntensity = 0.5f;
	public Light light;
	
	// Use this for initialization
	void Start () {
		light.intensity = 0;
		StartCoroutine (InitializeLight (duration));
	}

	IEnumerator InitializeLight(float duration) {
		while (Mathf.Abs(light.intensity - targetIntensity) > 0.05f) {
			light.intensity = Mathf.MoveTowards (light.intensity, targetIntensity, targetIntensity / duration * Time.deltaTime);
			yield return null;
		}
	}
}
