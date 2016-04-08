using UnityEngine;
using System.Collections;

public class ConveyorBeltMovementBumpyBoy : MonoBehaviour {

	[SerializeField] private float m_speed = 0.1f;
	[SerializeField] private float m_bumpyness = 1f;
	[SerializeField] private float m_upper_y = 2f;
	[SerializeField] private float m_lower_y = -2f;

	void Start () {

	}

	void Update () {
		// Move in the circle
		Vector3 velocity = new Vector3(0,0,0);
		if (transform.position.z < 0 && transform.position.x < 0) {
			velocity.z += 1;
		} else if (transform.position.z < 0 && transform.position.x > 0) {
			velocity.z -= 1;
		} else {
			velocity.x = transform.position.z;
			velocity.z = -transform.position.x;
		}
		velocity = velocity.normalized * m_speed;
	    
		// Add bumpyness
		velocity.y = m_speed*m_bumpyness*Random.Range(-1f,1f);
		if (transform.position.y < m_lower_y)
			velocity.y = m_speed*m_bumpyness*Random.Range(0,1f);
		else if (transform.position.y > m_upper_y)
			velocity.y = m_speed*m_bumpyness*Random.Range(-1f,0);
		else
			velocity.y = m_speed*m_bumpyness*Random.Range(-1f,1f);
		// Move the bottle
		transform.Translate(velocity * Time.deltaTime);
	}
}
