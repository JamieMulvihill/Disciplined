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
    public string playerFire;
    [SerializeField] private Scientist scientist;
    public ParticleSystem laserBeam;
    public ParticleSystem laserSpawn;
    public ParticleSystem laserHit;
    public float laserSpeed = 200f;
    public GameObject enemy;
    private Vector3 laserPosition;
    void Start()
    {
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
        laserPosition = transform.GetChild(1).transform.position;
        laserSpawn = Instantiate(laserSpawn, laserPosition + gameObject.transform.forward, Quaternion.identity);
        laserBeam = Instantiate(laserBeam, laserPosition + gameObject.transform.forward, Quaternion.identity);
        laserHit = Instantiate(laserHit, laserPosition + gameObject.transform.forward, Quaternion.identity);
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (scientist.isCaptured) return;


        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f)
        {
            laserPosition = transform.GetChild(1).transform.position;
            RaycastHit hit;
            Debug.DrawRay(laserPosition, transform.forward * magnitude, Color.green, .1f);

            line.enabled = true;
            laserSpawn.transform.position = laserPosition + gameObject.transform.forward;
            laserSpawn.Play();
           
            if(!laserBeam.IsAlive())
            {
                laserBeam.Play();
            }

            if (Physics.Raycast(laserPosition, transform.forward, out hit, magnitude)) {    

                enemy = hit.collider.gameObject;
                if (hit.collider != null && hit.collider.gameObject.tag != "Fireball" && hit.collider.gameObject.tag != "Acid"){
                    line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, hit.distance, 1f));
                    if (enemy != null){
                        Health enemyHealth = enemy.GetComponent<Health>();
                        if (enemyHealth != null){
                            laserHit.Play();
                            laserHit.transform.position = hit.point;
                            laserBeam.transform.position = LaserEffect(laserPosition + gameObject.transform.forward, Vector3.MoveTowards(laserBeam.transform.position, hit.point, .5f), hit.point);
                            enemyHealth.TakeDamage(laserDamage * Time.deltaTime);
                        }
                        else{
                            laserBeam.transform.position = Vector3.zero;
                            laserHit.Stop();
                            laserBeam.Stop();
                        }
                    }
                } 
            } 
        }
        else {
            laserBeam.transform.position = laserPosition + gameObject.transform.forward;
            laserSpawn.transform.position = laserPosition + gameObject.transform.forward;
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

    public void DisignateController(int controllerIndex)
    {
        playerFire = "Fire" + controllerIndex.ToString();
    }
}
