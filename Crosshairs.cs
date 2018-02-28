﻿using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour {


	public LayerMask targetMask;
	public Color doHighLightColour;
	public SpriteRenderer dot;
	Color orginalDotColor;


	void Start() {
		Cursor.visible = false;

		orginalDotColor = dot.color;
	}

	// Update is called once per frame
	void Update () {
	
		transform.Rotate( Vector3.forward * -40 * Time.deltaTime);


	}

	public void DetectTargets(Ray ray) {

		if(Physics.Raycast(ray, 100 , targetMask)) {
			dot.color = doHighLightColour;

		} else {

			dot.color = orginalDotColor;
		}

	}

}
