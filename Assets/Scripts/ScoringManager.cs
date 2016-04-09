using UnityEngine;
using System;
using System.Collections;

public class ScoringManager : MonoBehaviour {

  private int m_CurrentScore;

  [SerializeField] private int m_InitialScore = 1000;

  public static ScoringManager instance = null; //Static instance of ScoringManager which allows it to be accessed by any other script.
  public event Action OnScoreChanged; // Called when the score changes

  //Awake is always called before any Start functions
  void Awake()
  {
    //Check if instance already exists
    if (instance == null)

      //if not, set instance to this
      instance = this;

    //If instance already exists and it's not this:
    else if (instance != this)

      //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a ScoringManager.
      Destroy(gameObject);    

    //Sets this to not be destroyed when reloading scene
    DontDestroyOnLoad(gameObject);
  }

	// Use this for initialization
	void Start () {
    m_CurrentScore = m_InitialScore;
	}

  public void ApplyScore(int score) {
    m_CurrentScore += score;

    Debug.Log("Current score: " + m_CurrentScore);

    if (OnScoreChanged != null)
      OnScoreChanged();
  }

  public int GetCurrentScore() {
    return m_CurrentScore;
  }

  public void PlayerRemovedItem(GameObject itemGameObject) {
    IScoringItem scoringItem = itemGameObject.GetComponent<IScoringItem>();

    if(scoringItem != null) {
      ApplyScore(scoringItem.GetPlayerRemovalScore());
    }
  }
}
