using UnityEngine;
using System.Collections;

// Tags an item on the belt as BEING allowed to pass through
public class RightItem : MonoBehaviour {
  [SerializeField] private int m_Score = 10;

  public int GetScore() {
    return m_Score;
  }
}
