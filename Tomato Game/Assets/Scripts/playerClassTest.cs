using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerClassTest : MonoBehaviour
{
    public string current_class;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/saveData.son";
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            saveData loadedData = JsonUtility.FromJson<saveData>(json);
            current_class = loadedData.player_class;
        }
        if (current_class == "melee")
        {
            Debug.Log("melee");
        }
        else if (current_class == "ranged")
        {
            Debug.Log("ranged");
        }
        else if (current_class == "mage")
        {
            Debug.Log("mage");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
