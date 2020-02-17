using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Play()
    {
        SceneManager.LoadScene("TestMap");
        print("Play");
    }

    private void Exit()
    {
        Application.Quit();
    }


}
