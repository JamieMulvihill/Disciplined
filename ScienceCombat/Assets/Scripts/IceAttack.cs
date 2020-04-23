using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    
    [Header("Overheat values")]
    public Overheat overheat;
    [SerializeField] protected float heatPerUse = 0f;
    [SerializeField] protected float cooloffPerSecond = 0f;
    [SerializeField] protected float chillThreshold = 100f;
    [Header("Other Settings")]
    public string triggerButton;
    public GameObject icePrefab;
    private GameObject ice;


    // Start is called before the first frame update
    void Start()
    {
        overheat = new Overheat();
        triggerButton += GetComponent<Scientist>().controllerIndex.ToString();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        overheat.chillThreshold = chillThreshold;
        overheat.Chill(cooloffPerSecond * Time.deltaTime);

        if (Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
        {
            overheat.Broil(heatPerUse);

            Vector3 SpawnPoint = transform.position;

            ice = Instantiate(icePrefab, SpawnPoint, Quaternion.LookRotation(transform.forward, transform.up));

        }
    }

}
