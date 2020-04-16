using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayers : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    private QuarantineManager QMScript;
    private BoxCollider zoneCollider;
    private List<Transform> playerTransforms;

    private void Start()
    {
        //zoneCollider = GetComponent<BoxCollider>();

        //QMScript = manager.GetComponent<QuarantineManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Biologist" || other.gameObject.tag == "Chemist" || other.gameObject.tag == "Engineer" || other.gameObject.tag == "Physicist")
        //{
        //    QMScript.playersToKill.Enqueue(other.gameObject);
        //    QMScript.playersInQueue = true;
        //    print("Player Queued");
        //}
    }
    public void PlayersWithinZoneCheck(Vector3 size)
    {
        playerTransforms = new List<Transform>();
        playerTransforms = Camera.main.GetComponent<MultipleTargetCamera>().targets;
        Bounds bounds = new Bounds(transform.position, size);

        foreach (Transform position in playerTransforms){
            if (bounds.Contains(position.transform.position)){
                Scientist inZoneScientist = position.GetComponent<Scientist>();
                if (inZoneScientist){
                    inZoneScientist.GetComponent<Health>().health = 0;
                    //playerTransforms.Remove(position);
                    //Camera.main.GetComponent<MultipleTargetCamera>().RemoveDeadPlayer(position);
                }
            }
        }
    }

}
