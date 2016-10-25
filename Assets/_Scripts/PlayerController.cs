/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 21, 2016
Short Revision: This class is for player control
History: 
Oct-25: Added bulletin board
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
	private bool _isBulletinVisible; // If Player is on BulletinBoard

	private Animator _animator;
	private GameObject _mainCamera;
	private GameObject _SpawnPoint;
	private GameObject _gameControllerObject;
	private GameController _gameController;

	// PUBLIC VARIABLES
	public float PlayerSpeed = 25f;
	public float JumpForce = 350f;
	public int PlayerLife = 3;


	[Header("Sounds")]
	public AudioSource JumpSound;
	public AudioSource ManScream;
	public AudioSource PickupSound;
	public AudioSource OuchSound;

	// Use this for initialization
	void Start () {
		this._initialise ();
	}

	// Update function
	void Update()
	{
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
				this._animator.SetInteger("PlayerState", 1);
				this._move = 1f;
				this._isFacingRight = true; // Faces right when moves to right
				this._flip ();
			}
			else if (this._move < 0f) { // If you are pressing a or left arrow		
				this._animator.SetInteger("PlayerState", 1);
				this._move = -1f;
				this._isFacingRight = false; // Faces left when moves to left
				this._flip ();
			} 
			else { // If you are not presssing anything
				this._animator.SetInteger("PlayerState", 0);
				this._move = 0f;
				// Instantly Stops
				this._rigidbody.velocity = Vector2.zero;
			}

			// check if input is present for jumping
			if (Input.GetKeyDown (KeyCode.Space)) {
				this._animator.SetInteger ("PlayerState", 2);
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
		if (this._transform.position.x >= -4.3f) {
			// Camera follows player (transform position)
			this._mainCamera.transform.position = new Vector3(this._transform.position.x, this._transform.position.y + 1f, -10f); // Camera moves at 80% per frame
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
		this._animator = GetComponent<Animator>(); // Gets Animator of component
		this._mainCamera = GameObject.FindWithTag("MainCamera");
		this._SpawnPoint = GameObject.FindWithTag("SpawnPoint");
		this._gameControllerObject = GameObject.Find ("Game Controller");
		this._gameController = this._gameControllerObject.GetComponent<GameController> ();
		this._move = 0f;
		this._isFacingRight = true;
		this._isGrounded = false;
		this._isBulletinVisible = false; // Player cannot see Bulletin Board

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
			this._gameController.LivesValue -= 1;
			this._transform.position = this._SpawnPoint.transform.position; //Respawn
		}

		// When you pickup item
		if (other.gameObject.CompareTag ("Item")) {
			this.PickupSound.Play ();
			this._gameController.FoodValue += 1;
			Destroy (other.gameObject);
		}

		if (other.gameObject.CompareTag ("BulletinBoard")) {
			this._gameController.MessageVisibility = true;				
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("BulletinBoard")) {
			this._gameController.MessageVisibility = false;				
		}
	}

	// When play is colliding (instance)
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			this._gameController.LivesValue -= 1;
			this.OuchSound.Play ();
			this._transform.position = this._SpawnPoint.transform.position; // Respawn
		}
	
	}

	// When player is colliding
	private void OnCollisionStay2D(Collision2D other){		
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
