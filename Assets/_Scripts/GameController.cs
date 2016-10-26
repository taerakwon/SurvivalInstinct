/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 25, 2016
Short Revision: This class is for Game Controllers (Ex- Scores, behaviours)
History: 
Oct-25: End Game Scenario, Restart Option
		Poison Particles added
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
	private GameObject _player;

	// Public Instances
	public Text LivesLabel;
	public Text FoodLabel;

	[Header("Message")]
	public GameObject BulletinMessage;
	[Header("Particle")]
	public ParticleSystem PoisonParticle;

	[Header("GameOver")]
	public Text GameOverLabel;
	public Text TotalCollected;
	public Button ReplayButton;


	//public RawImage LostCanvas;

	// Use this for initialization
	void Start () {		
		this._player = GameObject.Find ("Player"); // Player Object
		// Default variable values
		this._livesValue = 4; 
		this._foodValue = 0;
		this._messageVisible = false;
		// Hide Objects
		this.GameOverLabel.gameObject.SetActive (false);
		this.ReplayButton.gameObject.SetActive (false);
		this.TotalCollected.gameObject.SetActive (false);
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
		if (this._livesValue <= 0) {
			this._gameOver (false); // Did not survive
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

	// Replay
	public void ReplayGame()
	{
		SceneManager.LoadScene ("MainScene");		
	}

	// Plays Particle When Poison is triggered
	public void PlayPoisonParticle()
	{
		this.PoisonParticle.Play ();
	}

	// Public Accessor to private _gameOver method
	public void GameOver(bool survived)
	{
		this._gameOver (survived);
	}

	// PRIVATE Method

	// Game Over State
	private void _gameOver(bool survived)
	{
		if (survived) {
			this.GameOverLabel.color = new Color(255f/255f, 255f/255f, 255f/255f);
			this.GameOverLabel.text = "GAME OVER\nCONGRATULATIONS!\nYOU SURVIVED";
		} else {
			this.GameOverLabel.color = new Color(160f/255.0f, 9f/255.0f, 9f/255.0f);
			this.GameOverLabel.text = "GAME OVER\nYOU ARE DEAD";
		}
		this.TotalCollected.text = "TOTAL FOOD COLLECTED: " + this._foodValue;
		// Hide Objects
		this._player.gameObject.SetActive (false);
		this.LivesLabel.gameObject.SetActive (false);
		this.FoodLabel.gameObject.SetActive (false);
		// Show Objects
		this.GameOverLabel.gameObject.SetActive (true);
		this.ReplayButton.gameObject.SetActive (true);
		this.TotalCollected.gameObject.SetActive (true);
	}
}
