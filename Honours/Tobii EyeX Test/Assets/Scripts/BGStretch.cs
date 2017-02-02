﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGStretch : MonoBehaviour {
	SpriteRenderer sr;
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(
			worldScreenWidth / sr.sprite.bounds.size.x,
			worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}

}
