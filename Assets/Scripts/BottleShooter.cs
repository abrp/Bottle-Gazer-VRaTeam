using UnityEngine;
using System.Collections;

public class BottleShooter : MonoBehaviour {
	
	[SerializeField] private Vector3 m_minVelocity;
	[SerializeField] private Vector3 m_maxVelocity;

	private Rigidbody m_Rigidbody;
	//private Collider m_Collider;

	// Use this for initialization
	void Start () {
		//m_Collider = GetComponent<Collider> ();
		m_Rigidbody = GetComponent<Rigidbody>();

		Vector3 randomDirection = new Vector3 (
			Random.Range (m_minVelocity.x, m_maxVelocity.x),
			Random.Range (m_minVelocity.y, m_maxVelocity.y),
			Random.Range (m_minVelocity.z, m_maxVelocity.z)
      );
	  m_Rigidbody.velocity = randomDirection;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
