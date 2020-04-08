using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public string triggerButton;

    public GameObject icePrefab;
    private GameObject ice;


    // Start is called before the first frame update
    void Start()
    {
        triggerButton += GetComponent<Scientist>().controllerIndex.ToString();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f && !ice && GetComponent<Scientist>().isCaptured == false)
        {
            Vector3 SpawnPoint = transform.position;

            ice = Instantiate(icePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));

        }
    }

}
