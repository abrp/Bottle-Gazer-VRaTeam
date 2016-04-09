using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Destroyer : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<VRInteractiveItem>()){
			Destroy (other.gameObject);
		}
	}
}
