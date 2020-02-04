using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private List<PlayerController> players = new List<PlayerController>();
    private List<float> reset = new List<float>();
    float height = 10;
    bool hit;


    private void Start()
    {
        height = transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.max.y*2f;
        StartCoroutine(deleteSelf());
    }

    private void Update()
    {
        transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale, Vector3.one, 0.04f);
    }

    IEnumerator deleteSelf() {
        yield return new WaitForSeconds(8);
        if (!hit){
            int count = 0;
            foreach (var playerController in players){
                playerController.Speed = reset[count++];
                playerController.GetComponent<Rigidbody>().isKinematic = false;
            }

            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.tag != "Biologist" && collision.gameObject.tag != "Ground" &&
            collision.gameObject.GetComponent<PlayerController>() != null &&!collision.gameObject.GetComponent<PlayerController>().isCaptured) {

            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            players.Add(playerController);
            reset.Add(playerController.Speed);
            StartCoroutine(DEATH(playerController, playerController.Speed));
        }
    }
    IEnumerator DEATH(PlayerController capturedPlayer, float speed) {
        hit = true;
        capturedPlayer.Speed = 0;
        capturedPlayer.GetComponent<Rigidbody>().isKinematic = true;
        capturedPlayer.gameObject.transform.position = transform.position + transform.up * height;
        height += capturedPlayer.GetComponent<MeshFilter>().mesh.bounds.max.y*2;
        yield return new WaitForSeconds(5);
        capturedPlayer.GetComponent<Rigidbody>().isKinematic = false;
        capturedPlayer.Speed = speed;
        int count = 0;
        foreach (var playerController in players){
            playerController.Speed = reset[count++];
            playerController.GetComponent<Rigidbody>().isKinematic = false;
        }

        Destroy(gameObject);
    }


}
