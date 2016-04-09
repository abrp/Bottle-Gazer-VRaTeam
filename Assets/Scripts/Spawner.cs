using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	[SerializeField] private GameObject[] m_Items;
	[SerializeField] private float m_SpawnRate = 0.5f;
	[SerializeField] private int[] m_NumberOfSpawnItems; // size
	private float m_NextSpawnTime;
	private int[] m_PermutationTable;
	private int m_CurrentNumberOfItems = 0;	// size 20

	void Start () {
		SetupPermutationTable ();
	}

	private void SetupPermutationTable(){

		for (int n = 0; n < m_NumberOfSpawnItems.Length; n++) {
			m_CurrentNumberOfItems += m_NumberOfSpawnItems [n];
		}

		m_PermutationTable = new int[m_CurrentNumberOfItems];

		int i, j, k;

		// making permutation table
		int c = 0;

		for (j = 0; j < m_NumberOfSpawnItems.Length; j++) {
			for(i = 0; i < m_NumberOfSpawnItems[j]; i++){
					m_PermutationTable [c++] = j;
			}
		}

		i = m_CurrentNumberOfItems;

		while (i > 1) {
			i--;
			k = m_PermutationTable [i];
			j = UnityEngine.Random.Range (0, m_CurrentNumberOfItems);
			m_PermutationTable [i] = m_PermutationTable [j];
			m_PermutationTable [j] = k;
		}

		/*
		for(int n = 0; n < m_CurrentNumberOfItems; n++){
			Debug.Log (m_PermutationTable [n]);
		}
		*/
	}


	void Update () {
		if (m_NextSpawnTime < Time.time) {
			if (m_CurrentNumberOfItems > 0) {

				Instantiate (m_Items [m_PermutationTable [--m_CurrentNumberOfItems]], transform.position, Quaternion.identity);
				m_NextSpawnTime = Time.time + m_SpawnRate;
				//m_CurrentNumberOfItems--;

			}
		}
	}
}

