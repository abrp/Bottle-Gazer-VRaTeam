using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextLevelOnDelay : MonoBehaviour {
	[SerializeField] private int m_Time = 6;
	// Use this for initialization
	void Start () {
		Invoke ("LoadNextLevel", m_Time);
	}
	
	// Update is called once per frame
	void LoadNextLevel () {
		Debug.Log (SceneManager.GetActiveScene ().buildIndex + 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
