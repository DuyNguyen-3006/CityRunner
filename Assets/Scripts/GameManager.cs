using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] private string gameSceneName = "Game";

    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;   // ← THÊM: Text để hiển thị High Score
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject winUi;

    private const string HIGHSCORE_KEY = "high_score";        // ← THÊM: key lưu PlayerPrefs

    private int score = 0;
    private int highScore = 0;                             // ← THÊM
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

        // Đọc high score lưu trước đó
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        UpdateScore();
        UpdateHighScoreUI();
    }

    // ================= SCORE =================
    public void AddScore(int points)
    {
        if (isGameOver || isWin) return;
        score += points;
        UpdateScore();
        // (tuỳ chọn) Cập nhật realtime nếu muốn: TryUpdateHighScore();
    }

    private void UpdateScore()
    {
        if (!scoreText)
        {
            Debug.LogWarning("GameManager: scoreText is not assigned!");
            return;
        }
        scoreText.text = score.ToString();
    }

    // ================= HIGH SCORE =================
    private void TryUpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
            Debug.Log($"New High Score: {highScore}");
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = $"Best: {highScore}";
        // Nếu muốn format khác (ví dụ chỉ số), đổi chuỗi trên.
    }

    public int GetHighScore() => highScore;

    // (tuỳ chọn) nút Reset High Score
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey(HIGHSCORE_KEY);
        UpdateHighScoreUI();
        Debug.Log("High Score reset");
    }

    // ================= GAME OVER / WIN =================
    public void GameOver()
    {
        if (isInvincible || isWin) { Debug.Log("Ignore GameOver (invincible or already win)"); return; }

        isGameOver = true;
        // Cập nhật & lưu high score khi kết thúc lượt
        TryUpdateHighScore();

        Time.timeScale = 0f;
        ShowCursorForUI(true);
        if (gameOverUi) gameOverUi.SetActive(true);
        Debug.Log("GameOver triggered");
    }

    public void WinGame()
    {
        if (isGameOver || isWin) return;

        isWin = true;
        // Cập nhật & lưu high score khi kết thúc lượt
        TryUpdateHighScore();

        Time.timeScale = 0f;
        ShowCursorForUI(true);
        if (winUi) winUi.SetActive(true);
        Debug.Log("WinGame triggered");
    }

    // ================= RESTART =================
    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        ShowCursorForUI(true);

        // KHÔNG xoá high score khi restart – chỉ reset điểm hiện tại
        isGameOver = false;
        isWin = false;
        isInvincible = false;

        score = 0;
        UpdateScore();

        Time.timeScale = 1f;
        yield return null;

        if (!string.IsNullOrEmpty(gameSceneName))
            SceneManager.LoadScene(gameSceneName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ================= CHEATS (giữ API cũ) =================
    public void CheatWin()
    {
        score += 9999;
        UpdateScore();
        WinGame();
        Debug.Log("CheatWin executed");
    }

    public void AddScoreCheat(int amount)
    {
        score += amount;
        UpdateScore();
        // (tuỳ chọn) cập nhật realtime:
        // TryUpdateHighScore();
        Debug.Log($"Cheat +{amount} points");
    }

    public void ToggleInvincible()
    {
        isInvincible = !isInvincible;
        Debug.Log("Invincible: " + isInvincible);
    }

    // ================= HELPERS =================
    private void ShowCursorForUI(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public bool IsGameOver() => isGameOver;
    public bool IsWin() => isWin;
    public bool IsInvincible() => isInvincible;
}
