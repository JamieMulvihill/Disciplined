using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Scripts")]
    private camManager camScript;
    private PipeLineWaypoints pipelineScript;

    [Header("Camera")]
    public bool orbitCam;
    public bool orbitAnticlockwise;

    [Header("Spawning")]
    public bool pipelineInMotion;
    public bool spawnItem;
    public bool spawnPlayer;
    public bool playerHasDied;

    [Header("Quarantines")]
    public bool canQuarantine;
    public bool auto;
    public bool zone1;
    public bool zone2;
    public bool zone3;
    public bool zone4;
    public bool zone5;
    [Range(1, 5)]
    public int quarantineSpeed;

    [Header("Activation")]
    public bool activateItemParticles;

    [Header("Floats")]
    public float itemSpawnDelay;

    [Header("Lists")]
    private List<GameObject> players = new List<GameObject>();

    [Header("Queue")]
    public Queue<GameObject> queuedRespawns = new Queue<GameObject>();

    private void Start()
    {
        orbitCam = false;
        orbitAnticlockwise = false;
        spawnItem = false;
        spawnPlayer = false;
        activateItemParticles = false;
        zone1 = false;
        zone2 = false;
        zone3 = false;
        zone4 = false;
        zone5 = false;
        auto = false;
        canQuarantine = true;

        // add players to list
    }
}