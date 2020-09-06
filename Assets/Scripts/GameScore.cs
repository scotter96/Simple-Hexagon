using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public int score = 0;

    public Text scoreText;

    public void AddScore(int amount)
    {
        score += amount;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = "" + score;
    }
}
