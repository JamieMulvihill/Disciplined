using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phil's branch test.

public class Laser : MonoBehaviour
{
    [SerializeField] private float magnitude;
    [SerializeField] private float radius;
    LineRenderer line;
    [SerializeField] private string playerFire;
    [SerializeField] private PlayerController pc;








    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (pc.isCaptured) return;



        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f) {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * magnitude, Color.green, .1f);

            if (Physics.Raycast(transform.position, transform.forward, out hit, magnitude)){

                if (hit.collider != null) {

                    line.enabled = true;
                    line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, hit.distance, 1f));
                    print("Collided");
                    GameObject enemy = hit.collider.gameObject;
                    if (enemy != null)
                    {
                        print("Hit GameObject");
                        // Destroy(enemy);
                    }
                }
            }
            else{

                line.enabled = true;
                line.transform.localScale = new Vector3(radius, radius, Mathf.Lerp(0, magnitude, 1f));
            }
        }
        else {
            line.enabled = false;
        }
    }
}
