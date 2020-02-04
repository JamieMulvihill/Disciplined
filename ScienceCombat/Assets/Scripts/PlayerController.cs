﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed
      
    {
        get
        {
            return this.speed;
        }
        set
        {
            this.speed = value;
        }
    }

    public float value;
    public Rigidbody rigidBody;
    [SerializeField] private string playerHOR, PlayerVer;
    [SerializeField] private float rotationSpeed;
    public bool isCaptured { get { return speed == 0; } }


    private void Start()
    {
        Debug.Log("Collide");
        Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.green, 2.5f);
    }

    void FixedUpdate()
    {

        Vector3 joystickDirection = new Vector3(Input.GetAxis(playerHOR), 0, Input.GetAxis(PlayerVer));

        // If the stick is not at rest.
        if (Mathf.Abs(Input.GetAxis(playerHOR)) > 0.01f || Mathf.Abs(Input.GetAxis(PlayerVer)) > 0.01f)
        {
            value = Mathf.Abs(Input.GetAxis(playerHOR));
            // Setting the rotation of the player to turn towards the direction of the joystick.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(joystickDirection, transform.up), rotationSpeed);
        }
        // Setting the velocity of the player
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y,0) + transform.forward * new Vector3(Mathf.Abs(joystickDirection.x),0, Mathf.Abs(joystickDirection.z)).magnitude * speed;
    }


}
