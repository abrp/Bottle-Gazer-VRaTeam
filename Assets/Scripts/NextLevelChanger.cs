using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class NextLevelChanger : MonoBehaviour {

  private VRInteractiveItem m_LevelChangerTrigger;

	// Use this for initialization
	void Start () {
    m_LevelChangerTrigger = this.GetComponent<VRInteractiveItem>();
    m_LevelChangerTrigger.OnClick += this.LevelChangerTriggerClicked;
	}

  private void LevelChangerTriggerClicked() {
    LevelManager.instance.LoadNextLevel();
  }

}
