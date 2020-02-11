using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject topCH;
    [SerializeField] private GameObject orbitCH;

    public GameObject camera;
    public GameObject manager;

    [Header("Scripts")]
    private Manager managerScript;
    private OrbitCam orbitScript;

    [Header("Floats")]
    [SerializeField] private float speed;


    void Start()
    {
        managerScript = manager.GetComponent<Manager>();
        orbitScript = orbitCH.GetComponent<OrbitCam>();

        orbitCH.GetComponent<OrbitCam>().enabled = false;
    }

    void Update()
    {
        if (managerScript.orbitCam == false) //make it not move
        {
            if (orbitScript.enabled == true)
            {
                orbitCH.GetComponent<OrbitCam>().enabled = false;
            }
            camera.transform.position = Vector3.Lerp(camera.transform.position, topCH.transform.position, Time.deltaTime * speed);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, topCH.transform.rotation, Time.deltaTime * speed);
        }
        else if (managerScript.orbitCam == true) // make it move
        {
            if (orbitScript.enabled == false)
            {
                orbitCH.GetComponent<OrbitCam>().enabled = true;
            }
            camera.transform.position = Vector3.Lerp(camera.transform.position, orbitCH.transform.position, Time.deltaTime * speed);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, orbitCH.transform.rotation, Time.deltaTime * speed);
        }
    }
}