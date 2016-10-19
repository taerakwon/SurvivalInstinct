/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 19, 2016
Short Revision: This class is for player controll
History: 
Oct-19: Changed Timescale
Oct-19: Added Jump, isGrounded check
Oct-15: Created scripts for player
*/

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PRIVATE VARIABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private float _moveKey;
	private bool _isFacingRight;
	private bool _isGrounded; // Checks if player is grounded
	private float _jump;

	// PUBLIC VARIABLES
	public float PlayerSpeed = 25f;
	public float JumpForce = 350f;

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		this._initialise ();	
	}
	
	// Update is called once per frame (For Physics)
	void FixedUpdate () {		
		// If Player is Grounded, then allow
		if (this._isGrounded) 
		{		
			// Check if input is present for movement
			this._moveKey = Input.GetAxis ("Horizontal");
			// If you are pressing d or right arrow
			if (this._moveKey > 0f) {
				this._move = 1f;
				this._isFacingRight = true; // Faces right when moves to right
				this._flip ();
			}
			else if (this._moveKey < 0f) { // If you are pressing a or left arrow
				this._move = -1f;
				this._isFacingRight = false; // Faces left when moves to left
				this._flip ();
			} 
			else { // If you are not presssing anything
				this._move = 0f;
				// Instantly Stops
				this._rigidbody.velocity = Vector2.zero;
			}

			// check if input is present for jumping
			if (Input.GetKeyDown (KeyCode.Space)) {
				this._jump = 1f;
			}

			// Move
			this._rigidbody.AddForce (new Vector2 (this._move * this.PlayerSpeed, this._jump * this.JumpForce), ForceMode2D.Force); // Impulse = One shot, Force = Continuously

		} 
		else {
			this._move = 0f;
			this._jump = 0f;
		}// end of if grounded

		// Camera follows only if x > -8.22f
		if (this._transform.position.x >= -5.2f) {
			// Camera follows player (transform position)
			this.mainCamera.transform.position = new Vector3(this._transform.position.x * 0.8f, this._transform.position.y * 0.8f, -10f); // Camera moves at 80% per frame
		}
	}
	// PUBLIC METHOD


	// PRIVATE METHOD
	// Initialises character variables
	private void _initialise()
	{
		this._jump = 0f;
		this._transform = GetComponent<Transform> (); // Gets Transform component of player
		this._rigidbody = GetComponent<Rigidbody2D> (); // Gets Rigidbody2D component of player
		this._move = 0f;
		this._isFacingRight = true;
		this._isGrounded = false;

	}

	// Flip Method
	private void _flip(){
		if (this._isFacingRight) {
			this._transform.localScale = new Vector2 (1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (-1f, 1f);
		}
	}

	// When player is colliding
	private void OnCollisionEnter2D(Collision2D other){		
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}

	// When player is not colliding with anything
	private void OnCollisionExit2D(Collision2D other){		
		this._isGrounded = false;
		if (other.gameObject.CompareTag ("Border")) {
			this._isGrounded = true;
		}
			
	}

}
