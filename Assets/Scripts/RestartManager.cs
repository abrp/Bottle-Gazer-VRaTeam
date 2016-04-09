using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInput))]
public class RestartManager : MonoBehaviour {

  private VRInput m_VRInput;

	// Use this for initialization
	void Start () {
    m_VRInput = this.GetComponent<VRInput>();
    m_VRInput.OnCancel += this.OnBackButtonClicked;
	}
	
  void OnBackButtonClicked() {
    SceneManager.LoadScene(0);
  }

}
