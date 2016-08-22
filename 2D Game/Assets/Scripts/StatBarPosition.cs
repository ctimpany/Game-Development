using UnityEngine;
using System.Collections;

public class StatBarPosition : MonoBehaviour {

	void Start (){
		Vector2 topLeft = new Vector2(0, 0);
		transform.position = Camera.main.ScreenToWorldPoint(topLeft);
	}
}
