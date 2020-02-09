using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int targetedPlayer = 1;

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Physicist");
        player2 = GameObject.FindWithTag("Biologist");
        player3 = GameObject.FindWithTag("Chemist");
        player4 = GameObject.FindWithTag("Engineer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player1"))
        {
            targetedPlayer = 1;
        }
        else if (Input.GetButtonDown("Player2"))
        {
            targetedPlayer = 2;
        }
        else if (Input.GetButtonDown("Player3"))
        {
            targetedPlayer = 3;
        }
        else if (Input.GetButtonDown("Player4"))
        {
            targetedPlayer = 4;
        }
    }
}
