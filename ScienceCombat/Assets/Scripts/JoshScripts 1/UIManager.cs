using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIs = new GameObject[4];
    [SerializeField] private Sprite[] images = new Sprite[4];
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject playerContainer;
    private PlayerContainer pc;

    public bool cooldown1;
    public bool cooldown2;

    private int numberOfPlayers;

    void Start()
    {
        playerContainer = GameObject.Find("PlayerContainer");
        pc = playerContainer.GetComponent<PlayerContainer>();
        cooldown1 = false;
        cooldown2 = false;
        numberOfPlayers = pc.chosenPlayers.Count(i => i != null);
        for (int i = 0; i < numberOfPlayers; i++)
        {
            UIs[i].SetActive(false);
        }
        SetUI(numberOfPlayers);
    }

    void SetUI(int _numberOfPlayers)
    {
        if (_numberOfPlayers >= 0)
        {
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                UIs[i].SetActive(true);
                if(pc.chosenPlayers[i].Scientist.gameObject.tag == "Biologist")
                {
                    UIs[i].GetComponentInChildren<Image>().sprite = images[0];
                    UIs[i].GetComponentInChildren<cooldownUI>().scientist = pc.chosenPlayers[i].Scientist.gameObject;
                }
                else if (pc.chosenPlayers[i].Scientist.gameObject.tag == "Engineer")
                {
                    UIs[i].GetComponentInChildren<Image>().sprite = images[1];
                    UIs[i].GetComponentInChildren<cooldownUI>().scientist = pc.chosenPlayers[i].Scientist.gameObject;
                }
                else if (pc.chosenPlayers[i].Scientist.gameObject.tag == "Physicist")
                {
                    UIs[i].GetComponentInChildren<Image>().sprite = images[2];
                    UIs[i].GetComponentInChildren<cooldownUI>().scientist = pc.chosenPlayers[i].Scientist.gameObject;
                }
                else if (pc.chosenPlayers[i].Scientist.gameObject.tag == "Chemist")
                {
                    UIs[i].GetComponentInChildren<Image>().sprite = images[3];
                    UIs[i].GetComponentInChildren<cooldownUI>().scientist = pc.chosenPlayers[i].Scientist.gameObject;
                }
                //player.GetComponent<PlayerUI>().connectedPlayer = pc.chosenPlayers[i].Scientist;
                //player.GetComponent<PlayerUI>().ConnectPlayer();
            }
        }
    }
}

/*Get number of players in the game
 * 
 * create as many UI's as needed
 * 
 * Assign players to created UI
 */