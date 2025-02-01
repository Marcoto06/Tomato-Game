using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaScript : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPowerUp()
    {
        Instantiate(powerUp, spawnPoint.transform.position, Quaternion.identity);
    }
}
