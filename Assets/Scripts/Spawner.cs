using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	[SerializeField] private GameObject[] m_items;
	[SerializeField] private float m_spawnRate = 0.5f;
	private float m_nextSpawnTime;

	void Start () {
		
	}
	

	void Update () {
		if (m_nextSpawnTime > Time.time) {

		}
	}
}
