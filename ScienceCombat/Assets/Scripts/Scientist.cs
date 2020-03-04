﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject death;
    public string playerHOR, PlayerVer;
    public bool isCaptured = false;
    public bool hasGrant = false;
    public float grantEarnings = 0f;
    [SerializeField] private Health healthManager;
    [SerializeField] private SpriteRenderer healthSprite;
    public int controllerIndex;
    public Grant grant;
    [SerializeField] private GameObject [] scientistPrefabs;
    public Animator anim;

    private GameObject manager;

    void Start()
    {
        playerHOR += GetComponent<Scientist>().controllerIndex.ToString();
        PlayerVer += GetComponent<Scientist>().controllerIndex.ToString();
        manager = GameObject.FindGameObjectWithTag("Manager");
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
        Camera.main.GetComponent<MultipleTargetCamera>().AddPlayer(gameObject.transform);
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
            
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        // Setting the velocity of the player
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * new Vector3(Mathf.Abs(joystickDirection.x), 0, Mathf.Abs(joystickDirection.z)).magnitude * speed;
        
    }

    void Update()
    {
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
        
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

    public void EarningGrant(float cash) {
        grantEarnings += cash;
        Debug.Log("BLING BLING MOFOS");

    }
}
