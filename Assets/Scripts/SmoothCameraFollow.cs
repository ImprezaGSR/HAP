using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform backTarget;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void setTarget(Transform newTarget){
        target = newTarget;
    }

    public void GetBackCamera(Transform Camera){
        if(backTarget == Camera){
            target = backTarget;
        }
    }

    public void SetBackCamera(Transform Camera){
        backTarget = Camera;
    }

    public void ZoomOut(float yAmount, float zAmount){
        offset = offset + new Vector3(0, yAmount, zAmount);
    }
}
