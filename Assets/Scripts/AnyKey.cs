using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils; 
using UnityEngine.UI;

public class AnyKey : MonoBehaviour {
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	[SerializeField] private float m_DelayTime = 4f;
  [SerializeField] private string[] m_Strings;

  private float TimeForNextMessage = 0f;
	private int m_currentString = 0;

  private TextTyper m_TextTyper;

	private void Awake(){
		if (m_InteractiveItem == null) {
			m_InteractiveItem = gameObject.AddComponent<VRInteractiveItem> ();
		}
	}

	private void Start(){
    m_TextTyper = GameObject.FindObjectOfType<TextTyper>();
		m_currentString = -1;
		DisplayNexMessage ();
		TimeForNextMessage = Time.time + m_DelayTime;
	}

	private void Update() {
		if (Time.time > TimeForNextMessage)
			DisplayNexMessage ();
	}

	private void OnEnable(){
		m_InteractiveItem.OnClick += HandleOnClick;
	}
		

	private void HandleOnClick() {
		DisplayNexMessage ();
	}

	private void DisplayNexMessage() {
		m_currentString++;
		m_currentString %= m_Strings.Length;
		m_TextTyper.GetComponent<Text>().text = m_Strings[m_currentString];
    m_TextTyper.TypeTextOnScreen (0);
		TimeForNextMessage += m_DelayTime;
	}
}
