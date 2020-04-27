using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject realPlayer;
    public GameObject fakePlayer;
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
    //private bool yeeting;

    [Header("Transform")]
    public Transform realT;

    [Header("Floats")]
    private float delayTime;
    private float speed;

    public Camera camera;

    private Scoreboard scoreboard;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        scoreboard = FindObjectOfType<Scoreboard>();
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();

        spawnerAnim = spawner.GetComponent<Animator>();

        realT = realPlayer.transform;

        spawningPlayer = false;
        //yeeting = false;
        delayTime = 0;
        speed = 10;
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
            if (Time.time - delayTime < 2.5f)
                realT.position = target.transform.position;

            if (Time.time - delayTime > 4f)
            {
                spawningPlayer = false;
                //yeeting = true;
            }
        }

        //if(yeeting == true)
        //{
        //    Yeet();
        //}
    }

    public void SpawnFakePlayer()
    {
        //fakePlayer = managerScript.queuedRespawns.Peek();
        Instantiate(fakePlayer);
    }

    public void SpawnPlayer() // for spawning different scientists, just pass in player desired
    {
        newestPlayer = Instantiate(managerScript.queuedRespawns.Dequeue());
        realT = newestPlayer.transform;
        //scoreboard.scientists.Add(newestPlayer.GetComponent<Scientist>());
        //newestPlayer.GetComponent<Rigidbody>().useGravity = false;
        spawningPlayer = true;
        spawnerAnim.Play("Spawn");
        delayTime = Time.time;


        //get all components of player being spawned, probably use a switch or something
        //disable all components
        //newestPlayer.GetComponent<Laser>().enabled = false;
        //newestPlayer.GetComponent<Scientist>().enabled = false;
    }

    // It yeets
    //void Yeet()
    //{
    //    if(realT.position != yeetPlatform.transform.position)
    //        realT.position = Vector3.Lerp(realT.position, yeetPlatform.transform.position, speed * Time.deltaTime);
    //    else
    //    {
    //        yeeting = false;
    //        CalculateLanding();
    //    }
    //}

    //void CalculateLanding()
    //{
    //    Vector2 XZ = Random.insideUnitCircle * 13;
    //    realT.position = new Vector3(XZ.x, yeetPlatform.transform.position.y, XZ.y);
    //    RaycastHit hit;
    //    if (Physics.Raycast(realT.position, realT.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
    //    {
    //        if(hit.transform.tag == "Environment")
    //        {
    //            realT.position = yeetPlatform.transform.position;
    //            CalculateLanding();
    //        }
    //        else
    //        {
    //            newestPlayer.GetComponent<Rigidbody>().useGravity = true;
    //        }
    //    }
    //}
}
