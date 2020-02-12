using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phil's branch test.
// Phil's second branch test.

public class Laser : MonoBehaviour
{
    [SerializeField] private float magnitude;
    [SerializeField] private float radius;
    [SerializeField] private float laserDamage = 15;
    LineRenderer line;
    [SerializeField] private string playerFire;
    [SerializeField] private Scientist scientist;
    public ParticleSystem laserBeam;
    public ParticleSystem laserSpawn;
    public ParticleSystem laserHit;
    public float laserSpeed = 200f;

    void Start()
    {
        laserSpawn = Instantiate(laserSpawn, gameObject.transform.position + gameObject.transform.forward, Quaternion.identity);
        laserBeam = Instantiate(laserBeam, gameObject.transform.position + gameObject.transform.forward, Quaternion.identity);
        laserHit = Instantiate(laserHit, gameObject.transform.position + gameObject.transform.forward, Quaternion.identity);
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (scientist.isCaptured) return;


        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * magnitude, Color.green, .1f);

            line.enabled = true;
            laserSpawn.transform.position = gameObject.transform.position + gameObject.transform.forward;
            laserSpawn.Play();
            //laserBeam.transform.position = gameObject.transform.forward * (30* Time.deltaTime);
            //laserBeam.Play();
            if(!laserBeam.IsAlive())
            {
                laserBeam.Play();
            }

            if (Physics.Raycast(transform.position, transform.forward, out hit, magnitude))
            {    
                if (hit.collider != null && hit.collider.gameObject.tag != "Fireball" && hit.collider.gameObject.tag != "Acid") {
                  
                    
                    line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, hit.distance, 1f));
                    
                    
                    print("Collided");
                    GameObject enemy = hit.collider.gameObject;
                    if (enemy != null) {
                        print("Hit GameObject");
                        Health enemyHealth = enemy.GetComponent<Health>();
                        if (enemyHealth != null) {
                            laserHit.Play();
                            laserHit.transform.position = hit.point;
                            laserBeam.transform.position = LaserEffect(gameObject.transform.position + gameObject.transform.forward, Vector3.MoveTowards(laserBeam.transform.position, hit.point, .5f), hit.point);
                            enemyHealth.TakeDamage(laserDamage * Time.deltaTime);
                        }
                    }
                }
            }
            else{
                laserHit.Stop();
                laserBeam.Stop();
                line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, magnitude, 1f));     
            }
        }
        else
        {
            laserBeam.transform.position = gameObject.transform.position + gameObject.transform.forward;
            laserSpawn.transform.position = gameObject.transform.position + gameObject.transform.forward;
            line.enabled = false;
            laserSpawn.Stop();
            laserBeam.Stop();
            laserHit.Stop();
        }

    }

    Vector3 LaserEffect(Vector3 start, Vector3 current, Vector3 target) {
        float magnitude = Vector3.Distance(target,current);
        if (magnitude < .1) {
            return start;
        }
        return current;
    }
}
