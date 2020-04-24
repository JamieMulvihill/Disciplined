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
    public float cameraNearClamp = 14f;
    public float zoomLimiter;
    private Camera cam;
    public float defaultDistance = 15;
    Vector3 defaultTransform;
    private void Start(){
        defaultTransform = new Vector3(1.75f, .1f, 0);
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

        // get the centre point function and store the result, add the offset value and store new postion
        Vector3 centrePoint = GetCentrePoint();
        float standardOffset = -24f;

        // Check if the camera has moved too far in the Z axis and adjust using clamp value
        if (centrePoint.z < -cameraNearClamp) {
            offset.z = standardOffset + (-centrePoint.z - cameraNearClamp);
        }
        
        // Move the camera to follow the centre point of the players in the x axis
        offset.x = centrePoint.x;
        
        // alter the cameras position to the new position, using Smoothdamp to change gradually over time
        Vector3 newPosition = centrePoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom() {

        // set the cameras field of view and zoom values based off the Lerp calculations
        // of the greastest distance points divided by the zoom limiter 
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance() {

        // if there is only one player, set the distance to a the fixed value.
        if (targets.Count <= 1)
            return defaultDistance;

        
        // create a bounding box around the first elemnt of the list of transforms
        // Use the encapulate function to add the remaining transforms to the bounding box
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        // get the distnce of the two furthest away points of the box 
        float distance = (bounds.size.z * bounds.size.z) + (bounds.size.x * bounds.size.x);
        return Mathf.Sqrt(distance);
    }

    Vector3 GetCentrePoint()
    {
        //if there is only 1 player, use that position as the centre
        if (targets.Count == 1) {
            return targets[0].position;
        }
        if (targets.Count == 0)
        {
            return defaultTransform;
        }
        // create a bounding box around the first elemnt of the list of transforms
        // Use the encapulate function to add the remaining transforms to the bounding box,
        //this will adjust the centre point based on the indiviual transforms
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        return new Vector3(bounds.center.x, 0f, bounds.center.z);
    }

    public void RemoveDeadPlayer(Transform deadPlayerTransform) {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == deadPlayerTransform) {
                targets.Remove(targets[i]);
            }
        }
        GetCentrePoint();
    }

    public void AddPlayer(Transform playerTransform) {
        targets.Add(playerTransform);
    }
}
