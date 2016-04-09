using UnityEngine;
using System.Collections;

public class BumpyUpDown : MonoBehaviour {

	[SerializeField] private float m_Bumpyness = 1f;

	private float lowerBound;
	private float upperBound;
	private float Speed;

	private Rigidbody m_Rigidbody;

	private Collider m_Collider;

	// Use this for initialization
	void Start () {
		m_Collider = GetComponent<Collider> ();
		m_Rigidbody = GetComponent<Rigidbody>();

		lowerBound = transform.position.y;
		upperBound = transform.position.y+0.01f;
		Speed = 0;

		Debug.Log (" collider = " + m_Collider.transform.position.y);
		Debug.Log (" transform = " + transform.position.y);

	}
	
	// Update is called once per frame
	void Update () {

		//if (m_Rigidbody.isKinematic) {
			
			Vector3 velocity = new Vector3 (0, 0, 0);

			// Add bumpyness
			velocity.y = m_Bumpyness * Random.Range (-1f, 1f);
			if (transform.position.y < lowerBound)
				velocity.y = m_Bumpyness * Random.Range (0, 1f);
			else if (transform.position.y > upperBound)
				velocity.y = m_Bumpyness * Random.Range (-1f, 0);
			else
				velocity.y = m_Bumpyness * Random.Range (-1f, 1f);
			// Move the bottle
			transform.Translate (velocity * Time.deltaTime);
		}
	//}
}
