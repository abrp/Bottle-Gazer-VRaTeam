using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Radio : MonoBehaviour {
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	[SerializeField] private Color m_OverColor = new Color(1,0,0);
	[SerializeField] private Color m_OutColor = new Color(0,1,0);
	[SerializeField] private bool m_DebugState= false;
	[SerializeField] private AudioClip m_Noise;
	[SerializeField] private AudioClip[] m_AudioClips;
	[SerializeField] private bool m_IsChanging = false;

	private AudioSource m_AudioScource;

	private Renderer m_Renderer;
	private bool m_GazeOver;

	private int currentTrack = 0;

	private void Awake(){
		if (m_InteractiveItem == null) {
			m_InteractiveItem = gameObject.AddComponent<VRInteractiveItem> ();
		}
	}

	private void Start(){
		m_Renderer = GetComponent<Renderer> ();
		m_AudioScource = GetComponent<AudioSource> ();

		currentTrack = 1
		m_AudioScource.clip = m_AudioClips[currentTrack];
		m_AudioScource.time = Random.Range(0f, m_AudioClips[currentTrack].length);
		m_AudioScource.Play ();

	}
		
	private void OnEnable(){
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += Switch;
	}

	IEnumerator ChangeRadioStation(){
		// Do something
		m_IsChanging = true;
		m_AudioScource.Stop ();
		m_AudioScource.time = 0;
		m_AudioScource.clip = m_Noise;
		m_AudioScource.Play ();
		yield return new WaitForSeconds(m_Noise.length);
		currentTrack++;
		currentTrack %= m_AudioClips.Length;
		Debug.Log("Click on radio: currentTrack = " + currentTrack);
		m_AudioScource.Stop ();
		m_AudioScource.clip = m_AudioClips[currentTrack];
		m_AudioScource.time = Random.Range(0f, m_AudioClips[currentTrack].length);
		m_AudioScource.Play ();
		m_IsChanging = false;

	}

	void Switch() {
		if (!m_IsChanging) {
			StartCoroutine (ChangeRadioStation());
		}
	}

	private void HandleOver(){
		if (m_DebugState) {
			m_Renderer.material.color = m_OverColor;
		}
		m_GazeOver = true;
	}

	private void HandleOut(){
		if (m_DebugState) {
			m_Renderer.material.color = m_OutColor;
		}
		m_GazeOver = false;
	}
}
