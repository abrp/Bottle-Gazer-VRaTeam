using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRStandardAssets.Utils;

public class TutorialManager : MonoBehaviour {

  [SerializeField] private string m_CompletionText;
  [SerializeField] private float m_WaitAfterCompletion;

  private VRInteractiveItem m_InteractiveItem;
  private Text m_MonitorText;
  private LevelManager m_LevelManager;

	// Use this for initialization
	void Start () {
    m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
    m_MonitorText = GameObject.Find("MonitorText").GetComponent<Text>();
    m_InteractiveItem = this.GetComponent<VRInteractiveItem>();
    m_InteractiveItem.OnClick += this.TutorialCompleted;
	}
	
  void TutorialCompleted () {
    m_MonitorText.text = m_CompletionText;
    Invoke("LoadNextLevel", m_WaitAfterCompletion);
	}

  void LoadNextLevel() {
    m_LevelManager.LoadNextLevel();
  }
}
