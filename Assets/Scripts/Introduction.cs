using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private float displayTime = 5f;

    void Start()
    {
        Invoke(nameof(GoToMenu), displayTime);
    }

    void GoToMenu()
    {
        SceneCrossfade.Instance.CrossfadeToScene("MainMenu");
    }
}