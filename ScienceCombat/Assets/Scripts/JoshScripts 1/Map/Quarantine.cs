using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarantine : MonoBehaviour
{
    public bool raiseWalls;
    public bool up;
    public bool gasSent;

    private int speed;
    [SerializeField] private int wallNum;

    private Vector3 peak;
    private Vector3 trough;
    private Vector3 gasCheck;

    private Manager managerScript;
    private QuarantineManager QMScript;


    private void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        QMScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuarantineManager>();
        peak = new Vector3(0, 2.38f, 0);
        trough = new Vector3(0, 0, 0);
        gasCheck = new Vector3(0, 2.35f, 0);
        raiseWalls = false;
        gasSent = false;
    }

    private void Update()
    {
        speed = managerScript.quarantineSpeed;

        if (this.gameObject.transform.position.y == trough.y)
        { 
            gasSent = false;
        }

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
        if (this.gameObject.transform.position.y == peak.y && gasSent == false)
        {
            gasSent = true;
            if (wallNum < 5)
            {
                QMScript.Cleanse(wallNum);
            }
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, peak, speed * Time.deltaTime);
        }
    }

    void LowerWalls()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, trough, speed * Time.deltaTime);
    }
}
