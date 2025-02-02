using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;
    public GameObject startButton;
    public GameObject selectButton;
    public GameObject playButton;
    public string player_class;
    // Start is called before the first frame update
    void Start()
    {
        menu2.SetActive(false);
        menu1.SetActive(true);
        startButton.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (menu2.activeSelf == true && Input.GetButtonDown("Cancel"))
        {
            menu2.SetActive(false);
            menu1.SetActive(true);
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }

    public void Play()
    {
        menu2.SetActive(true);
        menu1.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectButton);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SelectClass(string selectClass)
    {
        saveData data = new saveData();
        data.player_class = selectClass;
        data.floor = 0;
        data.current_HP = 6;
        startButton.GetComponent<Button>().interactable = true;
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/saveData.json";
        System.IO.File.WriteAllText(path, json);
    }
}
