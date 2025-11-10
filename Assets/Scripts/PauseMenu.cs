using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;     // Main Pause (Resume / Options / MainMenu)
    [SerializeField] private GameObject optionsPanel;   // Options (Volume slider + key hints + Close)

    [Header("Widgets (Options)")]
    [SerializeField] private Slider musicSlider;        // 0..1
    [SerializeField] private Text volumeValueText;      // optional

    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private KeyCode toggleKey = KeyCode.Escape;

    private bool paused = false;

    void Awake()
    {
        Time.timeScale = 1f;
        ShowCursor(false);
    }

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (optionsPanel) optionsPanel.SetActive(false);

        float v = PlayerPrefs.GetFloat("music_volume", 1f);
        ApplyMusicVolume(v, applyToUI: true);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (!paused) Pause();
            else Resume();
        }
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        if (pausePanel) pausePanel.SetActive(true);
        if (optionsPanel) optionsPanel.SetActive(false);
        ShowCursor(true);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        if (pausePanel) pausePanel.SetActive(false);
        if (optionsPanel) optionsPanel.SetActive(false);
        ShowCursor(false);
    }

    public void OnResume() => Resume();

    public void OnOpenOptions()
    {
        if (!optionsPanel || !pausePanel) return;
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void OnCloseOptions()
    {
        if (!optionsPanel || !pausePanel) return;
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        ShowCursor(true);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnMusicSliderChanged(float value)
    {
        ApplyMusicVolume(value, applyToUI: false);
    }

    private void ApplyMusicVolume(float v, bool applyToUI)
    {
        v = Mathf.Clamp01(v);

        // Nếu bạn dùng MusicManager như bạn gửi, audioSource.volume sẽ đổi theo PlayerPrefs khi scene đổi.
        PlayerPrefs.SetFloat("music_volume", v);

        if (applyToUI && musicSlider) musicSlider.value = v;
        if (volumeValueText)
        {
            int percent = Mathf.RoundToInt(v * 100f);
            volumeValueText.text = percent + "%";
        }

        // Nếu muốn tác động ngay lập tức lên nhạc đang phát:
        if (MusicManager.Instance != null && MusicManager.Instance.audioSource != null)
            MusicManager.Instance.audioSource.volume = v;
    }

    private void ShowCursor(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
