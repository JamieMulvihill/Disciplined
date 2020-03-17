using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucker : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject homebase;
    [SerializeField] private GameObject sucker;
    [SerializeField] private GameObject[] zones = new GameObject[5];
    [SerializeField] public GameObject[] pipeWPs = new GameObject[2];
    private GameObject target;

    private float speed;
    public int zone;

    private bool playerCaught;
    private bool huntPlayers;
    public bool playersBeenSucked;

    private Vector3 targetPos;
    private Vector3 offset;

    private QuarantineManager QMScript;

    void Start()
    {
        //QMScript = manager.GetComponent<QuarantineManager>();
        offset = new Vector3(0, 11, 0);
        speed = 4;
        huntPlayers = false;
        targetPos = homebase.transform.position;
    }


    void Update()
    {
        //if (QMScript.playersInQueue == true)
        //{
        //    target = QMScript.playersToKill.Peek();
        //}
        //else
        //{
        //    target = null;
        //}
    }

    private void LateUpdate()
    {
        MoveSucker();
    }

    private void MoveSucker()
    {
        if (target)
        {
            if (!huntPlayers)
            {
                if (sucker.transform.position != zones[zone].transform.position)
                {
                    targetPos = zones[zone].transform.position;
                }
                else
                {
                    huntPlayers = true;
                }
            }
            else
            {
                if (sucker.transform.position.x != target.transform.position.x && sucker.transform.position.z != target.transform.position.z)
                {
                    Vector3 xz = new Vector3(target.transform.position.x, offset.y, target.transform.position.z);
                    targetPos = xz;
                }
                if (Mathf.Approximately(sucker.transform.position.x, target.transform.position.x) && Mathf.Approximately(sucker.transform.position.z, target.transform.position.z))
                {
                    playerCaught = true;
                    //target.GetComponent<Scientist>().beingSucked = true;
                    target.GetComponent<Rigidbody>().useGravity = false;
                    if (playersBeenSucked == true)
                    {
                        QMScript.playersToKill.Dequeue();
                        if (QMScript.playersToKill.Count == 0)
                        {
                            QMScript.playersInQueue = false;
                        }
                    }
                }
            }
        } else if(target == null && playersBeenSucked == true)
        {
            targetPos = homebase.transform.position;
        }
        //sucker.transform.position = Vector3.MoveTowards(sucker.transform.position, targetPos, speed * Time.deltaTime);
    }
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject death;
    public string playerHOR, PlayerVer;
    public bool isCaptured = false;
    [SerializeField] private Health healthManager;
    [SerializeField] private SpriteRenderer healthSprite;
    public int controllerIndex;

    private int tubeSpeed;
    public bool beingSucked;
    public bool wpcheck;
    [SerializeField] private GameObject sucker;
    public GameObject wp1;
    public GameObject wp2;
    private Vector3 targetPos;

    [SerializeField] private GameObject [] scientistPrefabs;

    private GameObject manager;

    void Start()
    {
        playerHOR += GetComponent<Scientist>().controllerIndex.ToString();
        PlayerVer += GetComponent<Scientist>().controllerIndex.ToString();
        manager = GameObject.FindGameObjectWithTag("Manager");
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
        //Camera.main.GetComponent<MultipleTargetCamera>().AddPlayer(gameObject.transform);

        beingSucked = false;
        tubeSpeed = 10;
        targetPos = wp1.transform.position;
        wpcheck = false;
    }

    void FixedUpdate()
    {
        if (isCaptured) return;

        Vector3 joystickDirection = new Vector3(Input.GetAxis(playerHOR), 0, Input.GetAxis(PlayerVer));

        // If the stick is not at rest.
        if (Mathf.Abs(Input.GetAxis(playerHOR)) > 0.01f || Mathf.Abs(Input.GetAxis(PlayerVer)) > 0.01f)
        {
            // Setting the rotation of the player to turn towards the direction of the joystick.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(joystickDirection, transform.up), rotationSpeed);
        }
        // Setting the velocity of the player
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * new Vector3(Mathf.Abs(joystickDirection.x), 0, Mathf.Abs(joystickDirection.z)).magnitude * speed;

        if(beingSucked == true)
        {
            GetSucked();
        }
    }

    void Update()
    {
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
        
        if (healthManager.health <= 0) {
            Camera gameCam = Camera.main;
            MultipleTargetCamera multipleTargetCamera = gameCam.GetComponent<MultipleTargetCamera>();
            //multipleTargetCamera.RemoveDeadPlayer(gameObject.transform);

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

            Destroy(gameObject);
        }
    }

    //public void DisignateController(int controllerIndex)
    //{
    //    this.controllerIndex = controllerIndex;
    //    playerHOR = "Horizontal" + controllerIndex.ToString();
    //    PlayerVer = "Vertical" + controllerIndex.ToString();
    //}
    private void OnDisable()
    {
        Instantiate(death, transform.position, transform.rotation);
    }

    public void GetSucked()
    {
        //if (wpcheck == false)
        //{
        //    targetPos = wp1.transform.position;
        //} else
        //{
        //    targetPos = wp2.transform.position;
        //}
        //if (this.gameObject.transform.position == wp1.transform.position)
        //{
        //    wpcheck = true;
        //    sucker.GetComponent<Sucker>().playersBeenSucked = true;
        //}
        if(this.gameObject.transform.position == wp2.transform.position)
        {
            beingSucked = false;
            this.gameObject.GetComponent<Health>().health = 0;
            sucker.GetComponent<Sucker>().playersBeenSucked = true;
        }
        else
        {
            sucker.GetComponent<Sucker>().playersBeenSucked = false;
        }
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, wp2.transform.position, tubeSpeed * Time.deltaTime);
    }
}
*/