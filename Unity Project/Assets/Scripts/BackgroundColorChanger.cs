using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundColorChanger : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    private float change;

    Vector3 offsets;

    private void Start()
    {
        AudioController.Instance.PlayMusic("Zonnestraal", .1f, true);
        cam = Camera.main;
        offsets = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        Color c = cam.backgroundColor;

        c.r = Mathf.PerlinNoise(Time.time * change, Mathf.Sin(Time.time * change) + offsets.x);
        c.g = Mathf.PerlinNoise(Time.time * change, Mathf.Sin(Time.time * change) + offsets.y);
        c.b = Mathf.PerlinNoise(Time.time * change, Mathf.Sin(Time.time * change) + offsets.z);

        cam.backgroundColor = c;
    }
}