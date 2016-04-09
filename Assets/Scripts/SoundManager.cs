using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[SerializeField] private int m_NumberOfVoices = 8;
	private int m_CurrentIndex = 0;
	private AudioSource[] m_AudioSources;
	public static SoundManager instance = null; 
	// Use this for initialization
	void Start () {
	
	}

	void Awake () { 
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


		m_AudioSources = new AudioSource[m_NumberOfVoices];
		for (int i = 0; i < m_NumberOfVoices; i++) {
			GameObject audioGameObject = new GameObject();
			audioGameObject.AddComponent<AudioSource> ();
			audioGameObject.transform.parent = this.transform;
			audioGameObject.name = "Dynamic AudioSource: " + i;
			m_AudioSources [i] = audioGameObject.GetComponent<AudioSource> ();
		}
	}
	
	public void Play(AudioClip audioClip,Vector3 position, float min, float max){
		if (audioClip != null) {
			m_AudioSources [m_CurrentIndex].transform.position = position;
			m_AudioSources[m_CurrentIndex].clip = audioClip;
			m_AudioSources [m_CurrentIndex].pitch = Random.Range (min, max);
			m_AudioSources[m_CurrentIndex].Play ();
			NextVoice();
		} else {
			Debug.LogError("No AudioClip");
		}
	}

	private void NextVoice(){
		m_CurrentIndex++;
		m_CurrentIndex = m_CurrentIndex % m_NumberOfVoices;
	}
}
