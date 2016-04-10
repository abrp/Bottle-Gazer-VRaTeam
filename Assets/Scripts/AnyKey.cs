using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils; 
using UnityEngine.UI;

public class AnyKey : MonoBehaviour {
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	[SerializeField] private bool m_DebugState= false;
	[SerializeField] private Color m_OverColor = new Color(1,0,0);
	[SerializeField] private Color m_OutColor = new Color(0,1,0);

	[SerializeField] private float m_DelayTime = 4f;
	private float TimeForNextMessage = 0f;

	[SerializeField] private string[] m_Strings;
	private int m_currentString = 0;

	private Renderer m_Renderer;
	private bool m_GazeOver;

	private void Awake(){
		if (m_InteractiveItem == null) {
			m_InteractiveItem = gameObject.AddComponent<VRInteractiveItem> ();
		}
	}

	private void Start(){
		m_Renderer = GetComponent<Renderer> ();
		m_currentString = m_Strings.Length;
		TimeForNextMessage = Time.time + m_DelayTime;
	}

	private void Update() {
		if (Time.time > TimeForNextMessage)
			DisplayNexMessage ();
	}

	private void OnEnable() {
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += HandleOnClick;
	}
		

	private void HandleOnClick() {
		DisplayNexMessage ();
	}

	private void DisplayNexMessage() {
		m_currentString++;
		m_currentString %= m_Strings.Length;
		//TextTyper.instance.message = "Push";
		TextTyper.instance.GetComponent<Text>().text = m_Strings[m_currentString];
		TextTyper.instance.TypeTextOnScreen (0);
		TimeForNextMessage += m_DelayTime;
	}

	private void HandleOver(){
		if(m_DebugState)
			m_Renderer.material.color = m_OverColor;
		m_GazeOver = true;
	}

	private void HandleOut(){
		if(m_DebugState)
			m_Renderer.material.color = m_OutColor;
		m_GazeOver = false;
	}
}
