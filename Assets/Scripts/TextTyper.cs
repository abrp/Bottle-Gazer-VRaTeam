using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour {
	
	public float letterPause = 0.1f;
	public float pauseTime = 0.1f;
	private AudioSource audioSource;
	public AudioClip writerSound;
	public AudioClip noticationStart;
	public AudioClip noticationEnd;
	public char pauseChar;
	public char clearScreen;
	public Animator animator;

	public Text textComp;
	public string message;

  private bool m_IsTyping = false;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
		TypeTextOnScreen (0);
	}

  public bool IsTyping() {
    return m_IsTyping;
  }

	// Use this for initialization
	public bool TypeTextOnScreen (float delay) {
    if(!m_IsTyping) {
      m_IsTyping = true;
      textComp = GetComponent<Text>();
      message = textComp.text;
      textComp.text = "";
      StartCoroutine(TypeText(message, delay));
      return true;
    } else {
      return false;
    }
	}
	
	IEnumerator TypeText (string message, float delay) {
		yield return new WaitForSeconds (delay);
		foreach (char letter in message.ToCharArray()) {
			audioSource.clip = writerSound;
			if (letter == pauseChar) {
				yield return new WaitForSeconds (pauseTime);
				//audioSource.clip = noticationStart;
			}else if(letter == clearScreen){
				textComp.text = "";
				audioSource.clip = noticationEnd;
				audioSource.Play ();
			} else {
				textComp.text += letter;
				if(audioSource != null){
					audioSource.Play ();
				}else{
					Debug.LogError("AudioClip");
				}
			}
			yield return new WaitForSeconds (letterPause);
		}
		yield return new WaitForSeconds (0.5f);
    m_IsTyping = false;
	}
}

