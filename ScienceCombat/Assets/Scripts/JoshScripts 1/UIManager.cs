using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool cooldown1;
    public bool cooldown2;

    void Start()
    {
        cooldown1 = false;
        cooldown2 = false;
    }

    void Update()
    {

    }
}

/*Get number of players in the game
 * 
 * create as many UI's as needed
 * 
 * Assign players to created UI
 */