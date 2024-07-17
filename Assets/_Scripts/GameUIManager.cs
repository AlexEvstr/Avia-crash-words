using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pause;

    public void PauseButton()
    {
        _pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseButton()
    {
        _pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}