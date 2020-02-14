using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePlayer : MonoBehaviour
{
    [Header("WayPoints")]
    private GameObject goFrom;
    private GameObject goTo;

    private GameObject manager;

    [Header("Transform")]
    private Transform goFromT;
    private Transform goToT;
    private Transform T;

    [Header("Ints")]
    private int entranceCounter;
    private int speed;

    [Header("Scripts")]
    private PipeLineWaypoints plw;
    private Manager managerScript;
    private CharacterSpawn cSpawnScript;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();
        cSpawnScript = manager.GetComponent<CharacterSpawn>();

        T = this.transform;
        T.position = plw.entranceWPs[0].transform.position;
        goToT = plw.entranceWPs[1].transform;

        entranceCounter = 1;
        speed = 5;
    }

    void Update()
    {
        T.position = Vector3.MoveTowards(T.position, goToT.position, speed * Time.deltaTime);

        if (T.position == goToT.position)
        {
            if (goToT == plw.conducter.transform)
            {
                SpawnRealPlayer();
                Destroy(this.gameObject);
            }
            else
            {
                entranceCounter++;
            }

            if (goToT == plw.entranceWPs[plw.entranceWPs.Length - 1].transform)
            {
                goToT = plw.conducter.transform;
            }
            else if (goToT != plw.entranceWPs[plw.entranceWPs.Length - 1].transform && goToT != plw.conducter.transform)
            {
                goToT = plw.entranceWPs[entranceCounter].transform;
            }
        }
    }

    private void SpawnRealPlayer()
    {
        cSpawnScript.SpawnPlayer();
    }
}
