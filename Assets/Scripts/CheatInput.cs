using UnityEngine;

public class CheatInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            if (GameManager.Instance != null) GameManager.Instance.ToggleInvincible();

        if (Input.GetKeyDown(KeyCode.K))
            if (GameManager.Instance != null) GameManager.Instance.AddScoreCheat(10);

        if (Input.GetKeyDown(KeyCode.F10)) // Win luôn + cộng điểm
            if (GameManager.Instance != null) GameManager.Instance.CheatWin();

        if (Input.GetKeyDown(KeyCode.R)) // Restart nhanh
            if (GameManager.Instance != null) GameManager.Instance.RestartGame();
    }
}