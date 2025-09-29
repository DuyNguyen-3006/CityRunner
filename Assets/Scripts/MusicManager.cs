using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;      // AudioSource nhạc nền
    public AudioClip[] sceneMusic;       // AudioClip theo scene (theo Build Index)

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // Giữ object qua các scene
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Lấy clip theo scene index
        if (scene.buildIndex < sceneMusic.Length)
        {
            AudioClip clip = sceneMusic[scene.buildIndex];
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
