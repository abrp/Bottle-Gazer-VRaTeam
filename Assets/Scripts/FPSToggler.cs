using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class FPSToggler : MonoBehaviour {

  private VRFPSCounter m_FPSCounter;
  private VRInteractiveItem m_InteractiveItem;

	// Use this for initialization
	void Start () {
    m_FPSCounter = GameObject.FindObjectOfType<VRFPSCounter>();
    m_InteractiveItem = this.GetComponent<VRInteractiveItem>();
    m_InteractiveItem.OnClick += m_FPSCounter.Toogle;
	}
}
