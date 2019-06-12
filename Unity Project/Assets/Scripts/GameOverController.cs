using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using WumpusEngine;

/// <summary>
/// This class controls all effects of the game over screen
/// </summary>
public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerName;

    [SerializeField]
    private Text Name;

    private GameControl master;

    //Bools to make sure the events only happen one time
    private bool hasStarted = false;
    private bool hasBloodEffectStarted = false;

    //The amount of time that has passed since the scene has been loaded
    private float timer = 0;

    //Delay after the scene loads to start the effects
    [SerializeField] float startDelay = 1;
    //The time after the animations start to do the blood effect
    [SerializeField] float bloodEffectDelay = 0.5f;

    [Space()]

    //The blood effect to play
    [SerializeField] GameObject bloodEffect;
    //The players animator
    [SerializeField] Animator playerAnim;

    public void Start()
    {
        playerName.SetActive(false);
        this.master = GameControl.GetMaintained();
    }

    /// <summary>
    /// Returns to the menu
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        //Update the timer
        timer += Time.deltaTime;

        //If the timer has passed the start delay, start things
        if (timer >= startDelay && hasStarted == false)
        {
            hasStarted = true;
            PlayDeath();
        }

        //If the timer has passed the bloodeffectDelay, start that
        if (timer >= startDelay + bloodEffectDelay && hasBloodEffectStarted == false)
        {
            hasBloodEffectStarted = true;
            PlayBloodEffect();
        }
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

    /// <summary>
    /// Plays the character death animation, and other things
    /// </summary>
    private void PlayDeath()
    {
        playerAnim.SetTrigger("Death");
        AudioController.Instance.PlaySoundEffect("Windows Shut Down Sound");
    }

    /// <summary>
    /// Plays the blood effect
    /// </summary>
    private void PlayBloodEffect()
    {
        Instantiate(bloodEffect);
    }
}