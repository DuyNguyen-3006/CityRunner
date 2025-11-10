using UnityEngine;

public class CheatInput : MonoBehaviour
{
    // Cho phép đổi phím hành động ngay trong Inspector nếu muốn
    [Header("Action Keys")]
    [SerializeField] private KeyCode invincibleKey = KeyCode.C;  // cùng UpArrow
    [SerializeField] private KeyCode addScoreKey = KeyCode.K;  // cùng RightArrow
    [SerializeField] private KeyCode winKey = KeyCode.F10;// cùng DownArrow
    [SerializeField] private KeyCode restartKey = KeyCode.R;  // cùng LeftArrow

    [Header("Modifiers")]
    [SerializeField] private bool requireShift = true; // phải giữ Shift?

    void Update()
    {
        // (Tuỳ chọn) không cho cheat khi game đang pause:
        // if (Time.timeScale == 0f) return;

        if (requireShift && !IsShiftHeld()) return;

        // Shift + Up + C -> Invincible
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(invincibleKey))
        {
            GameManager.Instance?.ToggleInvincible();
        }

        // Shift + Right + K -> +10 điểm
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(addScoreKey))
        {
            GameManager.Instance?.AddScoreCheat(10);
        }

        // Shift + Down + F10 -> Win luôn
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(winKey))
        {
            GameManager.Instance?.CheatWin();
        }

        // Shift + Left + R -> Restart
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(restartKey))
        {
            GameManager.Instance?.RestartGame();
        }
    }

    private bool IsShiftHeld()
        => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
}
