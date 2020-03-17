using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerContainer playerSpawner;
    public Camera camera;
    void Awake()
    {
        playerSpawner = FindObjectOfType<PlayerContainer>();
    }


    private void Start()
    {
        for (int i = 0; i < playerSpawner.chosenPlayers.Length; i++)
        {
            if (playerSpawner.chosenPlayers[i] != null)
            {
                Scientist scientist = playerSpawner.chosenPlayers[i].Scientist.GetComponent<Scientist>();
                //scientist.DisignateController(i + 1);
                scientist.controllerIndex = i + 1;
                Instantiate(playerSpawner.chosenPlayers[i].Scientist);
              
            }
        }
    }

}