using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItems : MonoBehaviour
{
    [Header("WayPoints")]
    private GameObject goFrom;
    private GameObject goTo;

    [Header("Game Objects")]
    private GameObject manager;
    [SerializeField] private GameObject particle;

    [Header("Transform")]
    private Transform T;
    private Transform goFromT;
    private Transform goToT;

    [Header("Ints")]
    private int entranceCounter;
    private int PLCounter;
    private int speed;
    private int chosenPipeLine;
    public int rarity;

    [Header("Bools")]
    private bool waited;

    [Header("Scripts")]
    private PipeLineWaypoints plw;
    private Manager managerScript;
    private ParticleSystem particles;

    [Header("Colours")]
    private Color gold;
    private Color silver;
    private Color bronze;

    // Phil---------------------------------------------------
    public bool hasSpawned = false;
    // -------------------------------------------------------

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();
        particles = particle.GetComponent<ParticleSystem>();

        T = this.transform;
        T.position = plw.entranceWPs[0].transform.position;
        goToT = plw.entranceWPs[1].transform;

        gold = new Color(255, 255, 0);
        silver = new Color(222, 222, 222);
        bronze = new Color(255, 143, 0);

        entranceCounter = 1;
        PLCounter = 0;
        speed = 5;
        chosenPipeLine = 0;
        waited = false;

        switch (rarity)
        {
            case 0:
                particles.startColor = bronze;
                break;
            case 1:
                particles.startColor = silver;
                break;
            case 2:
                particles.startColor = gold;
                break;
        }
    }

    void ChoosePipeLine()
    {
        chosenPipeLine = Random.Range(1, 4);
    }

    void Update()
    {
        T.position = Vector3.MoveTowards(T.position, goToT.position, speed * Time.deltaTime);

        if (chosenPipeLine == 0 && T.position == goToT.position)
        {
            if (goToT == plw.conducter.transform)
            {
                ChoosePipeLine();
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
        else
        {
            CheckPathing();
        }

        if (managerScript.activateItemParticles == true)
        {
            particle.SetActive(true);
        }
        else
        {
            particle.SetActive(false);
        }
    }

    void CheckPathing()
    {
        switch (chosenPipeLine)
        {
            case 1:
                //PipeLine 1
                if (T.position == goToT.position && PLCounter != plw.PL1WPs.Length)
                {
                    goToT = plw.PL1WPs[PLCounter].transform;
                    PLCounter++;
                } else if (T.position == goToT.position && PLCounter == plw.PL1WPs.Length)
                {
                    plw.pipeAnims[0].SetBool("Spit", true);
                    goToT.position = T.position;
                    StartCoroutine(Delay());
                    if (waited == true)
                    {
                        goToT = plw.PL1endPoint.transform;
                        speed = 20;
                    }
                }

                if (T.position == goToT.position && goToT == plw.PL1endPoint.transform)
                {
                    // Phil-----------------------------------------------
                    hasSpawned = true;
                    // ---------------------------------------------------
                    plw.pipeAnims[0].SetBool("Spit", false);
                    //StartCoroutine(DestroyItem());
                }

                break;

            case 2:
                //PipeLine 2
                if (T.position == goToT.position && PLCounter != plw.PL2WPs.Length)
                {
                    goToT = plw.PL2WPs[PLCounter].transform;
                    PLCounter++;
                } else if (T.position == goToT.position && PLCounter == plw.PL2WPs.Length)
                {
                    plw.pipeAnims[1].SetBool("Spit", true);
                    goToT.position = T.position;
                    StartCoroutine(Delay());
                    if(waited == true)
                    {
                        goToT = plw.PL2endPoint.transform;
                        speed = 20;
                    } 
                }

                if (T.position == goToT.position && goToT == plw.PL2endPoint.transform)
                {
                    // Phil-----------------------------------------------
                    hasSpawned = true;
                    // ---------------------------------------------------
                    plw.pipeAnims[1].SetBool("Spit", false);
                    //StartCoroutine(DestroyItem());
                }

                break;

            case 3:
                //PipeLine 3
                if (T.position == goToT.position && PLCounter != plw.PL3WPs.Length)
                {
                    goToT = plw.PL3WPs[PLCounter].transform;
                    PLCounter++;
                } else if (T.position == goToT.position && PLCounter == plw.PL3WPs.Length)
                {
                    plw.pipeAnims[2].SetBool("Spit", true);
                    goToT.position = T.position;
                    StartCoroutine(Delay());
                    if (waited == true)
                    {
                        goToT = plw.PL3endPoint.transform;
                        speed = 20;
                    }
                }

                if (T.position == goToT.position && goToT == plw.PL3endPoint.transform)
                {
                    // Phil-----------------------------------------------
                    hasSpawned = true;
                    // ---------------------------------------------------
                    plw.pipeAnims[2].SetBool("Spit", false);
                    //StartCoroutine(DestroyItem());
                }

                break;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.45f);
        waited = true;
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}