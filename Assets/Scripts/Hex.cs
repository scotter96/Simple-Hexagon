using UnityEngine;

public class Hex : MonoBehaviour
{
    Rigidbody2D rb;

    GameScore gameScore;
    GameOver gameOver;

    public float shrinkSpeed = 3f; // ? Default shrink speed for normal hexagon

    void Start()
    {
        gameScore = GameObject.Find("Game Manager").GetComponent<GameScore>();
        gameOver = GameObject.Find("Game Manager").GetComponent<GameOver>();
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = Random.Range(0f, 360f);
        transform.localScale = Vector3.one * 10f;
    }

    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= .05f)
        {
            Destroy (gameObject);
            if (!gameOver.triggered)
                gameScore.AddScore(1);
        }
    }
}
