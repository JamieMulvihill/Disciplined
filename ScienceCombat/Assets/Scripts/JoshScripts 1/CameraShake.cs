using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Transforms")]
    private Transform cam;

    [Space]

    [Header("Floats")]
    private float initialDuration;
    [SerializeField] private float power;
    [SerializeField] private float duration;
    [SerializeField] private float speed;

    [Space]

    [Header("Bools")]
    public bool shake;
    public bool activateSlowMo;
    private bool posPicked;

    [Space]

    [Header("Vector 3's")]
    private Vector3 startPosition;
    private Vector3 randPos;

    [Space]

    [Header("Scripts")]
    private TimeWarp timeWarp;

    void Start()
    {
        timeWarp = GetComponentInChildren<TimeWarp>();
        cam = this.gameObject.transform;
        shake = false;
        activateSlowMo = false;
        posPicked = false;
        initialDuration = duration;
    }


    private void Update()
    {
        if(activateSlowMo) //for testing
        {
            activateSlowMo = false;
            TriggerSlowMo();
        }
    }


    void FixedUpdate()
    {
        if (shake)
        {
            //start shaking
            if (duration > 0)
            {
                //setting the first spot to go to
                if (posPicked == false)
                {
                    posPicked = true;
                    startPosition = cam.transform.position;
                    randPos = cam.transform.position + Random.insideUnitSphere * power;
                }

                //moving to random spot
                if (cam.transform.position != randPos)
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, randPos, speed * Time.deltaTime);
                else //pick new spot when camera reaches random spot
                    randPos = cam.transform.position + Random.insideUnitSphere * power;
                //count down
                duration -= Time.deltaTime;
            }
            else
            {
                //reset all
                if (cam.transform.position != startPosition)
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, startPosition, speed * Time.deltaTime);
                else
                {
                    shake = false;
                    duration = initialDuration;
                    posPicked = false;
                }
            }
        }
    }

    public void TriggerSlowMo()
    {
        //trigger slow motion (this is for testing)
        timeWarp.triggerSlowMo = true;
    }
}