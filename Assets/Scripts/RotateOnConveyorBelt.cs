using UnityEngine;
using System.Collections;

public class RotateOnConveyorBelt : MonoBehaviour, IMotionControllable {

	[SerializeField] private float m_speed = 10f;
	[SerializeField] private Vector3 m_direction;

	private Rigidbody m_Rigidbody;
	Quaternion startAngle;
  private bool m_IsInMotion = true;

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
    if (m_IsInMotion && m_Rigidbody.isKinematic) {
			transform.Rotate (m_direction,m_speed*Time.deltaTime);
		}
	}

  // Sets the object in motion
  public void StartMotion() {
    m_IsInMotion = true;
  }

  // Halts the object motion
  public void StopMotion() {
    m_IsInMotion = false;
  }

}
