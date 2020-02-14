using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineWaypoints : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject[] items = new GameObject[3];
    [SerializeField] private GameObject[] playerClones = new GameObject[4];
    public GameObject[] pipeHeads = new GameObject[3];

    [Header("Waypoints")]
    public GameObject[] entranceWPs = new GameObject[5];
    public GameObject[] PL1WPs = new GameObject[23];
    public GameObject[] PL2WPs = new GameObject[28];
    public GameObject[] PL3WPs = new GameObject[25];
    public GameObject PL1endPoint;
    public GameObject PL2endPoint;
    public GameObject PL3endPoint;
    public GameObject conducter;

    [Header("Scripts")]
    private Manager managerScript;
    private CharacterSpawn cSpawnScript;

    [Header("Components")]
    public Animator[] pipeAnims = new Animator[3];

    [Header("Bools")]
    private bool canSpawn;
    private bool pipelineInMotion;
    private bool isObjectPlayer;

    void Start()
    {
        managerScript = this.gameObject.GetComponent<Manager>();
        cSpawnScript = this.gameObject.GetComponent<CharacterSpawn>();
        for (int i = 0; i < 3; i++)
        {
            pipeAnims[i] = pipeHeads[i].GetComponentInChildren<Animator>();
        }
        isObjectPlayer = false;
        canSpawn = true;
        pipelineInMotion = false;
    }

    private void Update()
    {
        if (managerScript.pipelineInMotion == true)
        {
            pipelineInMotion = true;
        } else
        {
            pipelineInMotion = false;
        }

        if (managerScript.spawnItem == true)
        {
            managerScript.spawnItem = false;
            isObjectPlayer = false;
            canSpawn = true;
            StartCoroutine(SendIt(isObjectPlayer));
        }

        if (managerScript.spawnPlayer == true)
        {
            managerScript.spawnPlayer = false;
            isObjectPlayer = true;
            canSpawn = true;
            StartCoroutine(SendIt(isObjectPlayer));
        }

        if (managerScript.playerHasDied == true)
        {
            managerScript.playerHasDied = false;
            managerScript.queuedRespawns.Enqueue(playerClones[0]);
            //change to which player has died
        }

        if (canSpawn == true && pipelineInMotion == true)
        {
            StartCoroutine(SendIt(isObjectPlayer));
        }

        if (managerScript.queuedRespawns.Count > 0)
        {
            isObjectPlayer = true;
        }
        else
        {
            isObjectPlayer = false;
        }
    }

    void SpawnItem()
    {
        int rand = Random.Range(0, 3);
        //print("Rarity: " + rand);
        GameObject newItem = Instantiate(items[rand]);
        newItem.GetComponent<PipeItems>().rarity = rand;
    }

    IEnumerator SendIt(bool _isObjectPlayer)
    {
        canSpawn = false;
        //if isobjectplayer == true then check to see which player does not have a character in game
        if(_isObjectPlayer == true)
        {
            cSpawnScript.SpawnFetus();
        } else
        {
            SpawnItem();
        }
        yield return new WaitForSeconds(managerScript.itemSpawnDelay);
        canSpawn = true;
    }
}

/*
 * Version 1 Map
[Header("GameObjects")]
public GameObject item;
public GameObject newPlayer;

[Header("Bools")]
public bool spawnItem;
public bool spawnPlayer;


[Header("Waypoints")]
public GameObject[] waypoints = new GameObject[32];
public GameObject endPoint;
void Start()
{
    spawnItem = true;
}

private void Update()
{
    if (spawnItem == true)
    {
        spawnItem = false;
        StartCoroutine(Delay());
    }
}

void SpawnItem()
{
    GameObject newItem = Instantiate(item);
    print("item spawned");
}

IEnumerator Delay()
{
    SpawnItem();
    yield return new WaitForSeconds(10f);
    spawnItem = true;
}
*/