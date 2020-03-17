using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArm : MonoBehaviour
{
    Animator armAnimation;
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
        armAnimation = armVisual.GetComponent<Animator>();
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
        string tag = other.gameObject.tag;
        if (!usingArm)
        {
            return;
        }
        else if (tag != "Engineer" && tag != "Biologist" && tag != "Physicist" && tag != "Chemist")
        {
            if (tag != "Fireball" && tag != "Acid" && tag != "Virus" && tag != "Vines" && tag != "Punch Hitbox" && tag != "Quarentine")
            {
                if (!retracting)
                {
                    retracting = true;
                    Debug.Log("Retracted robotic arm because of: " + other.gameObject.name);
                }
            }
            return;
        }
        else if (other.gameObject.GetComponent<Scientist>().isCaptured == false)
        {
            hitGuy = other.gameObject;
            hitGuy.GetComponent<Scientist>().isCaptured = true;
            hand.GetComponent<Collider>().enabled = false;
        }
    }
    void Start()
    {
        playerAlt += GetComponent<Scientist>().controllerIndex.ToString();
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
        originalSpeed = GetComponent<Scientist>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Scientist>().isCaptured && usingArm)
        {
            retracting = false;
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
            retracting = false;
        }
        if (Input.GetButtonDown(playerAlt) && !onCooldown && GetComponent<Scientist>().isCaptured == false)
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
        if (retracting)
        {
            armAnimation.SetFloat("direction", -1f * armSpeed);
        }
        else
        {
            armAnimation.SetFloat("direction", 1f * armSpeed);
        }
        // puts arm away if arm has finished retracting and has not caught anything
        if (usingArm)
        {
            if (retracting && (hand.transform.position - gameObject.transform.position).magnitude <= forwardOffset)
            {
                if (hitGuy != null)
                {
                    hitGuy.GetComponent<Scientist>().isCaptured = false;
                    hitGuy = null;
                }
                retracting = false;
                usingArm = false;
                onCooldown = true;
            }
        }
        if (hitGuy != null)
        {
            hitGuy.transform.position = hand.transform.position;
            retracting = true;
        }
        var armVisualPosition = armVisual.transform.localPosition;
        armVisualPosition.Set(armVisualPosition.x, armVisualPosition.y, armVisualPosition.z + forwardOffset);
    }

    public void DisignateController(int controllerIndex)
    {
        playerAlt = "AltMove" + controllerIndex.ToString();
    }
}
