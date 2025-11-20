using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [Header("Movement Settings")]
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 5f;
    public float driftFactor = 0.98f;

    [Header("References")]
    public Joystick joystick;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

        if (joystick == null)
        {
            GameObject joystickObj = GameObject.Find("MoveJoystick");
            if (joystickObj != null)
                joystick = joystickObj.GetComponent<Joystick>();
        }
    }

    void FixedUpdate()
    {
        if (joystick == null) return;

        HandleMovement();   // <-- метод из интерфейса
        HandleRotation();   // <-- метод из интерфейса
    }

    public void HandleMovement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical);

        if (inputDir.magnitude > 0.1f)
        {
            moveDirection = inputDir.normalized;

            currentVelocity += moveDirection * acceleration * Time.fixedDeltaTime;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, maxSpeed);
        }
        else
        {
            currentVelocity *= driftFactor;
        }

        rb.velocity = currentVelocity;
    }

    public void HandleRotation()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
