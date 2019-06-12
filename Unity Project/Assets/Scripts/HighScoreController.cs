using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using WumpusEngine;

public class HighScoreController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> highScoreObjects;
    [SerializeField]
    private GameObject highScoresParent;

    private List<HighScoreInfo> highScores;

    [SerializeField]
    private GameObject highScorePrefab;

    private GameControl master;

    // Start is called before the first frame update
    void Start()
    {
        master = GameControl.GetMaintained();
        foreach (GameObject go in highScoreObjects)
        {
            Destroy(go);
        }
        foreach (HighScoreInfo highScore in this.master.GetHighscores())
        {
            GameObject go = Instantiate(highScorePrefab, highScoresParent.transform);
            highScoreObjects.Add(go);
            go.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = highScore.Name;
            go.transform.Find("PlayerMap").GetComponent<TextMeshProUGUI>().text = highScore.CaveName;
            go.transform.Find("PlayerCoins").GetComponent<TextMeshProUGUI>().text = highScore.Coins.ToString();
            go.transform.Find("PlayerArrows").GetComponent<TextMeshProUGUI>().text = highScore.Arrows.ToString();
            go.transform.Find("PlayerPoints").GetComponent<TextMeshProUGUI>().text = highScore.Points.ToString();
        }
    }

    public void QuitToMenue()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
