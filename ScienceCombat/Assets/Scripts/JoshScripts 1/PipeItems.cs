using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItems : MonoBehaviour
{
    [Header("WayPoints")]
    private GameObject goFrom;
    private GameObject goTo;

    private GameObject manager;

    [Header("Transform")]
    private Transform T;
    private Transform goFromT;
    private Transform goToT;

    [Header("Ints")]
    private int entranceCounter;
    private int PLCounter;
    private int speed;
    private int chosenPipeLine;

    [Header("Bools")]
    private bool waited;

    [Header("Scripts")]
    private PipeLineWaypoints plw;
    private Manager managerScript;
    //private ParticleSystem particles;

    //[Header("Colours")]
    //private Color lightBlue;
    //private Color pink;
    //private Color orange;


    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        managerScript = manager.GetComponent<Manager>();
        plw = manager.GetComponent<PipeLineWaypoints>();
        //particles = this.gameObject.GetComponentInChildren<ParticleSystem>();

        T = this.transform;
        T.position = plw.entranceWPs[0].transform.position;
        goToT = plw.entranceWPs[1].transform;

        //lightBlue = new Color(0, 171, 197);
        //pink = new Color(253, 71, 255);
        //orange = new Color(255, 154, 0);

        entranceCounter = 1;
        PLCounter = 0;
        speed = 5;
        chosenPipeLine = 0;
        waited = false;
    }

    void ChoosePipeLine()
    {
        chosenPipeLine = Random.Range(1, 4);

        //if (managerScript.activateItemParticles == true)
        //{
        //    switch (chosenPipeLine)
        //    {
        //        case 1:
        //            particles.startColor = lightBlue;
        //            break;
        //        case 2:
        //            particles.startColor = pink;
        //            break;
        //        case 3:
        //            particles.startColor = orange;
        //            break;
        //    }
        //}
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

                //if (PLCounter == plw.PL1WPs.Length - 1)
                //{
                //    plw.pipeAnims[0].SetBool("Spit", true);
                //}

                if (T.position == goToT.position && goToT == plw.PL1endPoint.transform)
                {
                    plw.pipeAnims[0].SetBool("Spit", false);
                    StartCoroutine(DestroyItem());
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

                //if (PLCounter == plw.PL2WPs.Length - 1)
                //{
                //    plw.pipeAnims[1].SetBool("Spit", true);
                //}

                if (T.position == goToT.position && goToT == plw.PL2endPoint.transform)
                {
                    plw.pipeAnims[1].SetBool("Spit", false);
                    StartCoroutine(DestroyItem());
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

                //if (PLCounter == plw.PL3WPs.Length - 1)
                //{
                //    plw.pipeAnims[2].SetBool("Spit", true);
                //}

                if (T.position == goToT.position && goToT == plw.PL3endPoint.transform)
                {
                    plw.pipeAnims[2].SetBool("Spit", false);
                    StartCoroutine(DestroyItem());
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
