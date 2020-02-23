using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject death;
    public string playerHOR, PlayerVer;
    public bool isCaptured = false;
    [SerializeField] private Health healthManager;
    [SerializeField] private SpriteRenderer healthSprite;
    public int controllerIndex;

    [SerializeField] private GameObject [] scientistPrefabs;

    private GameObject manager;

    void Start()
    {
        playerHOR += GetComponent<Scientist>().controllerIndex.ToString();
        PlayerVer += GetComponent<Scientist>().controllerIndex.ToString();
        manager = GameObject.FindGameObjectWithTag("Manager");
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
    }

    void FixedUpdate()
    {
        if (isCaptured) return;

        Vector3 joystickDirection = new Vector3(Input.GetAxis(playerHOR), 0, Input.GetAxis(PlayerVer));

        // If the stick is not at rest.
        if (Mathf.Abs(Input.GetAxis(playerHOR)) > 0.01f || Mathf.Abs(Input.GetAxis(PlayerVer)) > 0.01f)
        {
            // Setting the rotation of the player to turn towards the direction of the joystick.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(joystickDirection, transform.up), rotationSpeed);
        }
        // Setting the velocity of the player
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * new Vector3(Mathf.Abs(joystickDirection.x), 0, Mathf.Abs(joystickDirection.z)).magnitude * speed;
    }

    void Update()
    {
        healthSprite.color = new Color(healthManager.redValue / 255, healthManager.greenGuiValue / 255, 0 / 255, 1f);
        
        if (healthManager.health <= 0) {
            Camera gameCam = Camera.main;
            MultipleTargetCamera multipleTargetCamera = gameCam.GetComponent<MultipleTargetCamera>();
            multipleTargetCamera.RemoveDeadPlayer(gameObject.transform);

            

            switch(gameObject.tag)
            {
                case "Biologist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(0);
                    break;
                case "Chemist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(1);
                    break;
                case "Engineer":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(2);
                    break;
                case "Physicist":
                    manager.GetComponent<PipeLineWaypoints>().EnqueueClone(3);
                    break;
            }

            Destroy(gameObject);
        }
    }

    public void DisignateController(int controllerIndex)
    {
        this.controllerIndex = controllerIndex;
        playerHOR = "Horizontal" + controllerIndex.ToString();
        PlayerVer = "Vertical" + controllerIndex.ToString();
    }
    private void OnDisable()
    {
        Instantiate(death, transform.position, transform.rotation);
    }
}
