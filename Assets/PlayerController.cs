using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayerController : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer)
			return;

		float x = Input.GetAxis("Horizontal") * 0.1f;
		float y = Input.GetAxis ("Vertical") * 0.1f;
		transform.Translate (x, y, 0);

		if (Math.Abs (transform.position.x) > 20) {
			print ("above or below 10");
			transform.Translate (-2 * transform.position.x, 0, 0);
		}
		if (Math.Abs (transform.position.y) > 10) {
			transform.Translate (0, -2 * transform.position.y, 0);
		}
	}

	public override void OnStartLocalPlayer() {
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}
}
