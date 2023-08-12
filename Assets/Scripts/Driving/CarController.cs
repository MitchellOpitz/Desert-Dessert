using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    Rigidbody2D RB;

    Vector2 MovementInput;

    [Header("Acceleration")]
    [SerializeField]
    float AccelerationSpeed;

    [Header("Steering")]
    [SerializeField]
    float SteeringSpeed;

    [SerializeField]
    float DriftFactor;
    float RotationAngle = 0;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Accelerate();
        Steer();

        KillHorizontalVelocity();
    }

    public void SetMovementInput(InputAction.CallbackContext Context)
    {
        MovementInput = Context.ReadValue<Vector2>();
    }

    void Accelerate()
    {
        RB.AddForce(transform.up * MovementInput.y * AccelerationSpeed);
    }

    void Steer()
    {
        float MinimumSpeedBeforeAllowingTurning = RB.velocity.magnitude / 8;
        MinimumSpeedBeforeAllowingTurning = Mathf.Clamp01(MinimumSpeedBeforeAllowingTurning);

        RotationAngle -= MovementInput.x * SteeringSpeed * MinimumSpeedBeforeAllowingTurning;
        RB.MoveRotation(RotationAngle);
    }

    void KillHorizontalVelocity()
    { // To simply add friction
        Vector2 ForwardVelocity = transform.up * Vector2.Dot(RB.velocity, transform.up);
        Vector2 HorizontalVelocity = transform.right * Vector2.Dot(RB.velocity, transform.right);

        RB.velocity = ForwardVelocity + HorizontalVelocity * DriftFactor;
    }
}
