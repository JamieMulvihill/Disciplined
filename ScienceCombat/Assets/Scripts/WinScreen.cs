using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WinScreen : MonoBehaviour
{
    public GameObject cam;
    public Grant grant;
    public Scientist[] sci;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Canvas>().enabled) 
        {
            cam.SetActive(true);
            cam.GetComponent<CinemachineVirtualCamera>().LookAt = grant.winner.transform;
        }
    }
}
