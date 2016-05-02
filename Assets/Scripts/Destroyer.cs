using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Destroyer : MonoBehaviour {

  private LevelManager m_LevelManager;

  void Start () {
    m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
  }

	void OnTriggerEnter(Collider other) {
    if(other.GetComponent<VRInteractiveItem>()) {
      ScoringManager.instance.ItemReachedEndOfBelt(other.gameObject);
      if (m_LevelManager != null) {
        m_LevelManager.BeltItemRemoved(other.gameObject.GetComponent<BeltItem>());
	}
      Destroy(other.gameObject);
    }
	}
}
