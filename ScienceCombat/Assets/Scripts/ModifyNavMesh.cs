using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModifyNavMesh : MonoBehaviour
{
    public GameObject grant;

    public bool GrantWithinZone(GameObject navZone) {

        // create a bounds the size of the navZone and check id the grant within the defined bounds
        Bounds bounds = new Bounds(navZone.transform.position, navZone.GetComponent<NavMeshObstacle>().size);
        if (bounds.Contains(grant.transform.position)) {
            return true;
        }
        return false;
    }
}
