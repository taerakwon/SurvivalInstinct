/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 19, 2016
Short Revision: This class is for player controll
History: 

Oct-19: Added respawn feature
		Added DeathPlane Interaction
		Changed Timescale
		Added Jump, isGrounded check
Oct-15: Created scripts for player
*/

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PRIVATE VARIABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private bool _isFacingRight;
	private bool _isGrounded; // Checks if player is grounded
	private float _jump;
	private float _respawnTime = 7f;
	private bool _isDead;


	// PUBLIC VARIABLES
	public float PlayerSpeed = 25f;
	public float JumpForce = 350f;
	public int PlayerLife = 3;
	public Camera mainCamera;
	public Transform SpawnPoint;

	[Header("Sounds")]
	public AudioSource JumpSound;
	public AudioSource ManScream;

	// Use this for initialization
	void Start () {
		this._initialise ();
	}

	// Update function
	void Update()
	{
		if (this._isDead && this.PlayerLife > 0) {
			this._respawnTime -= 0.1f;
		}
		this._respawn ();
	}
	
	// Update is called once per frame (For Physics)
	void FixedUpdate () {
			
		// If Player is Grounded, then allow
		if (this._isGrounded) 
		{		
			// Check if input is present for movement
			this._move = Input.GetAxis ("Horizontal");

			// If you are pressing d or right arrow
			if (this._move > 0f) {
				this._move = 1f;
				this._isFacingRight = true; // Faces right when moves to right
				this._flip ();
			}
			else if (this._move < 0f) { // If you are pressing a or left arrow		
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
				this.JumpSound.Play ();
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
		if (this._transform.position.x >= -5.2f && this._isDead == false) {
			// Camera follows player (transform position)
			this.mainCamera.transform.position = new Vector3(this._transform.position.x * 0.8f, this._transform.position.y * 0.8f, -10f); // Camera moves at 80% per frame
		}
	}
	// PUBLIC METHOD


	// PRIVATE METHOD
	// Initialises character variables
	private void _initialise()
	{
		this._isDead = false;
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("DeathPlane")) {
			// transport player's position to spawn point
			this.ManScream.Play ();
			this.PlayerLife -= 1;
			this._isDead = true;
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

	// Respawn when _respawnTime < 0
	private void _respawn()
	{		
		if (this._respawnTime < 0f) {
			this._transform.position = this.SpawnPoint.position;
			this._respawnTime = 5f;
			this._isDead = false;
		}
	}
}
