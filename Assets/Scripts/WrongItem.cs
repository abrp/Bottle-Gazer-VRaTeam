using UnityEngine;
using System.Collections;

// Tags an item on the belt as NOT being supposed to pass through
public class WrongItem : MonoBehaviour {

  [SerializeField] private int m_Score = -10;

  public int GetScore() {
    return m_Score;
  }
}
