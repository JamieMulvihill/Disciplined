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

        // add players to list
    }
}