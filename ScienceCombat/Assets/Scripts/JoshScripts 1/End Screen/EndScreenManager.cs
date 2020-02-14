using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements = new GameObject[4];
    //[SerializeField] private Animation[] uiAnims;

    void Start()
    {
        StartCoroutine(StatsOnScreen());
    }


    void Update()
    {
        
    }

    IEnumerator StatsOnScreen()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].GetComponent<Animator>().SetTrigger("Animate");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
