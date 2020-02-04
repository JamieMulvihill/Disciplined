using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
    public GameObject seeds;
    [SerializeField] private string playerAlt2;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float fireRate = 1f;
    private float lastShot = -1f;
    private GameObject inst;
    [SerializeField] private PlayerController playerController;

    public void Update()
    {
        if (playerController.isCaptured) return;

        if (FindObjectsOfType<Vines>().Length + FindObjectsOfType<Seeds>().Length >= 1 ) return;


        if (Input.GetButtonDown(playerAlt2))
        {
            if (Time.time > fireRate + lastShot)
            {
                Vector3 SpawnPoint = transform.position + (transform.forward * 2) + (transform.up * 2);

                inst = Instantiate(seeds, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));
                Rigidbody rigidBody = inst.GetComponent<Rigidbody>();
                rigidBody.transform.position = SpawnPoint;
                rigidBody.velocity = new Vector3(horizontalVelocity * transform.forward.x, verticalVelocity * (transform.forward.y + 1), horizontalVelocity * transform.forward.z);
                lastShot = Time.time;
            }
        }
    }
}
