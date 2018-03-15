using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayerController : NetworkBehaviour {

	[SyncVar(hook="OnColor")]
	public Color myColor;

	public AudioSource audio;

	[SyncVar]
	public Boolean tagged;

	public int ConfigDelay;
	int movementDelay;
	int collisionDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer)
			return;

		if (collisionDelay > 0)
			collisionDelay--;
		if (movementDelay > 0) {
			movementDelay--;
			return;
		}
		float x = Input.GetAxis("Horizontal") * 0.1f;
		float y = Input.GetAxis ("Vertical") * 0.1f;
		transform.Translate (x, y, 0);

		if (transform.position.x > 20) {
			transform.Translate (-40, 0, 0);
		} else if (transform.position.x < -20) {
			transform.Translate (40, 0, 0);
		}
		if (transform.position.y > 10) {
			transform.Translate (0, -20, 0);
		} else if (transform.position.y < -10) {
			transform.Translate (0, 20, 0);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			CmdTag ();
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		print ("Collision");
		if (collision.gameObject.CompareTag("Player") && collisionDelay <= 0) {
			print ("Trigger");
			audio.Play ();
			if (!tagged)
				movementDelay = ConfigDelay;
			collisionDelay = ConfigDelay;
			CmdTag ();
		}
	}

	[Command]
	public void CmdTag () {
		print ("Changed Color");
		if (tagged) {
			tagged = false;
			OnColor (Color.green);
		} else {
			tagged = true;
			OnColor (Color.red);
		}
	}

	void OnColor(Color newColor)
	{
		GetComponent<Renderer>().material.color = newColor;
		myColor = newColor;
	}

	public override void OnStartClient()
	{
		GetComponent<Renderer>().material.color = myColor;
	}
}
