using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Variables 
    [Header("Overheat values")]
    public Overheat overheat;
    [SerializeField] protected float heatPerUse = 0f;
    [SerializeField] protected float cooloffPerSecond = 0f;
    [SerializeField] protected float chillThreshold = 100f;

    [Header("Other Settings")]
    [SerializeField] private float magnitude;
    [SerializeField] private float radius;
    [SerializeField] private float laserDamage = 15;
    [SerializeField] private Scientist scientist; 
    public string playerFire; // string used to define the inout
    public GameObject laser; // GameObject for particle effects
    private Vector3 laserPosition; // offeet for the laser from the Physicist
    public GameObject enemy;

    [Header("Cooling down delay")]
    [SerializeField] protected float timeToChill = 0f;

    LineRenderer line;
    GameObject laserObject;
    bool isOn = false;

    void Start()
    {
        overheat = new Overheat();
        playerFire += GetComponent<Scientist>().controllerIndex.ToString();
        laserPosition = transform.GetChild(3).transform.position;
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {

        overheat.chillThreshold = chillThreshold; // allows value to be adjusted in editor during play
        overheat.timeToChill = timeToChill; // allows value to be adjusted in editor during play
        overheat.Chill(cooloffPerSecond * Time.deltaTime);

        if (scientist.isCaptured) return;

        // Set the laser postion offset value to be the Position of the Laser GameObject, hwich is a child of the Physicist
        laserPosition = transform.GetChild(3).transform.position;

        //Check if the Player has input to fire the lase and the laser is not Overheated
        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
        {
            overheat.Broil(heatPerUse * Time.deltaTime);

            // If the laser isnt already firing, instantiate a laser and set it to on
            if (!isOn) {
                laserObject = Instantiate(laser, laserPosition + gameObject.transform.forward, Quaternion.identity);
                isOn = true;
            }

            // Create a RayCastHit to store the result of the raycast for identifing what has been hit
            RaycastHit hit;

            // Line renderer is used to create the bas of the lasert effect
            line.enabled = true;
            laserObject.transform.position = laserPosition + transform.forward;
            
            // Cast a ray from the position of the laser 
            if (Physics.Raycast(laserPosition, transform.forward, out hit, magnitude)) {    

                // store what the ray has hit as the enemy and check the tag to see if hit object is valid
                enemy = hit.collider.gameObject;
                laserObject.transform.GetChild(1).transform.position = laserPosition + transform.forward;

                if (hit.collider != null && hit.collider.gameObject.tag != "Fireball" && hit.collider.gameObject.tag != "Acid"
                    && hit.collider.tag != "Virus" && hit.collider.tag != "Vines" && hit.collider.tag != "Punch Hitbox")
                {
                    line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, hit.distance, 1f));

                    // if enemy is valid, check if it has health component, if it does, add the extra particle effects
                    // if it does not, just use the standard particle effects
                    if (enemy != null)
                    {
                        Health enemyHealth = enemy.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            laserObject.transform.GetChild(0).transform.position =
                                LaserEffect(laserPosition, Vector3.MoveTowards(laserObject.transform.GetChild(0).transform.position, hit.point, .5f), hit.point);
                            laserObject.transform.GetChild(2).gameObject.SetActive(true);
                            laserObject.transform.GetChild(2).transform.position = hit.point;enemyHealth.TakeDamage(laserDamage * Time.deltaTime);
                        }
                        else
                        {
                            laserObject.transform.GetChild(2).gameObject.SetActive(false);
                            laserObject.transform.GetChild(0).transform.position = laserPosition + transform.forward;
                        }
                    }
                }
            }
        }

        // if the player isnt firing the laser, turn it off, detroy the laser object and turn off the line renderer 
        else {
            isOn = false;
            Destroy(laserObject);
            line.enabled = false;
        }
    }

    // function to move the laser particle effect along the lser from the base to the end
    Vector3 LaserEffect(Vector3 start, Vector3 current, Vector3 target) {
        // Get the distance from where the particle currently is and check it against the magnitude
        // if lower than a specicified distance set the particle to back to the start of the laser
        // else set it the current position
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
    private void OnDestroy()
    {
        Destroy(laserObject);
    }

}
