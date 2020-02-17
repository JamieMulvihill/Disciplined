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
    private bool coRoutineNotStarted = false;

    [Header("Transform")]
    public Transform realT;

    float delayTime = 0;

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
            realT.position = target.transform.position;
            Debug.Log("Working??");
            coRoutineNotStarted = true;
            //StartCoroutine(Delay());
            if (Time.time - delayTime > 2.5f) {
                spawningPlayer = false;
            }
            //if (waited == true)
            //{
            //    waited = false;
               
            //    //newestPlayer.GetComponent<Laser>().enabled = true;
            //    //newestPlayer.GetComponent<Scientist>().enabled = true;
            //}
        }
    }

    public void SpawnFetus()
    {
        Instantiate(fakePlayer);
    }

    public void SpawnPlayer() // for spawning different scientists, just pass in player desired
    {
        newestPlayer = Instantiate(managerScript.queuedRespawns.Dequeue());
        realT = newestPlayer.transform;
        spawningPlayer = true;
        spawnerAnim.Play("Spawn");
        delayTime = Time.time;
        //get all components of player being spawned, probably use a switch or something
        //disable all components
        //newestPlayer.GetComponent<Laser>().enabled = false;
        //newestPlayer.GetComponent<Scientist>().enabled = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        waited = true;
        coRoutineNotStarted = false;
    }
}
