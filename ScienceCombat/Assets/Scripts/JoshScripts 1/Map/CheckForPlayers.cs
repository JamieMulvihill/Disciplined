using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayers : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    private QuarantineManager QMScript;


    private void Start()
    {
        //QMScript = manager.GetComponent<QuarantineManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Biologist" || other.gameObject.tag == "Chemist" || other.gameObject.tag == "Engineer" || other.gameObject.tag == "Physicist")
        {
            QMScript.playersToKill.Enqueue(other.gameObject);
            QMScript.playersInQueue = true;
            print("Player Queued");
        }
    }
}
