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
    private GameObject newestPlayer;

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
           
            StartCoroutine(Delay());
            if (waited == true)
            {
                spawnerAnim.SetBool("SpawnPlayer", false);
                waited = false;
                print("stopping spawn anim");
                spawningPlayer = false;
                //newestPlayer.GetComponent<Laser>().enabled = true;
                //newestPlayer.GetComponent<Scientist>().enabled = true;
            }
        }
    }
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //There is a bug with the animation here,
    //SpawnPlayer will not repeat more than once regardless of being set back to being false -_-
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public void SpawnFetus()
    {
        Instantiate(fakePlayer);
    }

    public void SpawnPlayer() // for spawning different scientists, just pass in player desired
    {
        newestPlayer = Instantiate(managerScript.queuedRespawns.Dequeue());
        print("Player Dequeued");
        realT = newestPlayer.transform;
        spawningPlayer = true;
        //get all components of player being spawned, probably use a switch or something
        //disable all components
        //newestPlayer.GetComponent<Laser>().enabled = false;
        //newestPlayer.GetComponent<Scientist>().enabled = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        waited = true;
    }
}
