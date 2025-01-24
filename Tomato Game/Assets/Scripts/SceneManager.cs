using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;
    // Start is called before the first frame update
    void Start()
    {
            menu2.SetActive(false);
            menu1.SetActive(true);
    }

    public void Play()
    {
            menu2.SetActive(true);
            menu1.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
