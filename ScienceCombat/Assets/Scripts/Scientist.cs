using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject death;
    public string playerHOR, PlayerVer;
    public bool isCaptured = false;
    public bool isSliding = false;
    public bool hasGrant = false;
    public float grantEarnings = 0;
    [SerializeField] private Health healthManager;
    public int controllerIndex;
    public Grant grant;
    [SerializeField] private GameObject [] scientistPrefabs;
    public Animator anim;
    public Canvas winCan;

    public Vector3 slippingVelocity;

    private GameObject manager;

    // Pickups---------------------------------------
    public string pickupButton;
    [SerializeField] private GameObject glovesPrefab;
    [SerializeField] private GameObject gogglesPrefab;
    [SerializeField] private GameObject labCoatPrefab;
    GameObject item;

    public bool holdingGloves = false;
    public bool holdingGoggles = false;
    public bool holdingLabCoat = false;
    // ----------------------------------------------

    public int test = 0;

    void Start()
    {
        playerHOR += GetComponent<Scientist>().controllerIndex.ToString();
        PlayerVer += GetComponent<Scientist>().controllerIndex.ToString();
        manager = GameObject.FindGameObjectWithTag("Manager");
        Camera.main.GetComponent<MultipleTargetCamera>().AddPlayer(gameObject.transform);
        pickupButton += GetComponent<Scientist>().controllerIndex.ToString();
    }

    void FixedUpdate()
    {
        if (isCaptured || isSliding) return;

        Vector3 joystickDirection = new Vector3(Input.GetAxis(playerHOR), 0, Input.GetAxis(PlayerVer));

        // If the stick is not at rest.
        if (Mathf.Abs(Input.GetAxis(playerHOR)) > 0.01f || Mathf.Abs(Input.GetAxis(PlayerVer)) > 0.01f)
        {
            // Setting the rotation of the player to turn towards the direction of the joystick.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(joystickDirection, transform.up), rotationSpeed);
            
            anim.SetBool("run", true);
            anim.SetFloat("pace", (1.0f/10.0f) * gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetFloat("pace", 1.0f);
        }
        // Setting the velocity of the player
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * new Vector3(Mathf.Abs(joystickDirection.x), 0, Mathf.Abs(joystickDirection.z)).magnitude * speed;
        
    }

    void Update()
    {
        test++;
        Debug.Log("real:" + gameObject.GetInstanceID());


        if (healthManager.health <= 0) {
            Camera gameCam = Camera.main;
            MultipleTargetCamera multipleTargetCamera = gameCam.GetComponent<MultipleTargetCamera>();
            multipleTargetCamera.RemoveDeadPlayer(gameObject.transform);

            if (grant)
            {
                grant.scientist = null;
                grant.isPossessed = false;
                grant.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }

            switch(gameObject.tag)
            {
                case "Biologist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(0);
                    break;
                case "Chemist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(1);
                    break;
                case "Engineer":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(2);
                    break;
                case "Physicist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(3);
                    break;
            }
            anim.Play("Death");
            Destroy(gameObject);
        }

        // Pickups---------------------------------------
        if (item != null)
        {
            //Vector3 relativeSpawnPosition = new Vector3(0, 2, 0);
            //item.transform.SetPositionAndRotation(transform.position + relativeSpawnPosition, transform.rotation);
        }
        // ----------------------------------------------

        if(grant.winner != null) 
        {
            isCaptured = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSliding)
        {
            if (collision.gameObject.tag == "Invisible Wall")
            {
            
                Vector3 reflectedVector = Vector3.Reflect(GetComponent<Rigidbody>().velocity, collision.contacts[0].normal);
                slippingVelocity = reflectedVector;
                //transform.forward = reflectedVector.normalized;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Pickups---------------------------------------
        if(other.tag == "Gloves" ||
           other.tag == "Goggles" ||
           other.tag == "Lab Coat")
        {
            if(other.gameObject.GetComponent<PipeItems>().hasSpawned)
            {
                if (Mathf.Abs(Input.GetAxis(pickupButton)) > 0.01f)
                {
                    if (!holdingGloves && other.tag == "Gloves")
                    {
                        holdingGloves = true;
                        Destroy(other.gameObject);
                    }
                    else if (!holdingGoggles && other.tag == "Goggles")
                    {
                        holdingGoggles = true;
                        Destroy(other.gameObject);
                    }
                    if (!holdingLabCoat && other.tag == "Lab Coat")
                    {
                        holdingLabCoat = true;
                        Destroy(other.gameObject);
                    }
                    //Vector3 relativeSpawnPosition = new Vector3(0, 2, 0);
                    //item = Instantiate(glovesPrefab, transform.position + relativeSpawnPosition, Quaternion.identity);
                }
            }
        }
        // ------------------------------------------------
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Gloves" ||
    //       other.tag == "Goggles" ||
    //       other.tag == "Lab Coat")
    //    {
           
    //        pickedUP = false;
    //    }
    //}

    private void OnDisable()
    {
        Instantiate(death, transform.position, transform.rotation);
    }

    public void EarningGrant(float cash) 
    {

        grantEarnings += cash;

    }
}
