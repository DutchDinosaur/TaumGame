using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform TrackingPosition;
    [SerializeField]
    private Vector3 CamPosOffset = new Vector3(0, -14.24f, -13.7f);

    private Vector3 camVelocity;
    [SerializeField]
    private float smoothTime = .5f;

    void Update()
    {
        if (TrackingPosition == null)
        {
            return;
        }

        Vector3 desiredPos = TrackingPosition.transform.position + CamPosOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(GetComponent<Camera>().transform.position, desiredPos, ref camVelocity, smoothTime);
        GetComponent<Camera>().transform.position = smoothedPosition;

        GetComponent<Camera>().transform.LookAt(TrackingPosition.transform);
    }
}