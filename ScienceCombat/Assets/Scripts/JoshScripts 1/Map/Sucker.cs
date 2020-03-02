using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucker : MonoBehaviour
{
    [SerializeField] private GameObject[] zones = new GameObject[5];
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject homebase;
    private GameObject target;

    private float speed;
    public int zone;

    private bool playerCaught;

    private Vector3 offset;

    private QuarantineManager QMScript;

    void Start()
    {
        QMScript = manager.GetComponent<QuarantineManager>();
        offset = new Vector3(0, 4, 0);
    }


    void Update()
    {
        if(QMScript.playersToKill.Count != 0)
        {
            target = QMScript.playersToKill.Peek();
        }
        else
        {
            target = null;
        }
        MoveSucker();
    }

    private void MoveSucker()
    {
        if (target != null)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, target.transform.position + offset, speed * Time.deltaTime);
            if (this.gameObject.transform.position == target.transform.position + offset && playerCaught == false)
            {
                playerCaught = true;
                print("Player Caught");
                QMScript.playersToKill.Dequeue();
            }
        } else
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, homebase.transform.position, speed * Time.deltaTime);
        }
    }
}
