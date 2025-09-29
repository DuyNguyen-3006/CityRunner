using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame button clicked!"); // test
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;

    }

    public void HowToPlay()
    {
        Debug.Log("HowToPlay button clicked!");
        SceneManager.LoadScene("HowToPlay");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");
        Application.Quit();
    }
}
