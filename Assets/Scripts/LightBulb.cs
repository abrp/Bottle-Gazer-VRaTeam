using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

	[SerializeField] private Color m_OnColor;
	[SerializeField] private Color m_OffColor;

	[SerializeField] private float delay = 1f;

  private Renderer m_Renderer;
	private bool m_IsPlaying = false;

	void Start () {
		m_Renderer = GetComponent<Renderer> ();
		m_Renderer.material.color = m_OffColor;
		m_Renderer.material.SetColor("_EmissionColor",m_OffColor);
	}

	public void Flash(){
		if (!m_IsPlaying) {
			StartCoroutine (TurnOn ());
		}
	}

	IEnumerator TurnOn(){
		m_IsPlaying = true;
		m_Renderer.material.color = m_OnColor;
		m_Renderer.material.SetColor("_EmissionColor",m_OnColor);
		yield return new WaitForSeconds (delay);
		m_Renderer.material.color = m_OffColor;
		m_Renderer.material.SetColor("_EmissionColor",m_OffColor);
		m_IsPlaying = false;
	}
}
