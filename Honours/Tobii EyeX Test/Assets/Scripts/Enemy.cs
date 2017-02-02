using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private bool isAlive;

	// Use this for initialization
	void Start () {
		isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool getAliveState(){
		return isAlive;
	}

	public void setAliveState(bool state){
		isAlive = state;
	}
}
