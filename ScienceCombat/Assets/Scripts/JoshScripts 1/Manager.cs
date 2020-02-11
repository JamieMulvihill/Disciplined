using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Scripts")]
    private camManager camScript;
    private PipeLineWaypoints pipelineScript;

    [Header("Bools")]
    public bool orbitCam;
    public bool orbitAnticlockwise;
    public bool spawnItem;
    public bool spawnPlayer;

    [Header("Floats")]
    public float itemSpawnDelay;

    private void Start()
    {
        orbitCam = false;
        orbitAnticlockwise = false;
        spawnItem = false;
        spawnPlayer = false;
    }
}