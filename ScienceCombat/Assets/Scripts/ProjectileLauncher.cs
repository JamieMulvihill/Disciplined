using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] protected Projectile projectilePrefab;
    public string triggerButton;
    protected Projectile projectile;
    [SerializeField] protected float verticalVelocity;
    [SerializeField] protected float horizontalVelocity;
    [SerializeField] protected float upOffset;                    // spawn position offset (up)
    [SerializeField] protected float forwardOffset;               // spawn position offset (forward)
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float momentumScalar = 0.0f;       // carries velocity from player to projectile; 1.0 = full retention
    private float lastShotTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponent<Scientist>().isCaptured)
        { 
            // If the trigger is pressed.
            if (Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f)            
            {
                // If enough time has passed.
                if (Time.time > fireRate + lastShotTime)
                {
                    // Create a spawn point in front and above of the scientist.
                    Vector3 SpawnPoint = transform.position + (transform.forward * forwardOffset) + (transform.up * upOffset);

                    // Create a projectile at the spawn point.
                    projectile = Instantiate(projectilePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));

                    /// GetComponent is expensive...
                    // Set the velocity of the projectile.
                    // base projectile speed then add player velocity
                    projectile.GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity * transform.forward.x, verticalVelocity, horizontalVelocity * transform.forward.z);
                    Vector3 scientistVelocity = GetComponentInParent<Scientist>().GetComponent<Rigidbody>().velocity;
                    projectile.GetComponent<Rigidbody>().velocity += scientistVelocity * momentumScalar; // add player velocity


                    // Keep track of the last time a projectile was launched.
                    lastShotTime = Time.time;
                }
            }
        }
    }

    public void DisignateController(int controllerIndex)
    {
        triggerButton = "Fire" + controllerIndex.ToString();
    }
}
