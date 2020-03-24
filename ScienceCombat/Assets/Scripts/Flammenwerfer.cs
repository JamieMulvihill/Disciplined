using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammenwerfer : ProjectileLauncher
{
    // Start is called before the first frame update
    //[SerializeField] private GameObject projectile;     // projectile which will be launched
    //[SerializeField] private float upOffset;            // spawn position offset (up)
    //[SerializeField] private float forwardOffset;       // spawn position offset (forward)
    //[SerializeField] private float verticalVelocity;    // vertical launch velocity (up)
    //[SerializeField] private float horizontalVelocity;  // horizontal launch velocity (forward)
    //[SerializeField] private float fireRate = 0.25f;    // 
    //[SerializeField] private float momentumScalar = 0.0f;         // FlameVelocityRetention - carries velocity from player to fireball; 1 = full retention

    [SerializeField] GameObject flameVisual;
    [SerializeField] float updateDelay = 1f;
    [Header("Cooling down delay")]
    [SerializeField] protected float timeToChill = 0f;
    IEnumerator FlameTimer()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(updateDelay);
            overheat.timeToChill = timeToChill; // allows value to be adjusted in editor during play
            if (Mathf.Abs(Input.GetAxis(triggerButton)) > 0.01f && GetComponent<Scientist>().isCaptured == false && overheat.GetOverheated() == false)
            {
                flameVisual.SetActive(true);
            }
            else
            {
                flameVisual.SetActive(false);
            }
        }
    }

    void Awake()
    {
        StartCoroutine(FlameTimer());
        overheat = new Overheat();
    }
}
