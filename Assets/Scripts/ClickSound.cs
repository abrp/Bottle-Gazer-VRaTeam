using UnityEngine;
using System.Collections;

public class ClickSound : MonoBehaviour {

	[SerializeField] AudioClip m_AudioClip;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			SoundManager.instance.Play (m_AudioClip, transform.position, 1, 1);
		}
	}
}
