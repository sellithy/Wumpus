using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuButtons;

    private void Start()
    {
        AudioController.Instance.PlayMusic("Deoxys", .25f, true, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ChooseLayoutScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void HighScoreTrigger()
    {
        SceneManager.LoadScene("HighScore");
    }
}
