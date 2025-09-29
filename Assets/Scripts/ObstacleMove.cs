using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private ObstacleController controller;
    private float destroyX;

    public void Init(ObstacleController controller, float destroyX)
    {
        this.controller = controller;
        this.destroyX = destroyX;
    }

    void Update()
    {
        float speed = controller.moveSpeed;
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
