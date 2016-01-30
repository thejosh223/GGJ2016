using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public Transform endPoint;

	public void SpawnEnemy(float duration) {
		GameObject g = Instantiate(enemyPrefab);
		g.transform.SetParent(transform);
		g.transform.position = spawnPoint.position;

		Enemy e = g.GetComponent<Enemy>();
		e.absoluteSpeed = Vector3.Distance(spawnPoint.position, endPoint.position) / duration;
		e.targetX = endPoint.position.x;
	}

}
