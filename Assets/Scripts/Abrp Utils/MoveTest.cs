using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

	[SerializeField] float m_speed = 1;

	void Start () {
	
	}

	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * m_speed);
	}
}
