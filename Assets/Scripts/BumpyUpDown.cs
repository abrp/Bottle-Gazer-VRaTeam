using UnityEngine;
using System.Collections;

public class BumpyUpDown : MonoBehaviour {

	[SerializeField] private float m_Bumpyness = 0.02f;
	[SerializeField] private float m_UpDownSkin = 0.3f;

	private float lowerBound;
	private float upperBound;
	private float force;
	private Vector3 velocity = new Vector3 (0, 0, 0);	// private float Speed;

	private Rigidbody m_Rigidbody;
	private Collider m_Collider;

	// Use this for initialization
	void Start () {
		m_Collider = GetComponent<Collider> ();
		m_Rigidbody = GetComponent<Rigidbody>();

		lowerBound = transform.position.y;
		upperBound = transform.position.y+m_UpDownSkin;

		Debug.Log (" collider = " + m_Collider.transform.position.y);
		Debug.Log (" transform = " + transform.position.y);
		Debug.Log (" lowerBound = " + lowerBound);
		Debug.Log (" lowerBound = " + upperBound);

		force = 0f;
		velocity = new Vector3 (0, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {

		if (m_Rigidbody.isKinematic) {

			// Add bumpyness
			if (transform.position.y < lowerBound) {
				force = Random.Range (0, m_Bumpyness);
				velocity.y = 0f;
			} else if (transform.position.y > upperBound) {
				force = Random.Range (-m_Bumpyness, 0);
				velocity.y = 0f;
			} else {
				force = Random.Range (-m_Bumpyness, m_Bumpyness);
			}
			// Move the bottle
			velocity.y += force*Time.deltaTime;
			transform.Translate (velocity * Time.deltaTime);
		}
	}
}
