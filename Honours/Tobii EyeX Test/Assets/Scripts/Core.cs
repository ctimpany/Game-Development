using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Core : MonoBehaviour {

	int top;
	public int maxSpawns;
	int spawnCount = 0;
	//public Spawn[] spawnPoints;
	public List<Spawn> spawnPoints;
	DateTime timeStart, timeCur;
	public double timeCooldown;
	bool onCooldown;
	// Use this for initialization
	void Start () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Spawn Point");
		//spawnPoints = new Spawn[objs.Length];
		spawnPoints = new List<Spawn>();
		onCooldown = false;
		foreach(GameObject obj in objs){
			Spawn temp = gameObject.AddComponent<Spawn>();
			temp.spawnPoint = obj;
			temp.isAvailable = true;
			temp.enemy = null;
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
		if (onCooldown && ((timeCur - timeStart).TotalSeconds >= timeCooldown))
			onCooldown = false;
		foreach (Spawn spawn in spawnPoints) {

			if (!(spawn.enemy == null)) {
				Enemy temp = spawn.enemy.GetComponent<Enemy>();
				if (!(temp.isAlive)) {
					if (!onCooldown) {
						onCooldown = true;
						timeStart = System.DateTime.Now;
					}
					Destroy (spawn.enemy);
					spawnCount--;
					spawn.isAvailable = true;
				}
			}

			if (!(spawnCount == maxSpawns) && spawn.isAvailable && rand == i && !onCooldown) {
				GameObject enemy =
					(GameObject) Instantiate (/*GameObject.Find ("Alien")*/ Resources.Load("Alien"), spawn.spawnPoint.GetComponent<Transform> ().position, Quaternion.identity);
				spawnCount++;
				spawn.enemy = enemy;
				spawn.isAvailable = false;
			}
			i++;
		}
	}
		
}
