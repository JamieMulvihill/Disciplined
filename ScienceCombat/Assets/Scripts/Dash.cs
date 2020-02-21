using System.Collections;
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

    public void DisignateController(int controllerIndex)
    {
        playerAlt1 = "AltMove" + controllerIndex.ToString();
    }
}
