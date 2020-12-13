using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
	public static GameObject player;
	public GameObject startMenu;
	public GameObject gameOverMenu;
	public UnityEngine.UI.Button startButton;
	public UnityEngine.UI.Button quitButton;
	public UnityEngine.UI.Text scoreLabel;
	public UnityEngine.UI.Text healthLabel;
	public UnityEngine.UI.Text finalScoreLabel;
	static AudioSource scoreSound;
	public int gameOverDelay;
	public float firstAsteroidLaunchTime;


	static int score = 0;
	static int health = 100;
	public static bool isStarted = false;
	public static bool isStoped = false;
	public GameObject playerExplosion;
	public static GameObject playerExplosionEffect;
	public GameObject playerExplosion2;
	public static GameObject playerExplosionEffect2;

	void Start()
	{
		gameOverMenu.SetActive(false);
		startButton.onClick.AddListener(delegate {
			EmitterScript.nextLaunchTime = Time.time + firstAsteroidLaunchTime;
			startMenu.SetActive(false);
			isStarted = true;
		});
		quitButton.onClick.AddListener(delegate {
			Application.Quit();
		});
		scoreSound = GetComponent<AudioSource>();

		playerExplosionEffect = playerExplosion;
		playerExplosionEffect2 = playerExplosion2;
	}

	void Update()
	{
		if (PlayerScript.isGameOver)
        {
			finalScoreLabel.text = "Your score: " + score;
			StartCoroutine(gameOver());
		}

		scoreLabel.text = "Score: " + score;
		healthLabel.text = "HP: " + health;
	}

	public static void increaseScore(int increment)
	{
		score += increment;
		if (score % 100 == 0 && score > 0)
		{
			scoreSound.Play();
		}
	}
	public static void decreaseHealth(float decrement)
	{
		if (health > 0)
		{
			health -= (int)decrement + 1;
			if (health < 0)
			{
				health = 0;
				isStoped = true;
			}
			/*Debug.Log("HP: " + health);
			Debug.Log(decrement);*/
		}
	}

	/*public static void showGameOver()
	{
		StartCoroutine(gameOver());
	}
	*/
	IEnumerator gameOver()
	{
		yield return new WaitForSeconds(gameOverDelay);

		gameOverMenu.SetActive(true);
	}
}
