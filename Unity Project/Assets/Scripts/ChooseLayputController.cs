using System.Collections;
using System.Collections.Generic;
using WumpusEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLayputController : MonoBehaviour
{
    private GameControl master;
    // Start is called before the first frame update
    void Start()
    {
        master = GameControl.GetMaintained();
    }

    public void ChooseALayout(int layoutNum)
    {
        this.master.ChooseLayout(layoutNum);
        SceneManager.LoadScene("Game");
    }
}
