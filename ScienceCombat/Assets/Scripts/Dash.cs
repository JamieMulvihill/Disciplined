using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Dash : MonoBehaviour {

    [Header("Overheat values")]
    public Overheat overheat;
    [SerializeField] protected float heatPerUse = 0f;
    [SerializeField] protected float cooloffPerSecond = 0f;
    [SerializeField] protected float chillThreshold = 100f;

    [Header("Other Settings")]
    public string playerAlt1;
    [SerializeField] private float meshSize = 1;
    [SerializeField] private Scientist scientist;
    [SerializeField] private GameObject wormholeEntrance;
    [SerializeField] private GameObject wormholeExit;
    private void Start()
    {
        overheat = new Overheat();
        playerAlt1 += GetComponent<Scientist>().controllerIndex.ToString();
    }

    void Update(){

       overheat.chillThreshold = chillThreshold;
       overheat.Chill(cooloffPerSecond * Time.deltaTime);
       if (scientist.isCaptured) return;

       // Check is the input for Dash pressed
       if (Input.GetButtonDown(playerAlt1) && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
       {
            overheat.Broil(heatPerUse);
            // store the result of the raycast
            RaycastHit hit;
            Vector3 RayPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(wormholeEntrance, new Vector3(RayPos.x, 2, RayPos.z), Quaternion.identity);
            
            // check if the Ray has hit anything within 10 units in fromt of the Physicist
            // adjust the Physiscist position according to result of raycast
            if (Physics.Raycast(RayPos, transform.forward, out hit, 10, ~(1 << 8)) && hit.collider != null)
            {
                Instantiate(wormholeExit, new Vector3(hit.point.x - (transform.forward.x), 2, hit.point.z - (transform.forward.z)), Quaternion.identity);
                transform.position = new Vector3(hit.point.x - (transform.forward.x), 0, hit.point.z - (transform.forward.z));
            }
            else
            {

                Instantiate(wormholeExit, new Vector3(transform.position.x, 2, transform.position.z) + transform.forward * 10, Quaternion.identity);
                transform.position += transform.forward * 10;
            }
       }
    }
}
