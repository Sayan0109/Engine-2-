using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CameraFollow : MonoBehaviour
{
    public Transform target;       // сюда перетащи корабль
    public Vector3 offset = new Vector3(0f, 15f, -10f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}