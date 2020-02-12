using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFlames : MonoBehaviour
{
    [SerializeField] GameObject flames;
    [SerializeField] float updateDelay = 1f;

    // Update is called once per frame
    public void Show(bool isFiring)
    {
        flames.SetActive(isFiring);
    }
}
