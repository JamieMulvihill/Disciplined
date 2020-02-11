using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineWaypoints : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] pipeHeads = new GameObject[3];

    [Header("Waypoints")]
    public GameObject[] entranceWPs = new GameObject[3];
    public GameObject[] PL1WPs = new GameObject[14];
    public GameObject[] PL2WPs = new GameObject[16];
    public GameObject[] PL3WPs = new GameObject[14];
    public GameObject PL1endPoint;
    public GameObject PL2endPoint;
    public GameObject PL3endPoint;
    public GameObject conducter;

    [Header("Bools")]
    public bool pL1anim;
    public bool pL2anim;
    public bool pL3anim;

    [Header("Scripts")]
    private Manager managerScript;

    [Header("Components")]
    public Animator[] pipeAnims = new Animator[3];

    void Start()
    {
        managerScript = this.gameObject.GetComponent<Manager>();
        for(int i = 0; i < 3; i++)
        {
            pipeAnims[i] = pipeHeads[i].GetComponent<Animator>();
        }

        pL1anim = false;
        pL2anim = false;
        pL3anim = false;
    }

    private void Update()
    {
        if (managerScript.spawnItem == true)
        {
            managerScript.spawnItem = false;
            StartCoroutine(Delay());
        }

        if (managerScript.spawnPlayer == true)
        {
            managerScript.spawnPlayer = false;
            SpawnPlayer();
        }

        //if(pL1anim == true)
        //{
        //    //pipeAnims[0].ResetTrigger("isItemSpawning");
        //    pL1anim = false;
        //    pipeAnims[0].SetTrigger("isItemSpawning");
        //}

        //if (pL2anim == true)
        //{
        //    //pipeAnims[1].ResetTrigger("isItemSpawning");
        //    pL2anim = false;
        //    pipeAnims[1].SetTrigger("isItemSpawning");
        //}

        //if (pL3anim == true)
        //{
        //    //pipeAnims[2].ResetTrigger("isItemSpawning");
        //    pL3anim = false;
        //    pipeAnims[2].SetTrigger("isItemSpawning");
        //}
    }

    void SpawnItem()
    {
        GameObject newItem = Instantiate(item);
    }

    void SpawnPlayer()
    {
        GameObject newPlayer = Instantiate(player);
    }

    IEnumerator Delay()
    {
        SpawnItem();
        yield return new WaitForSeconds(10f);
        managerScript.spawnItem = true;
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