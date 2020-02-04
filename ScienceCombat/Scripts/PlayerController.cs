using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float LF = 0;
   
    public Rigidbody rb {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    public RaycastHit[] HIT;
    public GameObject Current;
    public GameObject item;
    public bool pickUp = false;
    public string playerHOR, PlayerVer,PlayerInter;

    //animator controller
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 current = rb.velocity;
        if (Mathf.Abs(Input.GetAxis(playerHOR)) > 0.01f || Mathf.Abs(Input.GetAxis(PlayerVer)) > 0.01f)
        {
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.GetAxis(playerHOR), 0, Input.GetAxis(PlayerVer)), transform.up), 0.3f);


            //animator perameter
            anim.SetFloat("speed", 1);
        }
        else
        {
            //animator perameter
            anim.SetFloat("speed", 0);
        }
        rb.velocity = transform.forward *
            new Vector3(
                Mathf.Abs(Input.GetAxis(playerHOR)), 0,
                Mathf.Abs(Input.GetAxis(PlayerVer))).magnitude;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) * 10;
        rb.velocity += new Vector3(0, 0, 0);


        LF = Input.GetAxis(playerHOR);

        //raycast
        HIT = Physics.RaycastAll(transform.position, transform.forward, 2);
        //rayCast part
       
        if (HIT.Length > 0)
        {
            foreach (RaycastHit hit in HIT)
            {
                if (!pickUp)
                {     if (hit.collider.GetComponent<Pickuper>() != null)
                    {
                        if (Input.GetButton(PlayerInter))
                        {
                            item = hit.collider.gameObject;
                            hit.collider.GetComponent<Pickuper>().AddObject(gameObject);
                            Current = hit.collider.gameObject; //visiblility in engin
                            pickUp = true;
                            print("Picked Up bollean Changed");
                            break;
                        }
                    }
                }
            }

        }
        else
        {
            //if (Current!=null&&Current.GetComponent<Pickuper>()) {
            //    Current.GetComponent<Pickuper>().RayHIT = false;
            //    Current = null;
            //}
        }


        if (Input.GetButton("PickUp2"))
        {
            print("PickUp2");
        }
        if (Input.GetButton("PickUp"))
        {
            print("PickUp");

        }
        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f) {
           
             print("god help me");
             LaunchProjectile launchProjectile;
             launchProjectile = gameObject.GetComponentInParent<LaunchProjectile>();
             launchProjectile.Launch(gameObject.transform.forward, new Vector3(rb.transform.position.x + (rb.transform.forward.x), rb.transform.position.y, rb.transform.position.z + (rb.transform.forward.z)));
        }
    }
}
