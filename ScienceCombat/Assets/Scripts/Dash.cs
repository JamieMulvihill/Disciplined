using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour{

    [SerializeField] private string playerAlt1;
    [SerializeField] private float meshSize = 1;
    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerSelector playerSelector;

    void Update(){
        if (pc.isCaptured) return;

        if (playerSelector.targetedPlayer == 1)
        {
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
}
