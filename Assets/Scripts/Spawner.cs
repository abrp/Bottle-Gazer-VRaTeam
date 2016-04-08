using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	[SerializeField] private GameObject[] m_items;
	[SerializeField] private float m_spawnRate = 0.5f;
	private float m_nextSpawnTime = 0;

	void Start () {
		
	}
	

	void Update () {
		if (m_nextSpawnTime < Time.time) {
			Instantiate (m_items [0], transform.position, Quaternion.identity);
			m_nextSpawnTime = Time.time + m_spawnRate;
		}
	}
}
