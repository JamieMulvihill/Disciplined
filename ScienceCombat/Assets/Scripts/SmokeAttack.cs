using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAttack : MonoBehaviour
{
    public string playerFire;

    public Smoke smokePrefab;
    private Smoke smoke;
    

    // Start is called before the first frame update
    void Start()
    {
        //DisignateController(gameObject.GetComponent<Scientist>().controllerIndex);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Mathf.Abs(Input.GetAxis(playerFire)) > 0.01f && !smoke)
        {
            Vector3 SpawnPoint = transform.position;

            smoke = Instantiate(smokePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));
            
        }


    }

    public void DisignateController(int controllerIndex)
    {
        playerFire = "AltMove" + controllerIndex.ToString();
    }

}
