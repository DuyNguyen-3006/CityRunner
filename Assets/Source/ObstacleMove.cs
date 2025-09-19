using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private float speed;
    private float destroyX;

    public void Init(float moveSpeed, float destroyPosX)
    {
        speed = moveSpeed;
        destroyX = destroyPosX;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
