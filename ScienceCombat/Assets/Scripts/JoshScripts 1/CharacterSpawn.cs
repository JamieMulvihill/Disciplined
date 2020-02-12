using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject realPlayer;
    [SerializeField] private GameObject fakePlayer;
    private GameObject manager;

    public GameObject spawner;
    public GameObject target;

    [Header("Scripts")]
    private PipeLineWaypoints plw;
    private Manager managerScript;

    private Animator spawnerAnim;

    [Header("Bools")]
    private bool spawningPlayer;
    private bool waited;

    [Header("Transform")]
    private Transform realT;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();

        spawnerAnim = spawner.GetComponent<Animator>();

        realT = realPlayer.transform;

        spawningPlayer = false;
        waited = false;
    }

    void Update()
    {
        if(managerScript.spawnPlayer == true)
        {
            managerScript.spawnPlayer = false;
            Instantiate(fakePlayer);
        }

        if (spawningPlayer == true)
        {
            spawnerAnim.SetBool("SpawnPlayer", true);
            realT.position = target.transform.position;
            if (waited == true)
            {
                spawnerAnim.SetBool("SpawnPlayer", false);
                spawningPlayer = false;
            }
        }
    }

    public void SpawnPlayer()
    {
        Instantiate(realPlayer);
        realT = realPlayer.transform;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        waited = true;
    }
}
