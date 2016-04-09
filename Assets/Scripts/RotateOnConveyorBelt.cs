using UnityEngine;
using System.Collections;

public class RotateOnConveyorBelt : MonoBehaviour {

	[SerializeField] private float m_speed = 10f;
	[SerializeField] private Vector3 m_direction;

	private Rigidbody m_Rigidbody;
	Quaternion startAngle;

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Rigidbody.isKinematic) {
			transform.Rotate (m_direction,m_speed*Time.deltaTime);
		}
	}
}
