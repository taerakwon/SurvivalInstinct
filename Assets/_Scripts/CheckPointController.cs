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

	// PUBLIC INSTANCE VARIABLE
	public Transform SpawnPoint;

	// Use this for initialization
	void Start () {
		this._transform = GetComponent<Transform> ();			
	}
	
	// Update is called once per frame
	void Update () {	
	}
		
	private void OnTriggerEnter2D(Collider2D other)
	{
		// When player hits
		if (other.gameObject.CompareTag ("Player")) {
			this.SpawnPoint.position = new Vector2 (this._transform.position.x, this._transform.position.y + 1f);
		}
	}
}
