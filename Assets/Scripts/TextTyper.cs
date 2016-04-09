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
	private bool isActive = true;
	public static TextTyper instance = null;

	//public string message;
	public Text textComp;
	public string message;

	void Awake(){

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start(){
		audioSource = GetComponent<AudioSource> ();
		TypeTextOnScreen (2);
	}

	void Update(){

	}

	// Use this for initialization
	public void TypeTextOnScreen (float delay) {
		if (!isActive) {
			isActive = true;
		}

		textComp = GetComponent<Text>();
		message = textComp.text;
		textComp.text = "";
		StartCoroutine(TypeText (message, delay));
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
	}
}

