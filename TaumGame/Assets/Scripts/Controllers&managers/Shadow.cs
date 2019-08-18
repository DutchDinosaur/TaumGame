using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    private Transform TrackingPosition;

    [SerializeField]
    private float maxShadowDistance = 50;
    [SerializeField]
    private float shadowZOffset = -.4f;
    [SerializeField]
    private float shadowYOffset = -.6f;
    private float distance;

    void LateUpdate()
    {
        RaycastHit hit;
        Ray ShadowRay = new Ray(new Vector3(TrackingPosition.position.x, TrackingPosition.position.y + shadowYOffset, TrackingPosition.position.z),new Vector3(0,0,1));
        distance = maxShadowDistance;

        if (Physics.Raycast(ShadowRay, out hit)){
            distance = hit.distance;
            Vector3 normal = hit.normal * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(normal.y, -normal.x,0));
        }

        transform.position = new Vector3(TrackingPosition.position.x,TrackingPosition.position.y + shadowYOffset, TrackingPosition.position.z + distance + shadowZOffset);
    }
}
