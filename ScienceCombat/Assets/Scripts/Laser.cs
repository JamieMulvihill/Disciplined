using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Overheat values")]
    public Overheat overheat;
    [SerializeField] protected float heatPerUse = 0f;
    [SerializeField] protected float cooloffPerSecond = 0f;
    [SerializeField] protected float chillThreshold = 100f;
    [Header("Other Settings")]
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
    public GameObject laser;
    GameObject inst;
    bool isOn = false;
    void Start()
    {
        overheat = new Overheat();
        playerFire += GetComponent<Scientist>().controllerIndex.ToString();
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
        laserPosition = transform.GetChild(3).transform.position;
        //laserSpawn = Instantiate(laserSpawn, laserPosition + gameObject.transform.forward, Quaternion.identity);
        //laserBeam = Instantiate(laserBeam, laserPosition + gameObject.transform.forward, Quaternion.identity);
        //laserHit = Instantiate(laserHit, laserPosition + gameObject.transform.forward, Quaternion.identity);
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {

        overheat.chillThreshold = chillThreshold; // allows value to be adjusted in editor during play
        overheat.Chill(cooloffPerSecond * Time.deltaTime);

        if (scientist.isCaptured) return;

        laserPosition = transform.GetChild(3).transform.position;
        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
        {
            overheat.Broil(heatPerUse * Time.deltaTime);

            if (!isOn) {
                inst = Instantiate(laser, laserPosition + gameObject.transform.forward, Quaternion.identity);
                isOn = true;
            }
            RaycastHit hit;
            Debug.DrawRay(laserPosition, transform.forward * magnitude, Color.green, .1f);

            line.enabled = true;
            inst.transform.position = laserPosition + transform.forward;
            //laserSpawn.transform.position = laserPosition + gameObject.transform.forward;
            //laserSpawn.Play();
            if (Physics.Raycast(laserPosition, transform.forward, out hit, magnitude)) {    

                enemy = hit.collider.gameObject;
                inst.transform.GetChild(1).transform.position = laserPosition + transform.forward;
                if (hit.collider != null && hit.collider.gameObject.tag != "Fireball" && hit.collider.gameObject.tag != "Acid" && hit.collider.tag != "Virus" && hit.collider.tag != "Vines" && hit.collider.tag != "Punch Hitbox")
                {
                    line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, hit.distance, 1f));
                    if (enemy != null)
                    {
                        Health enemyHealth = enemy.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            //laserHit.Play();
                            //laserHit.transform.position = hit.point;
                            inst.transform.GetChild(0).transform.position = LaserEffect(laserPosition, Vector3.MoveTowards(inst.transform.GetChild(0).transform.position, hit.point, .5f), hit.point);
                            inst.transform.GetChild(2).gameObject.SetActive(true);
                            inst.transform.GetChild(2).transform.position = hit.point;
                            //laserBeam.transform.position = LaserEffect(laserPosition + gameObject.transform.forward, Vector3.MoveTowards(laserBeam.transform.position, hit.point, .5f), hit.point);
                            enemyHealth.TakeDamage(laserDamage * Time.deltaTime);
                        }
                        else
                        {
                            inst.transform.GetChild(2).gameObject.SetActive(false);
                            inst.transform.GetChild(0).transform.position = laserPosition + transform.forward;
                            //laserBeam.transform.position = laserPosition;
                            //laserHit.Stop();
                            //laserBeam.Stop();
                        }
                    }
                }
            }
        }
        else {
            isOn = false;
            Destroy(inst);
            //laserBeam.transform.position = laserPosition + gameObject.transform.forward;
           // laserSpawn.transform.position = laserPosition + gameObject.transform.forward;
            line.enabled = false;
           // laserSpawn.Stop();
           // laserBeam.Stop();
           // laserHit.Stop();
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
    private void OnDestroy()
    {
        Destroy(inst);
        //Destroy(laserSpawn);
        //Destroy(laserBeam);
        //Destroy(laserHit);
    }
}
