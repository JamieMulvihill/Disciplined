using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbyArm : MonoBehaviour
{
    [SerializeField] private float animationLength;
    [SerializeField] private float armReach;
    [SerializeField] private string playerAlt;
    [SerializeField] private float forwardOffset;
    private GameObject hitGuy;
    bool Grab()
    {
        if (Input.GetButtonDown(playerAlt))
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, armReach);
            for(int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
            {
                //hits[hitIndex]
                if (hits[hitIndex].collider != null)
                {
                    if (hits[hitIndex].collider.gameObject.GetComponent<PlayerController>())
                    {
                        hitGuy = hits[hitIndex].collider.gameObject;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Grab() || hitGuy != null)
        {
            //transform.position
            if (Vector3.Distance(hitGuy.transform.position, transform.position + transform.forward * forwardOffset) < 1f)
            {
                        // hitGuy

                        hitGuy = null;
                //GetComponent<PlayerController>().Speed =
                return;
            }
            hitGuy.transform.position = Vector3.Lerp(hitGuy.transform.position, transform.position + transform.forward * forwardOffset, Time.deltaTime * 50);
        }
    }
}
