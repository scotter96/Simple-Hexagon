using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text versionText, highScoreText;

    public LocalStorage localStorage;

    bool showInfo = false;

    public GameObject instructionsObj;

    public GameObject[] helperUI;

    void Start()
    {
        // Debug.Log(Application.persistentDataPath);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        showInfo = false;
        versionText.text = $"v{Application.version} by scotter96";
        if (localStorage.highScore > 0)
            highScoreText.text = $"Your best score:\n{localStorage.highScore}";
    }

    public void FirstRun()
    {
        if (!localStorage.firstRun)
        {
            foreach (GameObject ui in helperUI)
            {
                Destroy(ui);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Interact("Start");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact("Info");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Interact("Exit");
        }
    }

    public void Interact(string name)
    {
        if (name == "Start")
            SceneManager.LoadScene("Game");
        else if (name == "Info")
        {
            showInfo = !showInfo;
            if (!showInfo)
			{
				instructionsObj.SetActive(false);
			}
			else
			{
				instructionsObj.SetActive(true);
			}
        }
        else if (name == "Exit")
            Application.Quit();

        if (localStorage.firstRun)
        {
            localStorage.FirstRunSave();
            FirstRun();
        }
    }
}
