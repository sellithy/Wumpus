using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WumpusEngine;
using TMPro;

public class MainGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject hexGame;
    [SerializeField]
    private GameObject[] decoyHexes;
    [SerializeField]
    private Text statusMessage;
    [SerializeField]
    private TextMeshProUGUI hazardStatus;
    [SerializeField]
    private Text roomNumberRead;
    [SerializeField]
    private GameObject[] menuButtons;
    [SerializeField]
    private GameObject bats;
    [SerializeField]
    private MouseController mc;
    [SerializeField]
    private GameObject pepsiMan;
    [SerializeField]
    private GameObject pitSpikes;

    public GameControl master;
    private bool check = false;

    // TO-DO
    // Give pieces of trivia in each room
    // Finish trivia implementation


    // Start is called before the first frame update
    void Awake()
    {
        master = GameControl.GetMaintained();
        master.ChooseLayout(1);
        CreateDoors();
        this.statusMessage.text = "Started Game";
        for (int i = 1; i < this.menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
    }

    private void Start()
    {
        AudioController.Instance.PlayMusic("Deoxys", 0.25f, true, 43);
        bats.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {      
        States state = this.master.GetState();
        switch (this.master.GetState())
        {
            case States.Wandering:
                this.hexGame.SetActive(true);
                break;
            case States.InRoom:
                this.statusMessage.text = "";
                CreateDoors();
                mc.UpdateDoors();
                for(int i = 0; i < this.master.IsHazardsNear().Length; i++)
                {
                    this.statusMessage.text += this.master.IsHazardsNear()[i] + "\n";
                }
                this.roomNumberRead.text = this.master.GetHint();
                bool[] hazard = this.master.IsHazardsInRoom();
                HazardsInRoom(hazard);
                Debug.Log(master.RoomNumber());
                this.master.ClientDoneWithHexGame();
                break;
        }
    }

    public void MenuTrigger()
    {
        if (check)
        {
            for (int i = 1; i < this.menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(false);
            }
            this.statusMessage.text = "";
            for (int i = 0; i < this.master.IsHazardsNear().Length; i++)
            {
                this.statusMessage.text += this.master.IsHazardsNear()[i] + "\n";
            }
            check = false;
        }
        else
        {
            for (int i = 1; i < this.menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(true);
            }
            this.statusMessage.text = "Arrows: " + this.master.GetPlayerInfo()[1] + "\nCoins: " + this.master.GetPlayerInfo()[2];
            check = true;
        }
    }

    public void QuitToMenuOpen()
    {
        SceneManager.LoadScene("QuitConfirmation", LoadSceneMode.Additive);
    }

    //check if hazards are in room
    public void HazardsInRoom(bool[] Hazards)
    {
        if (Hazards[2] == false)
        {
            //Delete PepsiMan
            pepsiMan.SetActive(false);
        }
        if (Hazards[2] == true)
        {
            this.hazardStatus.SetText("Wumpus is here!");
            //PEPSI MAAAAAAAAAAAAAAAAAAAAAAAN
            AudioController.Instance.PlayMusic("PepsiMan", 1f, true, 0);
            //Instantiate PEPSI MAN
            pepsiMan.SetActive(true);
            BattleWumpusTrigger();
        }
        else if (Hazards[1] == true)
        {
            this.hazardStatus.SetText("Pitfall is here!");
            // Pop up Pit
            pitSpikes.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<HiroController>().enabled = false;
            Invoke("PitTrigger", 1f);
        }
        else if (Hazards[0] == true)
        {
            this.hazardStatus.SetText("Bats are here!");
            // Pop up Bats
            bats.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<HiroController>().enabled = false;            
            Invoke("MovePlayerWithBats", 1f);
        } 
        else
        {
            //Nothing is here
            this.hazardStatus.SetText("No hazards here!");
        }
    }

    public void CreateDoors()
    {
        for (int i = 0; i < this.decoyHexes.Length; i++)
        {
            this.decoyHexes[i].SetActive(false);
        }
        for (int i = 0; i < this.master.GetDoors().Length; i++)
        {
            this.decoyHexes[this.master.GetDoors()[i]].SetActive(true);
        }
    }

    public void PurchaseSecretTrigger()
    {
        this.master.PurchaseSecret();
        for (int i = 1; i < this.menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
        SceneManager.LoadScene("FullScreenText", LoadSceneMode.Additive);
    }

    public void BattleWumpusTrigger()
    {
        this.master.BattleWumpus();
        for (int i = 1; i < this.menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
        SceneManager.LoadScene("FullScreenText", LoadSceneMode.Additive);
    }

    public void PurchaseArrowTrigger()
    {
        this.master.PurchaseArrows();
        for (int i = 1; i < this.menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
        SceneManager.LoadScene("FullScreenText", LoadSceneMode.Additive);
    }

    public void PitTrigger()
    {
        AudioController.Instance.PlaySoundEffect("Spikes");
        pitSpikes.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<HiroController>().enabled = true;

        this.master.Pitfall();
        for (int i = 1; i < this.menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
        SceneManager.LoadScene("FullScreenText", LoadSceneMode.Additive);
    }

    private void MovePlayerWithBats()
    {
        AudioController.Instance.PlaySoundEffect("Bird Flapping");
        bats.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<HiroController>().enabled = true;

        this.master.BatTransport();
        this.master.GetDoors();
    }

    public void ShootingArrow(char direction)
    {
        bool isWumpusDead = this.master.ShootArrow(direction);


        if (this.master.GetPlayerInfo()[1] == 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (isWumpusDead)
        {
            //Win the game
            
            SceneManager.LoadScene("WinScreen");
        }
    }

    public Text GetHintText()
    {
        return roomNumberRead;
    }
}