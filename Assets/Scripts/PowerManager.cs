using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
	bool powered = false;

	float powerTime = 0f;
	public float powerDuration = 5f;

	public GameObject playerGO, powerIndicator;
	Text powerText;

	public RectTransform powerBar;
	RectTransform powerMeter;

	SpriteRenderer playerRenderer;

	Player player;

	string activePower = null;

	void Start()
	{
		playerRenderer = playerGO.GetComponent<SpriteRenderer>();
		powerText = powerIndicator.GetComponent<Text>();
		powerMeter = powerBar.GetChild(0) as RectTransform;
		player = playerGO.GetComponent<Player>();
	}

	void Update ()
	{
		// ? Powerup debug mode 
		if (Input.GetKeyDown(KeyCode.Alpha1))
			StartPowerup("SlowTime");
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			StartPowerup("Invincibility");

		if (powered)
		{
			if (powerTime > 0)
			{
				powerTime -= Time.deltaTime;
				float powerMeterSizeX = (powerTime/powerDuration) * 100f;
				if (activePower == "SlowTime")
					powerMeterSizeX = (powerTime / (powerDuration / 2)) * 100f;
				float powerMeterPosX = ((powerMeterSizeX/100f) * powerBar.sizeDelta.x) / 2;
				powerMeter.sizeDelta = new Vector2 (powerMeterSizeX, powerMeter.sizeDelta.y);
				powerMeter.anchoredPosition = new Vector2 (powerMeterPosX, powerMeter.anchoredPosition.y);
			}

			if (powerTime <= 0)
			{
				powered = false;
				powerTime = 0;
				StopPowerup();
			}
		}
	}

    public void StartPowerup (string name)
	{
		if (powered)
			StopPowerup();

		powerTime = powerDuration;
		powerIndicator.SetActive(true);
		powerMeter.sizeDelta = new Vector2 (powerBar.sizeDelta.x, powerMeter.sizeDelta.y);
		powerMeter.anchoredPosition = new Vector2 (powerBar.anchoredPosition.x, powerMeter.anchoredPosition.y);

		if (name.Contains ("SlowTime"))
		{
			activePower = "SlowTime";
			Time.timeScale = 0.5f;
			powerTime *= Time.timeScale;
			powerText.text = "Time Slow Down";
		}
		else if (name.Contains ("Invincibility"))
		{
			activePower = "Invincibility";
			playerRenderer.color = new Color (playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 0.7f);
			player.isInvincible = true;
			powerText.text = "Invincibility";
		}
		powered = true;
	}

    public void StopPowerup ()
	{
		activePower = null;
		Time.timeScale = 1f;
		if (powered)
		{
			playerRenderer.color = new Color (playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 1f);
			player.isInvincible = false;
		}

		powerIndicator.SetActive(false);
	}
}