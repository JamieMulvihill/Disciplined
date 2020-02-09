using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private string triggerButton;
    public Projectile projectilePrefab;
    public Projectile projectile;
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
            if (Time.time > fireRate + lastShot)
            {
                Vector3 SpawnPoint = transform.position + (transform.forward * 2) + (transform.up * 2);

                projectile = Instantiate(projectilePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));


                projectile.GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity * transform.forward.x, verticalVelocity * (transform.forward.y + 1), horizontalVelocity * transform.forward.z);

                lastShot = Time.time;
            }
        }
    }
}
