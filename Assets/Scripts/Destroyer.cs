using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Destroyer : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
    if(other.GetComponent<VRInteractiveItem>()) {
      ScoringManager.instance.ItemReachedEndOfBelt(other.gameObject);
      LevelManager.instance.BeltItemRemoved(other.gameObject.GetComponent<BeltItem>());
      Destroy(other.gameObject);
    }
	}
}
