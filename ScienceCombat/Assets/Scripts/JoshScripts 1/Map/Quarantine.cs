using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarantine : MonoBehaviour
{
    public bool raiseWalls;
    public bool up;

    private int speed;

    private Vector3 peak;
    private Vector3 trough;

    private Manager managerScript;
    private QuarantineManager QMScript;

    private void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        QMScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuarantineManager>();
        peak = new Vector3(0, 2.38f, 0);
        trough = new Vector3(0, 0, 0);
        raiseWalls = false;
    }

    private void Update()
    {
        speed = managerScript.quarantineSpeed;

        if (this.gameObject.transform.position.y == trough.y)
            up = false;
        else
            up = true;

        if (raiseWalls == true)
        {
            RaiseWalls();
        }
        else
        {
            LowerWalls();
        }
    }

    void RaiseWalls()
    {
        if (this.gameObject.transform.position.y == peak.y)
        {
            //release gas
        }
        else
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, peak, speed * Time.deltaTime);
        }
    }

    void LowerWalls()
    {
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, trough, speed * Time.deltaTime);
    }
}
