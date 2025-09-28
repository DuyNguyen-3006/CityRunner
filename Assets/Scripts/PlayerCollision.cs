using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
    }
}
