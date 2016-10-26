/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 25, 2016
Short Revision: This class is for Game Controllers (Ex- Scores, behaviours)
History: 
Oct-25: Poison Particles added
		Message (Bulletin Message) Added
Oct-21:
		Created
*/


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	// Private Instances
	private int _livesValue;
	private int _foodValue;
	private bool _messageVisible;

	// Public Instances

	public Text LivesLabel;
	public Text FoodLabel;

	[Header("Message")]
	public GameObject BulletinMessage;
	[Header("Particle")]
	public ParticleSystem PoisonParticle;

	// Use this for initialization
	void Start () {
		this._livesValue = 4;
		this._foodValue = 0;
		this._messageVisible = false;
		this.BulletinMessage.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		this.LivesLabel.text = "LIVES LEFT: " + this._livesValue;
		this.FoodLabel.text = "FOOD COLLECTED: " + this._foodValue;
		if (this._messageVisible) {
			this.BulletinMessage.SetActive (true);
		} else {
			this.BulletinMessage.SetActive (false);			
		}
	}

	// Public Method

	public int LivesValue
	{
		get{
			return this._livesValue;
		}
		set{
			this._livesValue = value;
		}
	}

	public int FoodValue
	{
		get{
			return this._foodValue;
		}
		set{
			this._foodValue = value;
		}
	}

	// Visibility of Message
	public bool MessageVisibility
	{
		get{
			return this._messageVisible;
		}
		set{
			this._messageVisible = value;
		}
	}

	public void PlayPoisonParticle()
	{
		this.PoisonParticle.Play ();
	}
}
