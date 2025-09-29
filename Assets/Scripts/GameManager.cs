//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    private int score = 0;
//    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private GameObject gameOverUi;
//    private bool isGameOver = false;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        UpdateScore();
//        gameOverUi.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public void AddScore(int points)
//    {
//        if (!isGameOver)
//        {
//            score += points;
//            UpdateScore();
//        }
//    }
//    private void UpdateScore() 
//    {
//        scoreText.text = score.ToString();
//    }
//    public void GameOver()
//    {
//        isGameOver = true;
//        score = 0;
//        Time.timeScale = 0;
//        gameOverUi.SetActive(true);
//    }
//    public void RestartGame()
//    {
//        isGameOver = false;
//        score = 0;
//        UpdateScore();
//        Time.timeScale = 1;
//        SceneManager.LoadScene("Game");
//    }
//    public bool IsGameOver()
//    {
//        return isGameOver;
//    }
//}
//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    private int score = 0;
//    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private GameObject gameOverUi;
//    private bool isGameOver = false;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        UpdateScore();
//        gameOverUi.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public void AddScore(int points)
//    {
//        if (!isGameOver)
//        {
//            score += points;
//            UpdateScore();
//        }
//    }
//    private void UpdateScore() 
//    {
//        scoreText.text = score.ToString();
//    }
//    public void GameOver()
//    {
//        isGameOver = true;
//        score = 0;
//        Time.timeScale = 0;
//        gameOverUi.SetActive(true);
//    }
//    public void RestartGame()
//    {
//        isGameOver=false;
//        score = 0;
//        UpdateScore();
//        Time.timeScale= 1;
//        SceneManager.LoadScene("Game");
//    }
//    public bool IsGameOver()
//    {
//        return isGameOver;
//    }
//}
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;

    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject winUi;

    private bool isGameOver = false;
    private bool isWin = false;
    private bool isInvincible = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (gameOverUi) gameOverUi.SetActive(false);
        if (winUi) winUi.SetActive(false);
        UpdateScore();
    }

    public void AddScore(int points)
    {
        if (isGameOver || isWin) return;
        score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText == null) { Debug.LogWarning("GameManager: scoreText is not assigned!"); return; }
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (isInvincible || isWin) { Debug.Log("Ignore GameOver (invincible or already win)"); return; }

        isGameOver = true;
        Debug.Log("GameOver triggered");
        Time.timeScale = 0f;
        if (gameOverUi) gameOverUi.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame called");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ===== Win & Cheat =====
    public void WinGame()
    {
        if (isGameOver || isWin) return;
        isWin = true;
        Debug.Log("WinGame triggered");
        Time.timeScale = 0f;
        if (winUi) winUi.SetActive(true);
    }

    public void CheatWin()
    {
        score += 9999; // muốn bao nhiêu thì chỉnh
        UpdateScore();
        WinGame();
        Debug.Log("CheatWin executed");
    }

    public void AddScoreCheat(int amount)
    {
        score += amount;
        UpdateScore();
        Debug.Log($"Cheat +{amount} points");
    }

    public void ToggleInvincible()
    {
        isInvincible = !isInvincible;
        Debug.Log("Invincible: " + isInvincible);
    }

    // ===== Query =====
    public bool IsGameOver() => isGameOver;
    public bool IsWin() => isWin;
    public bool IsInvincible() => isInvincible;
}