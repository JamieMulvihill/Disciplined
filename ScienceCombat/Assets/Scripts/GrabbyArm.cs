using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbyArm : MonoBehaviour
{
    [SerializeField] GameObject hand;               // the object which collides with a player which is to be grabbed
    [SerializeField] private float forwardOffset;
    [SerializeField] private string playerAlt;      // button to use the grab attack
    private float originalSpeed;
    private float originalRotationSpeed;
    private GameObject hitGuy;                      // the guy currently caught by grabby arm

    //cooldown variables
    [SerializeField] private float cooldown;        // how long in seconds the player must wait to be able to use the grabby arm after its been used
    private bool onCooldown = false;                // represents whether or not more time must elapse before the grabby arm can be used again
    private float cooldownCheckRate = 0.1f;         // how often it should check whether cooldown should start ticking. Setting this too big may result in longer cooldowns than expected.
    [SerializeField] private float lifespan;        // represents how many seconds a grabby arm may be used before it breaks (immediately end use of grabby arm)
    private float remainingLifespan;                // represents the remaining amount of seconds a grabby arm may be used before it breaks. Resets when cooldown ends

    //arm limit variables
    [SerializeField] private float animationLength;
    [SerializeField] private float maxArmReach;     // represents the maximum possible reach of the grab attack
    private float armReach;                         // represents how far the arm may extend under the current context (such as a wall being in the way)
    private float armProgress = 0f;                 // represents the current distance reached while a grab attack is being performed
    bool usingArm = false;                          // if false, arm will be put away next turn
    bool retracting = false;                        // equal to true when arm is being retracted toward the player
    [SerializeField] private float armSpeed;        // affects how fast the arm extends

    //debug variables
    private Transform cylinderBaseTransform;
    [SerializeField] GameObject armVisual;          // just a cylinder representing the arm at the base of the hand

    IEnumerator Cooldown() // Always running. If onCooldown is true, will wait [cooldown] seconds and then set onCooldown back to false
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(0.1f);
            if (onCooldown)
            {
                yield return new WaitForSeconds(cooldown);
                onCooldown = false;
            }
        }
    }
    void Awake()
    {
        StartCoroutine(Cooldown());
        originalSpeed = GetComponent<Scientist>().speed;
        originalRotationSpeed = GetComponent<Scientist>().rotationSpeed;
    }
    private float GetArmSpace() // returns the maximum distance the arm may extend without passing through walls
    {
        if (Input.GetButtonDown(playerAlt))
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxArmReach);
            for (int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
            {
                if (hits[hitIndex].collider != null && hits[hitIndex].collider.gameObject.tag != "Fireball" && hits[hitIndex].collider.gameObject.tag != "Acid" && hits[hitIndex].collider.gameObject != hand && hits[hitIndex].collider.gameObject != armVisual)
                {
                    return Vector3.Distance(transform.position, hits[hitIndex].point) - forwardOffset;
                }
            }
        }
        return maxArmReach - forwardOffset;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Engineer" && other.gameObject.tag != "Biologist" && other.gameObject.tag != "Physicist" && other.gameObject.tag != "Chemist")
        {
            return;
        }
        if (other.gameObject.GetComponent<Scientist>().isCaptured == false)
        {
            hitGuy = other.gameObject;
            hitGuy.GetComponent<Scientist>().isCaptured = true;
            hand.GetComponent<Collider>().enabled = false;
        }
    }
    void Start()
    {
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
        originalSpeed = GetComponent<Scientist>().speed;
        remainingLifespan = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Scientist>().isCaptured && usingArm)
        {
            retracting = false;
            armProgress = 0f;
            usingArm = false;
            onCooldown = true;
        }
        // make arm and hand active when using arm.
        if (usingArm)
        {
            hand.SetActive(true);
            armVisual.SetActive(true);
        }
        else
        {
            hand.SetActive(false);
            armVisual.SetActive(false); 
            if (GetComponent<Scientist>().isCaptured == false)
            {
                GetComponent<Scientist>().speed = originalSpeed;
                GetComponent<Scientist>().rotationSpeed = originalRotationSpeed;
            }
            remainingLifespan = lifespan;
            retracting = false;
        }
        if (Input.GetButtonDown(playerAlt) && remainingLifespan > 0f && !onCooldown)
        {
            usingArm = true;
            armReach = GetArmSpace();
            GetComponent<Scientist>().speed = 0f;
            GetComponent<Scientist>().rotationSpeed = 0f;
            hand.GetComponent<Collider>().enabled = true;
        }
        if (usingArm && hitGuy == null)
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
                onCooldown = true;
            }
        }
        // decreases remainingLifespan while arm is out and puts the arm away if remainingLifespan hits 0
        if (usingArm && hitGuy != null)
        {
            remainingLifespan -= Time.deltaTime;
            if (remainingLifespan <= 0f)
            {
                retracting = false;
                armProgress = 0f;
                usingArm = false;
                onCooldown = true;
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

                //if it is at or beyond reach of arm NOW... set to retracting
                if (armProgress >= armReach)
                {
                    retracting = true;
                }
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
                return;
            }
        }
        armVisual.transform.localPosition = new Vector3(0f, 0f, (armProgress + forwardOffset) / 2);
        hand.transform.localPosition = new Vector3(0f, 0f, (armProgress + forwardOffset));
        armVisual.transform.localScale = new Vector3(armVisual.transform.localScale.x, (armProgress + forwardOffset) / 2, armVisual.transform.localScale.z);
    }

    public void DisignateController(int controllerIndex)
    {
        playerAlt = "AltMove" + controllerIndex.ToString();
    }
}
