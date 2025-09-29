using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f;
    public float moveSpeed = 2f;
    public float destroyX = -10f;

    public float laneY1 = -2f;
    public float laneY2 = -3.5f;

    public float spawnX = 10f;

    public float speedIncreaseStep = 1f;
    public float increaseInterval = 8f;
    public float maxSpeed = 12f;

    private float timer = 0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            // tăng tốc độ
            moveSpeed += speedIncreaseStep;
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

            // giảm thời gian spawn
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f); // ko nhỏ hơn 0.5s
            CancelInvoke(nameof(SpawnObstacle));
            InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);

            timer = 0f;

            Debug.Log($"[ObstacleController] Speed: {moveSpeed}, SpawnInterval: {spawnInterval}");
        }
    }

    void SpawnObstacle()
    {
        int randIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[randIndex];

        float randY = Random.value > 0.5f ? laneY1 : laneY2;
        Vector3 spawnPos = new Vector3(spawnX, randY, 0);
        GameObject obstacle = Instantiate(prefab, spawnPos, Quaternion.identity);

        obstacle.AddComponent<ObstacleMove>().Init(this, destroyX);
    }
}
