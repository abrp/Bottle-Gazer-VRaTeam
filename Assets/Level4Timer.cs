using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level4Timer : MonoBehaviour {

  [SerializeField] private float m_AfterSeconds;

	// Use this for initialization
	void Start () {
    Invoke("GoToNextLevel", m_AfterSeconds);
	}
	
	// Update is called once per frame
	void GoToNextLevel () {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
