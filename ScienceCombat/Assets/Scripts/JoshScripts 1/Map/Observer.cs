using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private Color[] colours;
    private Color selectedColour;

    private MeshRenderer rend;


    void Start()
    {
        rend = this.gameObject.GetComponent<MeshRenderer>();
        int rand = Random.Range(0, 8);
        PickColour(rand);
    }


    void PickColour(int colour)
    {
        switch(colour)
        {
            case 0:
                rend.material.color = Color.red;
                break;

            case 1:
                rend.material.color = Color.green;
                break;

            case 2:
                rend.material.color = Color.yellow;
                break;

            case 3:
                rend.material.color = Color.black;
                break;

            case 4:
                rend.material.color = Color.blue;
                break;

            case 5:
                rend.material.color = Color.cyan;
                break;

            case 6:
                rend.material.color = Color.magenta;
                break;

            case 7:
                rend.material.color = Color.grey;
                break;
        }
    }
}
