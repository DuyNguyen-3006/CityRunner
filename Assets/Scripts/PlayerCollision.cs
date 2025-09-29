using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private AudioSource sfxSource;      // AudioSource để phát SFX
    [SerializeField] private AudioClip eatCoinSound;     // âm thanh khi ăn coin
    [SerializeField] private AudioClip hitObstacleSound; // âm thanh khi đụng obstacle

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            // Phát âm thanh ăn coin
            if (sfxSource && eatCoinSound)
                sfxSource.PlayOneShot(eatCoinSound);

            // Xử lý coin
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Phát âm thanh va chạm
            if (sfxSource && hitObstacleSound)
                sfxSource.PlayOneShot(hitObstacleSound);

            // Kết thúc game
            gameManager.GameOver();
        }
    }
}
