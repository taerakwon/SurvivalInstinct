/* 
Game Title: Survival Instrinct
Created By: Taera Kwon (#300755802)
Last Edited By: Taera Kwon
Last Edited Date: Oct 25, 2016
Short Revision: This class is for Start Scene
History: 
Oct-25: Added Instructions
		Play and Instruction Buttons Added
		Created
*/


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour {	
	// PUBLIC INSTANCES
	public Text TitleLabel;
	public Text Instructions;
	public Button PlayButton;
	public Button InstructionButton;
	public Button MainButton;

	// Start
	void Start ()
	{
		// Hide Objects
		this.MainButton.gameObject.SetActive (false);
		this.Instructions.gameObject.SetActive (false);
	}

	public void PlayButton_Click()
	{
		SceneManager.LoadScene ("MainScene");
	}

	public void InstructionButton_Click()
	{
		Debug.Log ("HELLO");
		// Show Objects
		this.Instructions.gameObject.SetActive(true);
		this.MainButton.gameObject.SetActive (true);
		// Hide Objects
		this.TitleLabel.gameObject.SetActive (false);
		this.InstructionButton.gameObject.SetActive (false);
	}

	public void MainButton_Click()
	{
		SceneManager.LoadScene ("StartScene");
	}
}
