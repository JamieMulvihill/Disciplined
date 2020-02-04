using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = (players[0].transform.position + players[1].transform.position + players[2].transform.position + players[3].transform.position) / 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, (players[0].transform.position + players[1].transform.position + players[2].transform.position + players[3].transform.position) / 4, 0.04f);
    }
}
