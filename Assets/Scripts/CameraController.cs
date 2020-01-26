using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float sens = 0.2f;
    [SerializeField, Range(0, 1)] private float dampSpeed = 0.1f;
    [SerializeField, Range(0, 1)] private float turnSpeed = 0.2f;
    [SerializeField] private KeyCode key = KeyCode.Mouse1;
    [SerializeField] private Vector2 minMax = Vector2.zero;

    private Transform target = null;
    private bool isDragging = false;
    private Vector3 clickedPosition = Vector3.zero;
    private Vector3 draggingPosition = Vector3.zero;
    private Vector3 finalPosition = Vector3.zero;
    private Vector3 smoothVelocity = Vector3.zero;

    private Plane plane = new Plane();
    public Camera Cam { get; private set; }
    private Transform pivot = null;

    private float yaw = 0;
    private float pitch = 0;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
        Cam = Camera.main;
        pivot = transform.GetChild(0);

        yaw = transform.eulerAngles.y;
        pitch = pivot.localEulerAngles.x;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(yaw, Vector3.up), turnSpeed);
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.AngleAxis(pitch, Vector3.right), turnSpeed);

        transform.position = Vector3.SmoothDamp(transform.position, GetPos(), ref smoothVelocity, dampSpeed);
    }

    private Vector3 GetPos()
    {
        if (Manager.InPhotoMode)
        {
            if (target != null) return target.position;
            else return Vector3.zero;
        }
        else return finalPosition;
    }

    public void HandlePosition()
    {
        if (Input.GetKeyDown(key))
        {
            isDragging = true;

            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter)) clickedPosition = ray.GetPoint(enter);
        }
        else if (Input.GetKeyUp(key))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter))
            {
                draggingPosition = ray.GetPoint(enter);
                finalPosition = transform.position + clickedPosition - draggingPosition;
            }
        }
    }

    public void HandleRotation()
    {
        if (Input.GetKeyDown(key))
        {
            isDragging = true;

            clickedPosition = Input.mousePosition;
        }
        else if (Input.GetKeyUp(key))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            draggingPosition = Input.mousePosition;

            Vector3 diff = clickedPosition - draggingPosition;
            clickedPosition = draggingPosition;

            yaw -= diff.x * sens;
            yaw %= 360;
            pitch += diff.y * sens;

            pitch = Mathf.Clamp(pitch, minMax.x, minMax.y);
        }
    }

    public void SetTarget(Transform newTarget) => target = newTarget;

    public void SetDefault()
    {
        yaw = 45;
        pitch = 45;
        finalPosition = Vector3.zero;
    }
}
