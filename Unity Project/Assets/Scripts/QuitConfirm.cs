using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitConfirm : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonConfirm()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ButtonDeny()
    {
        SceneManager.UnloadSceneAsync(3);
    }
}
