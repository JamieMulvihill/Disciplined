using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zones = new GameObject[9];

    private bool isQuarantining;
    private bool areaDetermined;
    public bool allZonesDown;

    private int zoneToMove;

    private Manager managerScript;
    private Quarantine[] quarantineScript = new Quarantine[9];

    void Start()
    {
        for(int i = 0; i < zones.Length; i++)
        {
            quarantineScript[i] = zones[i].GetComponent<Quarantine>();
        }
        managerScript = this.gameObject.GetComponent<Manager>();
        isQuarantining = false;
        areaDetermined = false;
    }

    void Update()
    {
        if(managerScript.zone1 == true)
        {
            quarantineScript[0].raiseWalls = true;
        }
        else
        {
            quarantineScript[0].raiseWalls = false;
        }

        if(managerScript.zone2 == true)
        {
            quarantineScript[1].raiseWalls = true;
        }
        else
        {
            quarantineScript[1].raiseWalls = false;
        }

        if (managerScript.zone3 == true)
        {
            quarantineScript[2].raiseWalls = true;
        }
        else
        {
            quarantineScript[2].raiseWalls = false;
        }

        if (managerScript.zone4 == true)
        {
            quarantineScript[3].raiseWalls = true;
        }
        else
        {
            quarantineScript[3].raiseWalls = false;
        }

        if (managerScript.zone5 == true)
        {
            quarantineScript[4].raiseWalls = true;
        }
        else
        {
            quarantineScript[4].raiseWalls = false;
        }

        if (managerScript.zone1 == true || managerScript.zone2 == true)
        {
            quarantineScript[5].raiseWalls = true;
        }
        else
        {
            quarantineScript[5].raiseWalls = false;
        }

        if (managerScript.zone1 == true || managerScript.zone4 == true)
        {
            quarantineScript[6].raiseWalls = true;
        }
        else
        {
            quarantineScript[6].raiseWalls = false;
        }

        if (managerScript.zone2 == true || managerScript.zone3 == true)
        {
            quarantineScript[7].raiseWalls = true;
        }
        else
        {
            quarantineScript[7].raiseWalls = false;
        }

        if (managerScript.zone3 == true || managerScript.zone5 == true)
        {
            quarantineScript[8].raiseWalls = true;
        }
        else
        {
            quarantineScript[8].raiseWalls = false;
        }
    }

    void Timer(int _zoneToLower)
    {
        //pass in zone to lower and after a period of time (quaranting time) then lower the zone
    }
}
