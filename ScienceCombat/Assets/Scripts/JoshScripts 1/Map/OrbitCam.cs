using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCam : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject camPivot;
    [SerializeField] private GameObject target;

    public GameObject manager;

    [Header("Floats")]
    [SerializeField] private float time;

    [Header("Scripts")]
    private Manager managerScript;

    private void Start()
    {
        managerScript = manager.gameObject.GetComponent<Manager>();
    }
    void Update()
    {
        this.transform.LookAt(target.transform);
        if (managerScript.orbitAnticlockwise == false)
            Orbit();
        else
            OrbitACW();
    }

    void Orbit()
    {
        transform.RotateAround(camPivot.transform.position, Vector3.up, time * Time.deltaTime);
    }
    void OrbitACW()
    {
        transform.RotateAround(camPivot.transform.position, Vector3.down, time * Time.deltaTime);
    }
}
