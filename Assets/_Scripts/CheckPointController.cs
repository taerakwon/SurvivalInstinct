/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 19, 2016
Short Revision: This class is for Check Point Flag Control
History: 
Oct-19: Added OnTriggerEnter2D for CheckPoint When Player passes by
		Created
*/

using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {
	// PRIVATE INSTANCE VARIABLE
	private Transform _transform;
	private Animator _animator;
	private bool _flagChecked;

	[SerializeField]
	private AudioSource _flagSound;

	// PUBLIC INSTANCE VARIABLE
	public Transform SpawnPoint;



	// Use this for initialization
	void Start () {
		this._flagChecked = false;
		this._transform = GetComponent<Transform> ();			
		this._animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {	
	}
		
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this._flagChecked == false) { // Only allow once
			
			// When player hits
			if (other.gameObject.CompareTag ("Player")) {
				this._flagChecked = true;
				this._flagSound.Play ();
				this._animator.SetBool ("FlagChecked", true);
				this.SpawnPoint.position = new Vector2 (this._transform.position.x, this._transform.position.y + 1f);
			}
		}
	}
}
