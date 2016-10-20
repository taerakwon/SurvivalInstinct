/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 19, 2016
Short Revision: This class is for enemy control
History: 

Oct-19: Enemy speed changes when sees player
		Added Private, Public Variables: Enemies and move
		Created
*/

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody; // Can use it to initialise movement
	private bool _isGrounded;
	private bool _isGroundAhead;
	private GameObject _Player;
	private bool _isPlayerFound;

	// PUBLIC INSTANCE VARIABLES
	public Transform SightStart;
	public Transform SightEnd;
	public Transform LineOfSight;
	public float Speed = 2f;

	// Use this for initialization
	void Start () {
		// make a reference to this object's Transform and Rigidbody2D components
		this._rigidbody = GetComponent<Rigidbody2D>();
		this._transform = GetComponent<Transform> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
		this._isPlayerFound = false;
	}
	
	// Update is called once per frame
	// When it comes to physics, it is better to use FixedUpdate.  Scrolling background, etc will be in Update()
	void FixedUpdate () {
		// Check if the object is grounded

		// move in the direction of his local scale
		this._rigidbody.velocity = new Vector2(this._transform.localScale.x, 0) * this.Speed;

		// This will be true if touches solid object
		this._isGroundAhead = Physics2D.Linecast(this.SightStart.position, this.SightEnd.position, 1 << LayerMask.NameToLayer("Platform")); // Draw a line
		this._isPlayerFound = Physics2D.Linecast(this.SightStart.position, this.LineOfSight.position, 1 << LayerMask.NameToLayer("Player")); // Draw a line

		if (this._isGroundAhead == false) {
			this._flip ();
		}

		// If player is detected in sight
		if (this._isPlayerFound) {
			// Increase speed
			this.Speed = 4f;
		}

	}


	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true; // Enemy is grounded if on Platform
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = false; // Enemy is not grounded if not on Platform
		}
	}

	private void _flip()
	{
		if (this._transform.localScale.x == 1) {
			this._transform.localScale = new Vector2 (-1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (1f, 1f);
		}
	}
}
