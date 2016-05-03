using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoringManager : MonoBehaviour {

  private int m_CurrentScore;
  private Text m_ScoreText;
	[SerializeField] private LightBulb m_RedBulb;
	[SerializeField] private LightBulb m_GreenBulb;
  [SerializeField] private Destroyer m_Destroyer;

  [SerializeField] private int m_InitialScore = 1000;

  [SerializeField] private AudioClip m_PlayerRemovedCorrectItem;
  [SerializeField] private AudioClip m_PlayerRemovedWrongItem;
  [SerializeField] private AudioClip m_CorrectItemReachedEndOfBelt;
  [SerializeField] private AudioClip m_WrongItemReachedEndOfBelt;

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

    m_ScoreText = this.GetComponentInChildren<Text>();
    UpdateScoreText();
	}

  public void ApplyScore(int score) {
    m_CurrentScore += score;

    UpdateScoreText();

		if (score > 0) {
			m_GreenBulb.Flash ();
		} else {
			m_RedBulb.Flash ();
		}

    if (OnScoreChanged != null)
      OnScoreChanged();
  }

  public int GetCurrentScore() {
    return m_CurrentScore;
  }

  // Call this when player removes an item, ie. pushes a bottle off the belt
  public void PlayerRemovedItem(GameObject itemGameObject) {
    IScoringItem scoringItem = itemGameObject.GetComponent<IScoringItem>();

    if(scoringItem != null) {
      var score = scoringItem.GetPlayerRemovalScore();
      ApplyScore(score);

      // Sound plays at the score board
      if(score > 0) {
        SoundManager.instance.Play(m_PlayerRemovedCorrectItem, m_GreenBulb.transform.position, 0.9f, 1f);
      } else {
        SoundManager.instance.Play(m_PlayerRemovedWrongItem, m_RedBulb.transform.position, 0.9f, 1f);      
      }
    }
  }

  // Call this when player removes an item, ie. pushes a bottle off the belt
  public void ItemReachedEndOfBelt(GameObject itemGameObject) {
    IScoringItem scoringItem = itemGameObject.GetComponent<IScoringItem>();

    if(scoringItem != null) {
      var score = scoringItem.GetEndOfBeltScore();
      ApplyScore(score);

      // Sound plays at end of belt
      if(score > 0) {
        SoundManager.instance.Play(m_CorrectItemReachedEndOfBelt, m_Destroyer.transform.position, 0.9f, 1f);
      } else {
        SoundManager.instance.Play(m_WrongItemReachedEndOfBelt, m_Destroyer.transform.position, 0.9f, 1f);      
      }
    }
  }

  private void UpdateScoreText() {
    m_ScoreText.text = m_CurrentScore.ToString();
  }
}
