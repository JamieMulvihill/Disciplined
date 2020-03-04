using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zones = new GameObject[9];
    [SerializeField] private GameObject[] playerChecks = new GameObject[5];
    //[SerializeField] private GameObject sucker;

    public Queue<GameObject> playersToKill;

    private bool isQuarantining;
    public bool playersInQueue;

    private int zoneToMove;
    private int lastNum;

    private float delayTime;

    private Manager managerScript;
    private Quarantine[] quarantineScript = new Quarantine[9];
    //private Sucker suckyScript;

    void Start()
    {
        playersToKill = new Queue<GameObject>();
        for (int i = 0; i < zones.Length; i++)
        {
            quarantineScript[i] = zones[i].GetComponent<Quarantine>();
        }
        managerScript = this.gameObject.GetComponent<Manager>();
        //suckyScript = sucker.gameObject.GetComponent<Sucker>();
        isQuarantining = false;
        playersInQueue = false;
    }

    void Update()
    {
        if (managerScript.canQuarantine == true && isQuarantining == false)
        {
            if (managerScript.zone1 == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone1 = false;
                int zone = 0;
                Quarantine(zone);
            }

            if (managerScript.zone2 == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone2 = false;
                int zone = 1;
                Quarantine(zone);
            }

            if (managerScript.zone3 == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone3 = false;
                int zone = 2;
                Quarantine(zone);
            }

            if (managerScript.zone4 == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone4 = false;
                int zone = 3;
                Quarantine(zone);
            }

            if (managerScript.zone5 == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone5 = false;
                int zone = 4;
                Quarantine(zone);
            }

            if (managerScript.auto == true)
            {
                managerScript.canQuarantine = false;
                isQuarantining = true;
                int rand = Random.Range(0, 5);
                while (rand == lastNum)
                {
                    rand = Random.Range(0, 5);
                }
                lastNum = rand;
                Quarantine(rand);
            }
        }
    }

    void Quarantine(int quarantinedZone)
    {
        switch (quarantinedZone)
        {
            case 0:
                quarantineScript[0].raiseWalls = true;
                quarantineScript[5].raiseWalls = true;
                quarantineScript[6].raiseWalls = true;
                break;

            case 1:
                quarantineScript[1].raiseWalls = true;
                quarantineScript[5].raiseWalls = true;
                quarantineScript[7].raiseWalls = true;
                break;

            case 2:
                quarantineScript[2].raiseWalls = true;
                quarantineScript[7].raiseWalls = true;
                quarantineScript[8].raiseWalls = true;
                break;

            case 3:
                quarantineScript[3].raiseWalls = true;
                quarantineScript[6].raiseWalls = true;
                break;

            case 4:
                quarantineScript[4].raiseWalls = true;
                quarantineScript[8].raiseWalls = true;
                break;
        }
    }

    void DeQuarantine()
    {
        switch (zoneToMove)
        {
            case 0:
                quarantineScript[0].raiseWalls = false;
                quarantineScript[5].raiseWalls = false;
                quarantineScript[6].raiseWalls = false;
                break;

            case 1:
                quarantineScript[1].raiseWalls = false;
                quarantineScript[5].raiseWalls = false;
                quarantineScript[7].raiseWalls = false;
                break;

            case 2:
                quarantineScript[2].raiseWalls = false;
                quarantineScript[7].raiseWalls = false;
                quarantineScript[8].raiseWalls = false;
                break;

            case 3:
                quarantineScript[3].raiseWalls = false;
                quarantineScript[6].raiseWalls = false;
                break;

            case 4:
                quarantineScript[4].raiseWalls = false;
                quarantineScript[8].raiseWalls = false;
                break;
        }
        isQuarantining = false;
        managerScript.canQuarantine = true;
    }

    public void KillZone(int _activeZone)
    {
        zoneToMove = _activeZone;
        playerChecks[_activeZone].SetActive(true);
        if (playersInQueue == false)
        {
            playerChecks[_activeZone].SetActive(false);
            Invoke("DeQuarantine", 5);
        }
    }
}