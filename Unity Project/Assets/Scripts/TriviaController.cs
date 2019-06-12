using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WumpusEngine;
using TMPro;

public enum TriviaType { Secret, Arrows, Wumpus, Pitfall }

public class TriviaController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] triviaButtons;
    [SerializeField]
    private TextMeshProUGUI questionMessage;
    [SerializeField]
    private TextMeshProUGUI[] answerMessages;
    private GameControl master;

    // Start is called before the first frame update
    void Start()
    {
        this.master = GameControl.GetMaintained();
        LoadQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadQuestion()
    {
        if (this.master.GetPlayerInfo()[2] == 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }
        QuestionCard question = this.master.GetQuestion();
        this.questionMessage.text = question.GiveQuestion();
        string[] answers = (string[])question.GiveAnswers();

        for (int i = 0; i <= 3; i++)
        {
            answerMessages[i].text = answers[i];
        }
    }

    public void AnsweringQuestion(int buttonNumber)
    {
        WumpusEngine.TriviaType type = this.master.GetTriviaType();
        bool correct = this.master.AnswerQuestion(answerMessages[buttonNumber].text);
        if (correct)
        {
            questionMessage.text = "Correct!";
        }
        else
        {
            questionMessage.text = "Incorrect...";
        }

        if (this.master.DidPlayerWin() == TriviaState.InProgress)
            LoadQuestion();
        else if (this.master.DidPlayerWin() == TriviaState.Won)
        {
            //What are we fighting?
            switch (type)
            {
                case WumpusEngine.TriviaType.Secrets:
                     GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameController>().GetHintText().text = this.master.GetHint();
                    //Unload the trivia Scene
                    SceneManager.UnloadSceneAsync("FullScreenText");
                    break;
                case WumpusEngine.TriviaType.Pit:
                    //Unload the trivia Scene
                    SceneManager.UnloadSceneAsync("FullScreenText");
                    this.master.GetDoors();
                    break;
                default:
                    //Unload the trivia Scene
                    SceneManager.UnloadSceneAsync("FullScreenText");
                    break;
            }
        }
        else if (this.master.DidPlayerWin() == TriviaState.Lost)
        {
            //What are we fighting?
            switch (type)
            {
                case WumpusEngine.TriviaType.Wumpus:
                    //Lose the game
                    SceneManager.LoadScene("GameOver");
                    break;
                case WumpusEngine.TriviaType.Pit:
                    //Lose the game
                    SceneManager.LoadScene("GameOver");
                    break;
                default:
                    //Unload the trivia Scene
                    SceneManager.UnloadSceneAsync("FullScreenText");
                    break;
            }
        }
    }
}
