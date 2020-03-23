using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour{

    [Header("Overheat values")]
    public Overheat overheat;
    [SerializeField] protected float heatPerUse = 0f;
    [SerializeField] protected float cooloffPerSecond = 0f;
    [SerializeField] protected float chillThreshold = 100f;
    [Header("Other Settings")]
    public string playerAlt1;
    [SerializeField] private float meshSize = 1;
    [SerializeField] private Scientist scientist;

    private void Start()
    {
        overheat = new Overheat();
        playerAlt1 += GetComponent<Scientist>().controllerIndex.ToString();
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
    }

    void Update(){
       overheat.chillThreshold = chillThreshold;
       overheat.Chill(cooloffPerSecond * Time.deltaTime);
       if (scientist.isCaptured) return;

       if (Input.GetButtonDown(playerAlt1) && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
       {
           overheat.Broil(heatPerUse); // could very easily make it use more heat as the distance dashed increases.

           RaycastHit hit;
           Debug.DrawRay(transform.position, transform.forward * 10, Color.green, 3f);
            Vector3 RayPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (Physics.Raycast(RayPos, transform.forward, out hit, 10, ~(1 << 8)) && hit.collider != null/* && hit.collider.gameObject.tag != "Quarentine" && hit.collider.gameObject.tag != "Punch Hitbox"*/)
            {
               transform.position = new Vector3(hit.point.x - (transform.forward.x), 0, hit.point.z - (transform.forward.z));
            }
           else
           {
               transform.position += transform.forward * 10;
           }
       }

    }

    public void DisignateController(int controllerIndex)
    {
        playerAlt1 = "AltMove" + controllerIndex.ToString();
    }
}
