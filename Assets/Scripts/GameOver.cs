using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public bool triggered = false;

	public Text bestScoreText;
	public Text retryText;

	public GameObject player;
	public GameObject deathParticlePrefab;

	public LocalStorage localStorage;
	public GameScore gameScore;
	public CameraEffect cameraEffect;
	public PowerManager powerManager;

	public void Trigger ()
	{
		triggered = true;
		powerManager.StopPowerup();
		int highScore = localStorage.highScore;
		int score = gameScore.score;

		if (score < highScore)
			bestScoreText.text = $"Your best score is {highScore}, by the way..";
		else if (score == highScore)
		{
			if (score != 0)
				bestScoreText.text = $"Your best score is also {highScore} ! How appropriate.";
			else
				bestScoreText.text = "Really ??";
		}
		else
		{
			localStorage.GoSave();
			bestScoreText.text = "Congrats ! This is your new best score !";
		}
		bestScoreText.gameObject.SetActive(true);
		retryText.gameObject.SetActive(true);

		cameraEffect.StartDeathShake();
		GameObject deathParticle = Instantiate (deathParticlePrefab, player.transform.position, Quaternion.identity) as GameObject;
		Destroy (player);
		Destroy (deathParticle, 2f);
		
		GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerups");
		foreach (GameObject powerup in powerups)
			Destroy (powerup);
	}

	void Update ()
	{
		if (triggered && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}