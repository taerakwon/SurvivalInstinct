using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private bool _isGrounded;
	private bool _isGroundAhead;

	private GameObject _Player;

	// PUBLIC INSTANCE VARIABLES
	public Transform SightStart;
	public Transform SightEnd;

	// Use this for initialization
	void Start () {
		// make a reference to this object's Transform and Rigidbody2D components
		this._rigidbody = GetComponent<Rigidbody2D>();
		this._transform = GetComponent<Transform> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
	}
	
	// Update is called once per frame
	// When it comes to physics, it is better to use FixedUpdate.  Scrolling background, etc will be in Update()
	void FixedUpdate () {
		// Check if the object is grounded
		// This will be true if touches solid object
		this._isGroundAhead = Physics2D.Linecast(this.SightStart.position, this.SightEnd.position, 1 << LayerMask.NameToLayer("Solid"));

		if (this._isGroundAhead == false) {
			this._flip ();
		}

	}


	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = false;
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
