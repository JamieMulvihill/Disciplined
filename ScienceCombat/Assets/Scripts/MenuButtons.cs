using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //print("Play");
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    private IEnumerator Wait()
    {
        print("waiting");
        yield return new WaitForSeconds(29f);
        print("waited");
        canvas.GetComponent<Canvas>().enabled = true;
        
    }


}
