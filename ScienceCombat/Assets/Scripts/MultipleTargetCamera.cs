using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    private Vector3 velocity;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float cameraNearClamp = 7f;
    public float zoomLimiter;
    private Camera cam;
    public GameObject centre;
    private void Start(){
        cam = GetComponent<Camera>();
    }

    private void LateUpdate(){
        //if the list of transofrms is empty return
        if (targets.Count == 0) {
            return;
        }
        // Move and Zoom functions to control camera
        Move();
        Zoom();
    }

    void Move(){
        // call the get centrepoint function and store the result, add the offset value and store new postion
        Vector3 centrePoint = GetCentrePoint();
        centre.transform.position = centrePoint; ///***** centre is used to debug the cameras cetre point with a game object, nothing else****
        float standardOffset = -30f;
        if (centrePoint.z < -cameraNearClamp) {
            offset.z = standardOffset + (-centrePoint.z - cameraNearClamp);
        }
        Vector3 newPosition = centrePoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom() {

        Debug.Log(GetGreatestDistance());
        // set the cameras field of view and zoom values based of the Lerp calculations of the greastest distance points divided by the zoom limiter 
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance() {

        // create a bounding box around the first elemnt of the list of transforms
        // Use the encapulate function to add the remaining transforms to the bounding box
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        // get the distnce of the two furthest away points of the box using pythageros
        float distance = (bounds.size.z * bounds.size.z) + (bounds.size.x * bounds.size.x);
        return Mathf.Sqrt(distance);
    }

    Vector3 GetCentrePoint() {

        if (targets.Count == 1) {
            return targets[0].position;
        }

        // create a bounding box around the first elemnt of the list of transforms
        // Use the encapulate function to add the remaining transforms to the bounding box, this will adjust the centre point based on the indiviual transforms
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
