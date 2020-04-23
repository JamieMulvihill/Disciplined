using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerContainer playerSpawner;
    public Camera camera;
    [SerializeField] private Canvas UICanvas;
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
                scientist.controllerIndex = i;
                Instantiate(playerSpawner.chosenPlayers[i].Scientist);
                for (int j = 0; j < 4; j++)
                {
                    string bar = "Player ";
                    bar += j + 1;
                    UICanvas.transform.Find(bar + " Primary").GetComponent<cooldownUI>().firstUpdate = true;
                    UICanvas.transform.Find(bar + " Secondary").GetComponent<cooldownUI>().firstUpdate = true;
                }
            }
        }
    }

}