using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauser : MonoBehaviour
{
	public bool isPaused = false;

	public GameObject pausePanel;

	void Start()
	{
		Time.timeScale = 1;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isPaused)
			{
				Interact("Pause");
			}
			else
			{
				Interact("Resume");
			}
		}

		if (isPaused && Input.GetKeyDown(KeyCode.Return))
		{
			Interact("Quit");
		}

		if (Input.GetMouseButtonDown(1) && !isPaused)
			Interact("Pause");
	}

	public void Interact(string name)
	{
		if (name == "Quit")
			SceneManager.LoadScene("Menu");
		else if (name == "Resume")
		{
			Time.timeScale = 1;
			pausePanel.SetActive(false);
			isPaused = !isPaused;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (name == "Pause")
		{
			Time.timeScale = 0;
			pausePanel.SetActive(true);
			isPaused = !isPaused;

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}