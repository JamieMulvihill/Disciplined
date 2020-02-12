using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbyArm : MonoBehaviour
{
    [SerializeField] GameObject hand; // the object which collides with a player which is to be grabbed
    [SerializeField] private float animationLength;
    [SerializeField] private float maxArmReach;
    private float armReach;
    private float armProgress = 0f; // represents the current distance reached while a grab attack is being performed
    [SerializeField] private string playerAlt;
    [SerializeField] private float forwardOffset;
    [SerializeField] private float armSpeed;
    [SerializeField] private float lifespan; // represents how many seconds a grabby arm may be used before it breaks
    private float remainingLifespan; // represents how many seconds a grabby arm may be used before it breaks
    bool usingArm = false; // if false, arm will be put away next turn
    bool retracting = false; // equal to true when arm is being retracted toward the player

    private float originalSpeed;
    private GameObject hitGuy;


    //debug variables
    private Transform cylinderBaseTransform;
    [SerializeField] GameObject cylinder;
    private float GetArmSpace()
    {
        if (Input.GetButtonDown(playerAlt))
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxArmReach);
            for (int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
            {
                //hits[hitIndex]
                if (hits[hitIndex].collider != null && hits[hitIndex].collider.gameObject.tag != "Fireball" && hits[hitIndex].collider.gameObject.tag != "Acid" && hits[hitIndex].collider.gameObject != hand && hits[hitIndex].collider.gameObject != cylinder)
                {
                    //if (hits[hitIndex].collider.gameObject.GetComponent<Scientist>())
                    //{
                    //hitGuy = hits[hitIndex].collider.gameObject;
                    return Vector3.Distance(transform.position, hits[hitIndex].point) - forwardOffset;
                    //}
                }
            }
        }
        return maxArmReach - forwardOffset;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Scientist>())
        {
            hitGuy = other.gameObject;
            hitGuy.GetComponent<Scientist>().isCaptured = true;
        }
    }
    //bool Grab()
    //{
    //    //GetComponent<PlayerController>().Speed = 0f;
        


    //    if (Input.GetButtonDown(playerAlt))
    //    {
    //        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, armReach);
    //        for(int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
    //        {
    //            //hits[hitIndex]
    //            if (hits[hitIndex].collider != null)
    //            {
    //                if (hits[hitIndex].collider.gameObject.GetComponent<Scientist>())
    //                {
    //                    hitGuy = hits[hitIndex].collider.gameObject;
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = GetComponent<Scientist>().speed;
        remainingLifespan = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingArm)
        {
            hand.SetActive(true);
            cylinder.SetActive(true);
        }
        else
        {
            hand.SetActive(false);
            cylinder.SetActive(false);
        }
        if (Input.GetButtonDown(playerAlt) && remainingLifespan > 0f)
        {
            usingArm = true;
            armReach = GetArmSpace();
        }
        if (usingArm)
        {
            if (Input.GetButtonUp(playerAlt))
            {
                retracting = true;
            }
        }
        // puts arm away if arm has finished retracting and has not caught anything
        if (usingArm)
        {
            if (armProgress <= 0f && retracting && hitGuy == null)
            {
                retracting = false;
                armProgress = 0f;
                usingArm = false;
            }
        }
        // decreases remainingLifespan while arm is out and puts the arm away if remainingLifespan hits 0
        if (usingArm && hitGuy != null)
        {
            remainingLifespan -= Time.deltaTime;
            if (remainingLifespan <= 0f)
            {
                usingArm = false;
            }
        }
        // if arm is out
        // if less than reach of arm and not retracting, increase arm progress
        // if greater than 0 and retracting, decrease arm progress
        if (usingArm)
        {
            if (armProgress < armReach && !retracting)
            {
                armProgress += Time.deltaTime * armSpeed;
            }
            if (armProgress > 0f && retracting)
            {
                retracting = true;
                armProgress -= Time.deltaTime * armSpeed;
            }
        }
        if (hitGuy != null)
        {
            hitGuy.transform.position = transform.position + (transform.forward * armProgress) + (transform.forward * forwardOffset);
            retracting = true;
            if (remainingLifespan <= 0f)
            {
                hitGuy.GetComponent<Scientist>().isCaptured = false;
                hitGuy = null;
                //GetComponent<PlayerController>().Speed =
                return;
            }
        }
        cylinder.transform.localPosition = new Vector3(0f, 0f, (armProgress + forwardOffset) / 2);
        hand.transform.localPosition = new Vector3(0f, 0f, (armProgress + forwardOffset));
        cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, (armProgress + forwardOffset) / 2, cylinder.transform.localScale.z);
    }
}
