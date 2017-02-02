using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject spawnPoint { get; set; }
	public bool isAvailable { get; set; }
	private GameObject enemy;

	public Spawn(GameObject spawnPoint, GameObject enemy){
		this.spawnPoint = spawnPoint;
		this.isAvailable = true;
		this.enemy = enemy;
	}

	public bool isEnemyAlive(){
		return enemy.GetComponentInChildren<Enemy> ().getAliveState ();
	}

	public GameObject getEnemy(){
		return enemy;
	}

	public void setEnemy(GameObject enemy){
		this.enemy = enemy;
		int spawnLayer = spawnPoint.GetComponent<SpawnPoint> ().getLayer ();
		int enemyLayer = enemy.GetComponent<LayerControl> ().getLayer ();
		if (enemyLayer != spawnLayer) {
			enemy.GetComponent<LayerControl> ().setLayer (spawnLayer);
		}
	}

	public void destroyEnemy(){
		Destroy (this.enemy);
	}
}
