﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehaviour : MonoBehaviour
{
	public Example logic;

	public static int hitRegister;

	// Use this for initialization
	void Start () {
	}

	// private void OnTriggerEnter(Collider other) {
	// 	if (other.tag == "hitReg")
	// 	{
	// 		// Destroy(gameObject);
	// 		Debug.Log("hit");
	// 	}
	// }
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "hitReg")
		{
			// Destroy(gameObject);
			print("hit");
			hitRegister++;
		}
		// else {
		// 	print("yeet");
		// }
	}

	// void OnCollisionEnter2D (Collision2D col) {

	// 	if (col.gameObject.GetComponent<Rigidbody2D> () != null) {

	// 		float hitPos = (col.transform.position.y - transform.position.y) / (GetComponent<Collider2D> ().bounds.size.y / 2);
	// 		float hitDir = 1f;

	// 		if (col.relativeVelocity.x > 0) {
	// 			hitDir = -1f;
	// 		}

	// 		Vector2 dir = new Vector2 (hitDir, hitPos).normalized;
	// 		// col.gameObject.GetComponent<Rigidbody2D> ().velocity = dir * logic.ballSpeed;

	// 	}
	// }
}
