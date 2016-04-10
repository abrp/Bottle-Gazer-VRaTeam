using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	[SerializeField] private float rotationSpeedVertical = 1;
	[SerializeField] private float rotationSpeedHorizontal = 2;
	// Use this for initialization
	void Start () {
		rotationSpeedVertical = Random.Range (-32f, 32f);
		rotationSpeedHorizontal = Random.Range (-32f, 32f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, rotationSpeedVertical * Time.deltaTime);
		transform.Rotate (Vector3.left, rotationSpeedHorizontal * Time.deltaTime);
	}
}
