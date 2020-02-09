using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private string triggerButton;
    [SerializeField] private Projectile projectilePrefab;
    private Projectile projectile;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float fireRate = 1f;
    private float lastShot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the trigger is pressed.
        if (Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f)            
        {
            // If enough time has passed.
            if (Time.time > fireRate + lastShot)
            {
                // Create a spawn point in front and above of the scientist.
                Vector3 SpawnPoint = transform.position + (transform.forward * 2) + (transform.up * 2);

                // Create a projectile at the spawn point.
                projectile = Instantiate(projectilePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));

                /// GetComponent is expensive...
                // Set the velocity of the projectile.
                Vector3 scientistVelocity = GetComponentInParent<Scientist>().GetComponent<Rigidbody>().velocity;
                projectile.GetComponent<Rigidbody>().velocity = new Vector3(scientistVelocity.x + horizontalVelocity * transform.forward.x, verticalVelocity, scientistVelocity.z + horizontalVelocity * transform.forward.z);
                
                // Keep track of the last time a projectile was launched.
                lastShot = Time.time;
            }
        }
    }
}
