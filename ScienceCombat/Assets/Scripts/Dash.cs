﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour{

    public string playerAlt1;
    [SerializeField] private float meshSize = 1;
    [SerializeField] private Scientist scientist;

    private void Start()
    {
        playerAlt1 += GetComponent<Scientist>().controllerIndex.ToString();
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
    }

    void Update(){
       if (scientist.isCaptured) return;

       if (Input.GetButtonDown(playerAlt1) && GetComponent<Scientist>().isCaptured == false)
       {

           RaycastHit hit;
           Debug.DrawRay(transform.position, transform.forward * 10, Color.green, 3f);
            Vector3 RayPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (Physics.Raycast(RayPos, transform.forward, out hit, 10, ~(1 << 8)) && hit.collider != null && hit.transform.gameObject.tag != "Quarentine")
           {
               transform.position = new Vector3(hit.point.x - (transform.forward.x), 0, hit.point.z - (transform.forward.z));
           }
           else
           {
               transform.position += transform.forward * 10;
           }
       }

    }

    public void DisignateController(int controllerIndex)
    {
        playerAlt1 = "AltMove" + controllerIndex.ToString();
    }
}
