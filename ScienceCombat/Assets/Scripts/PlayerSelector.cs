using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int targetedPlayer = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        
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
