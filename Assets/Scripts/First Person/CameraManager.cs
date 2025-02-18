using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;

    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        var targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }



    [Serializable]
    public struct MouseSensitivity
    {
        public float horizontal;
        public float vertical;
        public bool invertHorizontal;
        public bool invertVertical;
    }

    public struct CameraRotation
    {
        public float Pitch;
        public float Yaw;
    }

    [Serializable]
    public struct CameraAngle
    {
        public float min;
        public float max;
    }

}