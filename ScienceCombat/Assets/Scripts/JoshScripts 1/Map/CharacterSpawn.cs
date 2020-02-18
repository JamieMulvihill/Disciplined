using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject realPlayer;
    [SerializeField] private GameObject fakePlayer;
    [SerializeField] private GameObject yeetPlatform;
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
    private bool yeeting;

    [Header("Transform")]
    public Transform realT;

    [Header("Floats")]
    private float delayTime;
    private float speed;


    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();

        spawnerAnim = spawner.GetComponent<Animator>();

        realT = realPlayer.transform;

        spawningPlayer = false;
        yeeting = false;
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

            if (Time.time - delayTime > 3.5f)
            {
                spawningPlayer = false;
                //yeeting = true;
            }
        }

        if(yeeting == true)
        {
            Yeet();
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
        newestPlayer.GetComponent<Rigidbody>().useGravity = false;
        spawningPlayer = true;
        spawnerAnim.Play("Spawn");
        delayTime = Time.time;
        //get all components of player being spawned, probably use a switch or something
        //disable all components
        //newestPlayer.GetComponent<Laser>().enabled = false;
        //newestPlayer.GetComponent<Scientist>().enabled = false;
    }

    void Yeet()
    {
        if(realT.position != yeetPlatform.transform.position)
            realT.position = Vector3.Lerp(realT.position, yeetPlatform.transform.position, speed * Time.deltaTime);
        else
        {
            yeeting = false;
            CalculateLanding();
        }
    }

    void CalculateLanding()
    {
        Vector2 XZ = Random.insideUnitCircle * 13;
        realT.position = new Vector3(XZ.x, yeetPlatform.transform.position.y, XZ.y);
        print(realT.position);
        RaycastHit hit;
        if (Physics.Raycast(realT.position, realT.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if(hit.transform.tag == "Environment")
            {
                realT.position = yeetPlatform.transform.position;
                CalculateLanding();
            }
        }
        else
        {
            newestPlayer.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
