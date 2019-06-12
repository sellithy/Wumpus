using UnityEngine.SceneManagement;
using WumpusEngine;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    private float timer = 0;
    private bool winDone = false;

    [SerializeField]
    private float delay = 1f;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private ParticleSystem confetti;

    [SerializeField]
    private GameObject playerName;

    [SerializeField]
    private Text Name;

    private GameControl master;

    private void Start()
    {
        playerName.SetActive(false);
        this.master = GameControl.GetMaintained();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay && winDone == false)
        {
            winDone = true;
            DoWin();
        }
    }

    /// <summary>
    /// Returns to the menu
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SaveHighScore()
    {
        playerName.SetActive(true);
    }

    public void SaveName()
    {
        this.master.AddHighscore(Name.text, true);
        SceneManager.LoadScene("HighScore");
    }

    private void DoWin()
    {
        playerAnimator.SetTrigger("Jump");
        confetti.Play();
        AudioController.Instance.PlaySoundEffect("Party Blower");
    }
}