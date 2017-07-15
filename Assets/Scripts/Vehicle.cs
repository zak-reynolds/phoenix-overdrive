using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour {

    private Rigidbody rb;

    private VehicleInput input;

    private Vector3 velocity = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 targetPosition = Vector3.zero;

    [SerializeField]
    private float topSpeed;
    [SerializeField]
    private float rotationSpeed;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    public void SetInput(VehicleInput input)
    {
        this.input = input;
    }

    void Update()
    {
        var rotationSpeedScaled = rotationSpeed * Time.deltaTime;
        var topSpeedScaled = topSpeed * Time.deltaTime;

        targetRotation =
            Quaternion.AngleAxis(
                (transform.rotation.eulerAngles.y + rotationSpeedScaled * input.Steering) % 360,
                Vector3.up) *
            pitchWobbleFunc(input.PitchWobble, 16);

        velocity = transform.forward *
            topSpeedScaled * input.Throttle;
        targetPosition += velocity;
    }

	void FixedUpdate () {
        rb.MoveRotation(targetRotation);
        rb.AddForce(transform.forward * velocity.magnitude);
	}

    private Quaternion pitchWobbleFunc(float amount, float rate)
    {
        if (Mathf.Approximately(amount, 0))
            return Quaternion.identity;
        return Quaternion.AngleAxis(
            amount * (Mathf.Sin(rate * Time.time) / 2 + 0.5f) - amount / 2,
            Vector3.left);
    }
}
