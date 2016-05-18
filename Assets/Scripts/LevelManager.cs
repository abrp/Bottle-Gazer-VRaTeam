using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

  [SerializeField, Range(0, 60)] private int m_LevelDelay;
  [SerializeField, Range(0, 10)] private float m_EndGraceSeconds = 5f;

  private HashSet<BeltItem> m_BeltItems;
  private bool m_LastBeltItemAdded = false;

	void Start () {
    m_BeltItems = new HashSet<BeltItem>();

    if(m_LevelDelay > 0) {
      Invoke("LoadNextLevel", m_LevelDelay);
    }
	}
	
  public void BeltItemAdded(BeltItem item) {
    m_BeltItems.Add(item);
  }

  public void BeltItemRemoved(BeltItem item) {
    m_BeltItems.Remove(item);

    if(m_LastBeltItemAdded && m_BeltItems.Count == 0) {
      Invoke("LoadNextLevel", m_EndGraceSeconds);
    }
  }

  public void LastBeltItemAdded() {
    m_LastBeltItemAdded = true;
  }

  public void LoadNextLevel() {
    var currentIndex = SceneManager.GetActiveScene().buildIndex;
    if(currentIndex >= (SceneManager.sceneCountInBuildSettings - 1)) {
      // Last scene, restart
      SceneManager.LoadScene(0);
    } else {
      Debug.Log("LevelManager now loading scene " + (currentIndex + 1));
      SceneManager.LoadScene(currentIndex + 1);
    }
  }
}
