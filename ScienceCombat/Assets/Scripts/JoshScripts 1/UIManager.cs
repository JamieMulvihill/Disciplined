using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIwps = new GameObject[4];
    //[SerializeField] private GameObject[] UIs = new GameObject[4];
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
        SetUI(numberOfPlayers);
    }

    void SetUI(int _numberOfPlayers)
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            GameObject player;
            player = Instantiate(UI, UIwps[i].transform);
            player.GetComponent<PlayerUI>().connectedPlayer = pc.chosenPlayers[i].Scientist;
            //player.GetComponent<PlayerUI>().ConnectPlayer();
        }
    }
}

/*Get number of players in the game
 * 
 * create as many UI's as needed
 * 
 * Assign players to created UI
 */