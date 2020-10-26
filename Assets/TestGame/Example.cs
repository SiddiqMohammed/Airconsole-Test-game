using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Example : MonoBehaviour {

	public Rigidbody2D rightB;
	public Rigidbody2D leftB;
	// public Rigidbody2D ball;
	// public float ballSpeed = 10f;

	public Text uiText;
#if !DISABLE_AIRCONSOLE 
	private int scoreRacketLeft = 0;
	private int scoreRacketRight = 0;

	void Awake () {
		AirConsole.instance.onMessage += OnMessage;
		AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onDisconnect += OnDisconnect;
	}

	/// <summary>
	/// We start the game if 2 players are connected and the game is not already running (activePlayers == null).
	/// 
	/// NOTE: We store the controller device_ids of the active players. We do not hardcode player device_ids 1 and 2,
	///       because the two controllers that are connected can have other device_ids e.g. 3 and 7.
	///       For more information read: http://developers.airconsole.com/#/guides/device_ids_and_states
	/// 
	/// </summary>
	/// <param name="device_id">The device_id that connected</param>
	void OnConnect (int device_id) {
		if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0) {
			if (AirConsole.instance.GetControllerDeviceIds ().Count >= 2) {
				StartGame ();
			} else {
				uiText.text = "NEED MORE PLAYERS";
			}
		}
	}

	/// <summary>
	/// If the game is running and one of the active players leaves, we reset the game.
	/// </summary>
	/// <param name="device_id">The device_id that has left.</param>
	void OnDisconnect (int device_id) {
		int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber (device_id);
		if (active_player != -1) {
			if (AirConsole.instance.GetControllerDeviceIds ().Count >= 2) {
				StartGame ();
			} else {
				AirConsole.instance.SetActivePlayers (0);
				// ResetBall (false);
				uiText.text = "PLAYER LEFT - NEED MORE PLAYERS";
			}
		}
	}

	/// <summary>
	/// We check which one of the active players has moved the paddle.
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="data">Data.</param>
	void OnMessage (int device_id, JToken data) {
		int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber (device_id);
		if (active_player != -1) {
			if (active_player == 0) {
				if (data["move"] != null) {
					this.leftB.velocity = Vector3.up * (float)data ["move"];
				}
				if (data["moveRight"] != null) {
					this.leftB.velocity = Vector3.right * (float)data ["moveRight"];
				}
				if (data["moveLeft"] != null) {
					this.leftB.velocity = Vector3.left * (float)data ["moveLeft"];
				}
			}
			if (active_player == 1) {
				if (data["move"] != null) {
					this.rightB.velocity = Vector3.up * (float)data ["move"];
				}
				if (data["moveRight"] != null) {
					this.rightB.velocity = Vector3.right * (float)data ["moveRight"];
				}
				if (data["moveLeft"] != null) {
					this.rightB.velocity = Vector3.left * (float)data ["moveLeft"];
				}
				// this.rightB.velocity = Vector3.up * (float)data ["move"];
				// this.rightB.velocity = Vector3.right * (float)data ["moveRight"];
				// this.rightB.velocity = Vector3.left * (float)data ["moveLeft"];
			}
		}
	}

	void StartGame () {
		AirConsole.instance.SetActivePlayers (2);
		// ResetBall (true);
		scoreRacketLeft = 0;
		scoreRacketRight = 0;
		// UpdateScoreUI ();
	}

	// void ResetBall (bool move) {
		
	// 	// place ball at center
	// 	this.ball.position = Vector3.zero;
		
	// 	// push the ball in a random direction
	// 	if (move) {
	// 		Vector3 startDir = new Vector3 (Random.Range (-1, 1f), Random.Range (-0.1f, 0.1f), 0);
	// 		this.ball.velocity = startDir.normalized * this.ballSpeed;
	// 	} else {
	// 		this.ball.velocity = Vector3.zero;
	// 	}
	// }

	void UpdateScoreUI () {
		// update text canvas
		uiText.text = scoreRacketLeft + ":" + scoreRacketRight;
	}

	void FixedUpdate () {

		// check if ball reached one of the ends
		// if (this.ball.position.x < -9f) {
		// 	scoreRacketRight++;
		// 	UpdateScoreUI ();
		// 	ResetBall (true);
		// }

		// if (this.ball.position.x > 9f) {
		// 	scoreRacketLeft++;
		// 	UpdateScoreUI ();
		// 	ResetBall (true);
		// }
		scoreRacketLeft = ballBehaviour.hitRegister;
		scoreRacketLeft = ballBehaviour.hitRegister;
		// if (scoreRacketLeft)
	}

	void OnDestroy () {

		// unregister airconsole events on scene change
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;
		}
	}

	// void OnCollisionEnter2D(Collision2D other) {
	// 	if (other.gameObject.tag == "hitReg")
	// 	{
	// 		// Destroy(gameObject);
	// 		print("hit");
	// 	}
	// 	// else {
	// 	// 	print("yeet");
	// 	// }
	// }
	

#endif
}
