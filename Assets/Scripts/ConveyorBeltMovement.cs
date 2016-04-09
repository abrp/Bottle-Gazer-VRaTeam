using UnityEngine;
using System.Collections;

public class ConveyorBeltMovement : MonoBehaviour {

	[SerializeField] private float m_speed = 0.1f;
	private Rigidbody m_Rigidbody;

	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	void Update () {
		if (m_Rigidbody.isKinematic) {
			Vector3 velocity = new Vector3 (0, 0, 0);
			if (transform.position.z < 0 && transform.position.x < 0) {
				velocity.z += 1;
			} else if (transform.position.z < 0 && transform.position.x > 0) {
				velocity.z -= 1;
			} else {
				velocity.x = transform.position.z;
				velocity.z = -transform.position.x;
			}

			velocity = velocity.normalized * m_speed;
			
			transform.Translate (velocity * Time.deltaTime);
		}
	}

}
