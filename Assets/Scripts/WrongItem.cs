using UnityEngine;
using System.Collections;

// Tags an item on the belt as NOT being supposed to pass through
public class WrongItem : MonoBehaviour, IScoringItem {

  [SerializeField] private int m_Score = 10;
  [SerializeField] private int m_Penalty = -10;

  public int GetPlayerRemovalScore() {
    return m_Score;
  }

  public int GetEndOfBeltScore() {
    return m_Penalty;
  }
}
