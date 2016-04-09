using UnityEngine;
using System.Collections;

// Tags an item on the belt as BEING allowed to pass through
public class RightItem : MonoBehaviour, IScoringItem {
  [SerializeField] private int m_Score = 10;
  [SerializeField] private int m_Penalty = -10;

  public int GetEndOfBeltScore() {
    return m_Score;
  }

  public int GetPlayerRemovalScore() {
    return m_Penalty;
  }
}
