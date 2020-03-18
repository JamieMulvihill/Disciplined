using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Canvas can;
    public GameObject vp;

    // Start is called before the first frame update
    void Start()
    {
        can.enabled = false;
        StartCoroutine(WaitSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        //player skips
        if (Input.GetKeyDown($"joystick button 1"))
        {
            Enable();
        }

    }

    private void Play()
    {
        if(can == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Exit()
    { 
        if(can == true)
        {
            Application.Quit();
        }
    }

    private IEnumerator WaitSeconds()
    {
        
        yield return new WaitForSeconds(30f);
        Enable();
    }

    private void Enable()
    {
        can.enabled = true;
        Destroy(vp);
    }


}
