/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 14, 2016
Short Revision: This class is for player controll
*/

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PRIVATE VARIABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private bool _isFacingRight;

	// PUBLIC VARIABLES
	public float PlayerSpeed = 35f;

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		this._initialise ();	
	}
	
	// Update is called once per frame (For Physics)
	void FixedUpdate () {
		// Check if input is present for movement
		this._move = Input.GetAxis("Horizontal");

		if (this._move > 0f) {
			this._move = 1;
			this._isFacingRight = true; // Faces right when moves to right
			this._flip ();
		} else if (this._move < 0f) {
			this._move = -1;
			this._isFacingRight = false; // Faces left when moves to left
			this._flip ();
		} else {
			this._move = 0;
		}
		// Move
		this._rigidbody.AddForce (new Vector2 (this._move * this.PlayerSpeed, 0f), ForceMode2D.Force);

		// Camera follows only if x > -8.22f
		if (this._transform.position.x >= -8.22f) {
			// Camera follows player (transform position)
			this.mainCamera.transform.position = new Vector3(this._transform.position.x * 0.8f, this._transform.position.y * 0.8f, -10f); // Camera moves at 80%
		}


	}
	// PUBLIC METHOD


	// PRIVATE METHOD
	// Initialises character variables
	private void _initialise()
	{
		this._transform = GetComponent<Transform> (); // Gets Transform component of player
		this._rigidbody = GetComponent<Rigidbody2D> (); // Gets Rigidbody2D component of player
		this._move = 0f;
		this._isFacingRight = true;
	}

	// Flip Method
	private void _flip(){
		if (this._isFacingRight) {
			this._transform.localScale = new Vector2 (1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (-1f, 1f);
		}
	}

}
