using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModifyNavMesh : MonoBehaviour
{
    public bool quarenTime = false;
    public GameObject navZone;
    NavMeshSurface navSurface;
    float delayTime;
    // Start is called before the first frame update
   private void Start()
   {
       //delayTime = Time.time;
       //navSurface = gameObject.GetComponent<NavMeshSurface>();
   }

    //// Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - delayTime > 10f) {
            delayTime = Time.time;
            quarenTime = true;
        }

        if (quarenTime) {
            navZone.SetActive(true);
            quarenTime = false;
        }
        //else
        //    navZone.SetActive(false);
    }
}
