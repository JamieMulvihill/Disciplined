using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QuarantineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zones = new GameObject[9];
    [SerializeField] private GameObject[] wallForces = new GameObject[5];
    [SerializeField] private GameObject[] playerChecks = new GameObject[5];
    [SerializeField] private GameObject[] gasPos = new GameObject[5];
    [SerializeField] private GameObject gasPrefab;
    [SerializeField] private GameObject newGas;

    public Queue<GameObject> playersToKill;
    private bool isQuarantining;
    public bool playersInQueue;

    private int zoneToMove;
    private int lastNum;

    private float delayTime;

    private Manager managerScript;
    private Quarantine[] quarantineScript = new Quarantine[9];
    public GameObject[] navZones = new GameObject[6];
    public ModifyNavMesh navMesh;
    public GameObject cameraShaker;
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
                StartCoroutine(WarningShake(zone, 2f));
               
            }

            if (managerScript.zone2 == true)
            {
                cameraShaker.GetComponent<CameraShake>().shake = true;
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone2 = false;
                int zone = 1;
                StartCoroutine(WarningShake(zone, 2f));
            }

            if (managerScript.zone3 == true)
            {
                cameraShaker.GetComponent<CameraShake>().shake = true;
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone3 = false;
                int zone = 2;
                StartCoroutine(WarningShake(zone, 2f));
            }

            if (managerScript.zone4 == true)
            {
                cameraShaker.GetComponent<CameraShake>().shake = true;
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone4 = false;
                int zone = 3;
                StartCoroutine(WarningShake(zone, 2f));
            }

            if (managerScript.zone5 == true)
            {
                cameraShaker.GetComponent<CameraShake>().shake = true;
                managerScript.canQuarantine = false;
                isQuarantining = true;
                managerScript.zone5 = false;
                int zone = 4;
                StartCoroutine(WarningShake(zone, 2f));
            }

            if (managerScript.auto == true)
            {
                cameraShaker.GetComponent<CameraShake>().shake = true;
                managerScript.canQuarantine = false;
                isQuarantining = true;
                int rand = Random.Range(0, 5);
                while (rand == lastNum)
                {
                    rand = Random.Range(0, 5);
                }
                lastNum = rand;
                StartCoroutine(WarningShake(rand, 2f));
            }
        }
    }

    void Quarantine(int quarantinedZone)
    {
        wallForces[quarantinedZone].SetActive(true);

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
        Destroy(newGas);
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

    public void KillZone(int _activeZone){
        newGas =  Instantiate(gasPrefab, gasPos[_activeZone].transform);
        zoneToMove = _activeZone;
        playerChecks[_activeZone].SetActive(true);
        NavMeshHandler(_activeZone);
        navZones[_activeZone].GetComponent<CheckForPlayers>().PlayersWithinZoneCheck(navZones[_activeZone].GetComponent<NavMeshObstacle>().size);
        if (playersInQueue == false){
            playerChecks[_activeZone].SetActive(false);
            Invoke("DeQuarantine", 5);
        }
    }

    public void NavMeshHandler(int _activeZone) {
        if (!navMesh.GrantWithinZone(navZones[_activeZone])){
            navZones[_activeZone].SetActive(true);
            StartCoroutine(ReActivateZone(_activeZone, 5));
        }

        else if (navMesh.GrantWithinZone(navZones[_activeZone])) {
            for (int i = 0; i < navZones.Length; i++) {
                if (i != _activeZone) {
                    navZones[i].SetActive(true);
                    StartCoroutine(ReActivateZone(i, 5));
                }
            }
        }
    }

    IEnumerator ReActivateZone(int zone, float delay){

        yield return new WaitForSeconds(delay);
        navZones[zone].SetActive(false);
        if (zone < 5)
        {
            wallForces[zone].SetActive(false);
        }
    }

    IEnumerator WarningShake(int zone, float delay)
    {
        cameraShaker.GetComponent<CameraShake>().shake = true;
        yield return new WaitForSeconds(delay);
        Quarantine(zone);
    }

}