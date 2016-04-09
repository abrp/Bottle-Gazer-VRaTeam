using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

  [SerializeField, Range(0, 60)] private int[] m_LevelDelays;
  [SerializeField, Range(0, 10)] private float m_EndGraceSeconds = 5f;

  public static LevelManager instance = null; //Static instance of LevelManager which allows it to be accessed by any other script.
  private HashSet<BeltItem> m_BeltItems;
  private bool m_LastBeltItemAdded = false;

  //Awake is always called before any Start functions
  void Awake()
  {
    //Check if instance already exists
    if (instance == null)

      //if not, set instance to this
      instance = this;

    //If instance already exists and it's not this:
    else if (instance != this)

      //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a LevelManager.
      Destroy(gameObject);    

    //Sets this to not be destroyed when reloading scene
    DontDestroyOnLoad(gameObject);
  }

	// Use this for initialization
	void Start () {
    m_BeltItems = new HashSet<BeltItem>();

    var levelDelay = m_LevelDelays[SceneManager.GetActiveScene().buildIndex];
    if(levelDelay > 0) {
      Invoke("LoadNextLevel", levelDelay);
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
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
