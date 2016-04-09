using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

	private Color m_OnColor;
	private Color m_OffColor;
	private Renderer m_Renderer;
	private float delay;

	void Start () {
		m_Renderer = GetComponent<Renderer> ();
		m_Renderer.material.color = m_OffColor;
	}

	public void TurnOn(){
		StartCoroutine (TurningOn ());
	}

	IEnumerator TurningOn(){
		yield return new WaitForSeconds (delay);
	}
}
