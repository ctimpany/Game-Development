using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Core : MonoBehaviour {

	int top;
	public int maxSpawns;
	int spawnCount = 0;
	//public Spawn[] spawnPoints;
	private List<Spawn> spawnPoints;
	private	UnityEngine.Object[] enemyTypes;
	DateTime timeStart, timeCur;
	//public double timeCooldown;
	private double cooldownTime;
	bool onCooldown;

	// Use this for initialization
	void Start () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Spawn Point");
		enemyTypes = Resources.LoadAll ("MoleHoles");
		//spawnPoints = new Spawn[objs.Length];
		spawnPoints = new List<Spawn>();
		cooldownTime = 0;
		onCooldown = false;
		foreach(GameObject obj in objs){
			Spawn temp = new Spawn (obj, null);//gameObject.AddComponent<Spawn>();
			//temp.spawnPoint = obj;
			//temp.isAvailable = true;
			//temp.enemy = null;
			spawnPoints.Add (temp);
			//vecArray = objs.GetComponent<Transform>().position;
		}
		//top = spawnPoints.Length;
		//InvokeRepeating("checkSpawns",0.01f,10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//if(!(spawnCount == maxSpawns))
			checkSpawns ();
	}

	void checkSpawns(){
		int rand = UnityEngine.Random.Range (0, spawnPoints.Count);
		int i = 0;
		timeCur = System.DateTime.Now;

		if (onCooldown && ((timeCur - timeStart).TotalSeconds >= cooldownTime))
			onCooldown = false;
		
		foreach (Spawn spawn in spawnPoints) {

			if (!(spawn.getEnemy() == null)) {
				//Enemy temp = spawn.enemy.GetComponent<Enemy>();
				if (!(spawn.isEnemyAlive ())) {
					/*if (!onCooldown) {
					onCooldown = true;
					timeStart = System.DateTime.Now;
				}*/
					spawn.destroyEnemy ();

					if (spawnCount == maxSpawns)
						startCooldown ();

					spawnCount--;
					spawn.isAvailable = true;
				}
			}

			if (!(spawnCount == maxSpawns) && spawn.isAvailable && rand == i && !onCooldown) {
				//GameObject enemy =(GameObject)Instantiate (Resources.Load ("Mole"), 
				//											spawn.spawnPoint.GetComponent<Transform> ().position, 
				//											Quaternion.identity);


				int enemyIndex = UnityEngine.Random.Range (0, 10);
				switch (enemyIndex) {
				case 1:
					enemyIndex = 2;
					break;
				case 2:
					enemyIndex = 1;
					break;
				default: 
					enemyIndex = 0;
					break;
				}
				GameObject enemy =(GameObject)Instantiate (enemyTypes[enemyIndex], 
					spawn.spawnPoint.GetComponent<Transform> ().position, 
					Quaternion.identity);
				
				spawnCount++;
				spawn.setEnemy(enemy);
				spawn.isAvailable = false;
				if (spawnCount != maxSpawns) {
					startCooldown ();
				}
			}
			i++;
		}
	}


	private void startCooldown(){
		onCooldown = true;
		cooldownTime = UnityEngine.Random.Range (0, 4);
		timeStart = System.DateTime.Now;
	}
		
}
