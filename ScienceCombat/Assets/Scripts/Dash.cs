using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour{

    [SerializeField] private string playerAlt1;
    [SerializeField] private float meshSize = 1;
    [SerializeField] private Scientist scientist;

    void Update(){
       if (scientist.isCaptured) return;

       if (Input.GetButtonDown(playerAlt1))
       {

           RaycastHit hit;
           if (Physics.Raycast(transform.position, transform.forward, out hit, 10) && hit.collider != null)
           {
               transform.position = hit.point - (transform.forward * (meshSize));
           }
           else
           {
               transform.position += transform.forward * 10;
           }
       }

    }
}
