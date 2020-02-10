using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbyArm : MonoBehaviour
{
    [SerializeField] GameObject hand; // the object which collides with a player which is to be grabbed
    [SerializeField] private float animationLength;
    [SerializeField] private float armReach;
    private float armProgress = 0f; // represents the current distance reached while a grab attack is being performed
    [SerializeField] private string playerAlt;
    [SerializeField] private float forwardOffset;
    [SerializeField] private float armSpeed;
    [SerializeField] private float lifespan; // represents how many seconds a grabby arm may be used before it breaks
    private float remainingLifespan; // represents how many seconds a grabby arm may be used before it breaks
    bool usingArm = true; // if false, arm will be put away next turn
    bool retracting = false; // equal to true when arm is being retracted toward the player

    private float originalSpeed;
    private GameObject hitGuy;


    //debug variables
    private Transform cylinderBaseTransform;
    [SerializeField] GameObject cylinder;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Scientist>())
        {
            hitGuy = other.gameObject;
        }
    }
    bool Grab()
    {
        //GetComponent<PlayerController>().Speed = 0f;
        


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
        originalSpeed = GetComponent<Scientist>().speed;
        remainingLifespan = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(playerAlt))
        {
            usingArm = true;
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
            if (armProgress <= 0 && retracting && hitGuy == null)
            {
                retracting = false;
                armProgress = 0;
                usingArm = false;
            }
        }
        // decreases remainingLifespan while arm is out and puts the arm away if remainingLifespan hits 0
        if (usingArm)
        {
            remainingLifespan -= Time.deltaTime;
            if (remainingLifespan <= 0f)
            {
                usingArm = false;
            }
        }
        if (usingArm)
        {
            if (armProgress < armReach && !retracting)
            {
                armProgress += Time.deltaTime * armSpeed;
            }
            else
            {
                retracting = true;
                armProgress -= Time.deltaTime * armSpeed;
            }
        }
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
            armProgress = (hitGuy.transform.position - transform.position).magnitude;
            retracting = true;
        }
        cylinder.transform.localPosition = new Vector3(0f, 0f, armProgress);
        hand.transform.localPosition = new Vector3(0f, 0f, armProgress*2);
        cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, armProgress, cylinder.transform.localScale.z);
    }
}
