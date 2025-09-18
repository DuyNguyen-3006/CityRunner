using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // danh sách vật cản (cọc tiêu, bịch rác...)
    public float spawnInterval = 2f;      // thời gian giữa mỗi lần spawn
    public float moveSpeed = 2f;          // tốc độ chạy sang trái
    public float destroyX = -10f;         // khi ra ngoài màn hình thì xóa

    // vị trí 2 lane (set trong Inspector hoặc code sẵn)
    public float laneY1 = -2f;
    public float laneY2 = -3.5f;

    public float spawnX = 10f;  // tọa độ X bên phải màn hình

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // chọn prefab ngẫu nhiên
        int randIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[randIndex];

        // chọn lane ngẫu nhiên
        float randY = Random.value > 0.5f ? laneY1 : laneY2;

        // tạo object
        Vector3 spawnPos = new Vector3(spawnX, randY, 0);
        GameObject obstacle = Instantiate(prefab, spawnPos, Quaternion.identity);

        // gắn script move
        obstacle.AddComponent<ObstacleMove>().Init(moveSpeed, destroyX);
    }
}
