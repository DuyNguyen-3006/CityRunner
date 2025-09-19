using UnityEngine;

public class ScrollingRoad : MonoBehaviour
{
    public GameObject[] roads;   // chứa các sprite con đường
    public float speed = 5f;     // tốc độ chạy

    private float roadWidth;     // chiều rộng 1 sprite đường

    void Start()
    {
        // Tính chiều rộng sprite dựa trên SpriteRenderer
        SpriteRenderer sr = roads[0].GetComponent<SpriteRenderer>();
        roadWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // Cho tất cả đường chạy sang trái
        foreach (GameObject road in roads)
        {
            road.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Kiểm tra từng sprite xem có ra khỏi màn hình chưa
        for (int i = 0; i < roads.Length; i++)
        {
            GameObject road = roads[i];

            // nếu đường ra khỏi màn hình bên trái
            if (road.transform.position.x < -roadWidth)
            {
                // tìm sprite có X lớn nhất (sprite cuối cùng bên phải)
                float maxX = GetMaxX();
                // dịch sprite này ra sau sprite cuối cùng
                road.transform.position = new Vector3(maxX + roadWidth, road.transform.position.y, road.transform.position.z);
            }
        }
    }

    float GetMaxX()
    {
        float maxX = roads[0].transform.position.x;
        foreach (GameObject road in roads)
        {
            if (road.transform.position.x > maxX)
                maxX = road.transform.position.x;
        }
        return maxX;
    }
}
