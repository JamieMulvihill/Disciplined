using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayers : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    private List<Transform> playerTransforms;

    // Function to kill players within a Quaratined Zone
    public void PlayersWithinZoneCheck(Vector3 size)
    {
        // populate the List of Transforms with the Transforms of each player
        // create a bounds the size of the Quarantine Zone
        playerTransforms = new List<Transform>();
        playerTransforms = Camera.main.GetComponent<MultipleTargetCamera>().targets;
        Bounds bounds = new Bounds(transform.position, size);

        // loop through the list of player transforms and check are they with the bounds
        // if they are, get the Scientist Component and set its health to 0
        foreach (Transform position in playerTransforms){
            if (bounds.Contains(position.transform.position)){
                Scientist inZoneScientist = position.GetComponent<Scientist>();
                if (inZoneScientist){
                    inZoneScientist.GetComponent<Health>().health = 0;
                }
            }
        }
    }

}
