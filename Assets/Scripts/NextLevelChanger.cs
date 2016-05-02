using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class NextLevelChanger : MonoBehaviour {

  private VRInteractiveItem m_LevelChangerTrigger;
  private LevelManager m_LevelManager;

	// Use this for initialization
	void Start () {
    m_LevelChangerTrigger = this.GetComponent<VRInteractiveItem>();
    m_LevelChangerTrigger.OnClick += this.LevelChangerTriggerClicked;
    m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
	}

  private void LevelChangerTriggerClicked() {
    m_LevelManager.LoadNextLevel();
  }

}
