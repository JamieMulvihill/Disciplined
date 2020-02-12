﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private Scientist scientist;
    private float originalSpeed;

    private void Start()
    {
        //height = transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.max.y*2f;
        StartCoroutine(deleteSelf());
        
    }


    IEnumerator deleteSelf() {

       yield return new WaitForSeconds(6);
        if (scientist == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Biologist" && other.gameObject.tag != "Ground" && scientist == null)
        {
            transform.GetChild(0).position = other.transform.position;
            transform.GetChild(0).gameObject.SetActive(true);
            scientist = other.gameObject.GetComponent<Scientist>();
            originalSpeed = scientist.speed;
            scientist.isCaptured = true;
            StartCoroutine(Capture(scientist));
        }
    }

   IEnumerator Capture(Scientist capturedScientist) {
        capturedScientist.speed = 0;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = true;
        capturedScientist.isCaptured = false;
       // capturedScientist.gameObject.transform.position = transform.position;
        //capturedScientist.gameObject.transform.position = Vector3.MoveTowards(capturedScientist.gameObject.transform.position, transform.position, .5f);
        yield return new WaitForSeconds(6);
        scientist = null;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = false;
        capturedScientist.speed = originalSpeed;
        Destroy(gameObject);
   }


}
