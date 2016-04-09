using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils; 
using UnityEngine.UI;

public class AnyKey : MonoBehaviour {
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	[SerializeField] private bool m_DebugState= false;
	[SerializeField] private Color m_OverColor = new Color(1,0,0);
	[SerializeField] private Color m_OutColor = new Color(0,1,0);

	[SerializeField] private string[] m_Strings;
	private int m_currentString = 0;

	private Renderer m_Renderer;
	private bool m_GazeOver;

	private void Start(){
		m_Renderer = GetComponent<Renderer> ();

		m_currentString = 0;
		//TextTyper.instance.message = "Push";
		//TextTyper.instance.GetComponent<Text>().text = "push";
		//TextTyper.instance.TypeTextOnScreen (0);
	}

	private void Awake(){
		if (m_InteractiveItem == null) {
			m_InteractiveItem = gameObject.AddComponent<VRInteractiveItem> ();
		}
	}

	private void OnEnable(){
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += HandleOnClick;
	}
		

	private void HandleOnClick() {
		m_currentString++;
		m_currentString %= m_Strings.Length;
		//TextTyper.instance.message = "Push";
		TextTyper.instance.GetComponent<Text>().text = m_Strings[m_currentString];
		TextTyper.instance.TypeTextOnScreen (0);	
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
