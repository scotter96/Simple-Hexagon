using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public float spawnRate = 1f;

    public GameObject hexPrefab;

    public GameOver gameOver;

    float nextTimeToSpawn = 0f;

    public bool spawnPowers = true;

    [System.Serializable]
    public class Powerups {
        public GameObject slowTime;
        public GameObject invincibility;
    }
    public Powerups powerups;

    public Transform[] powerSpawnPoints;

    void Update()
    {
        if (Time.time >= nextTimeToSpawn && !gameOver.triggered)
        {
            GameObject hex = Instantiate (hexPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            nextTimeToSpawn = Time.time + 1f / spawnRate;

            // ? Power Spawner
            if (spawnPowers)
            {
                float odd = Random.Range (0,6f);
                if (odd >= 5f)
                {
                    if (odd < 5.5f)
                        SpawnPower(powerups.slowTime);
                    else if (odd >= 5.5f)
                        SpawnPower(powerups.invincibility);
                }
            }   
        }
    }

    void SpawnPower(GameObject obj)
    {
        int point = Random.Range (0, powerSpawnPoints.Length);
        Instantiate (obj, powerSpawnPoints[point].position, Quaternion.identity);
    }
}