using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammenwerfer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectile;     // projectile which will be launched
    [SerializeField] private float upOffset;            // spawn position offset (up)
    [SerializeField] private float forwardOffset;       // spawn position offset (forward)
    [SerializeField] private float verticalVelocity;    // vertical launch velocity (up)
    [SerializeField] private float horizontalVelocity;  // horizontal launch velocity (forward)
    [SerializeField] private float fireRate = 0.25f;    // 
    private float lastTime = 0.0f;                      // the time when a projectile was last launched

    void Update()
    {
        Werf(gameObject.transform.forward.normalized, gameObject.transform.position);
    }
    public void Werf(Vector3 forwardVector, Vector3 spawnPosition) //it werfs flammen (fireballs)
    {
        // if the current time is [fireRate] seconds later than [lastTime]
        if (Time.time > fireRate + lastTime)
        {

            GameObject inst = Instantiate(projectile);
            inst.AddComponent<Rigidbody>();
            Rigidbody rigidBody = inst.GetComponent<Rigidbody>();

            spawnPosition.y += upOffset;
            rigidBody.transform.position = spawnPosition;

            //set projectile velocity
            {
                float x = horizontalVelocity * forwardVector.x;
                float y = verticalVelocity;
                float z = horizontalVelocity * forwardVector.z;
                rigidBody.velocity = new Vector3(x, y, z);
            }

            lastTime = Time.time;
        }
    }
    //private void OnColiderEnter(Collider other) 
    //{
    //    Destroy(other);
    //}
}
