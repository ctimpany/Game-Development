using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	public int layer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setLayer(int newLayer){
		layer = newLayer;
	}

	public int getLayer(){
		return layer;
	}
}
