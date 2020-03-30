using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private string triggerButton;
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    private bool lastButtonState;
    private bool buttonState;

    private float lastTime;
    public Animator anim;

    void Start()
    {
        lastTime = Time.time;
        triggerButton += GetComponentInParent<Scientist>().controllerIndex.ToString();
        anim.SetTrigger("slap");
    }

    // Update is called once per frame
    void Update()
    {
        lastButtonState = buttonState;
        buttonState = Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f;
    }

    private void OnTriggerStay(Collider other)
    {
        // calling GetComponent every frame...
        Scientist scientist = other.gameObject.GetComponent<Scientist>();
        if (scientist != null)
        {
            if (!scientist.isCaptured)
            {
                if (buttonState && !lastButtonState)
                {
                    if (Time.time > lastTime + cooldown)
                    {
                        Debug.Log("Hit Player");
                        Debug.Log(other.gameObject);
                        other.gameObject.GetComponent<Health>().TakeDamage(damage);
                        lastTime = Time.time;
                    }
                }
            }
        }
    }
}