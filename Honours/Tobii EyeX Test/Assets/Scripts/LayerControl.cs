using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControl : MonoBehaviour {

	private int layer;
	public List<GameObject> objects = new List<GameObject>();
	// Use this for initialization
	void Start () {
		layer = 1;
	}
	
	// Update is called once per frame
	private void UpdateLayers () {
		int localLayer;
		switch (layer) {
		case 1:
			localLayer = 1;
			foreach (GameObject obj in objects) {
				obj.GetComponent<SpriteRenderer> ().sortingOrder = localLayer;
				localLayer++;
			}
			break;
		case 2:
			localLayer = 4;
			foreach (GameObject obj in objects) {
				obj.GetComponent<SpriteRenderer> ().sortingOrder = localLayer;
				localLayer++;
			}
			break;
		}
	}

	public int getLayer(){
		return layer;
	}

	public void setLayer(int newLayer){
		layer = newLayer;
		UpdateLayers ();
	}
}
