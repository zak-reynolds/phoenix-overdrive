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
    private float impulse = 0;

    [SerializeField]
    private float topSpeed;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Light[] headlamps;
    [SerializeField]
    private float headlampMaxIntensity = 3.75f;
    private float targetHeadlampIntensity = 3.75f;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    public void SetInput(VehicleInput input)
    {
        this.input = input;
    }

    public void AddImpulse(float amount)
    {
        impulse = amount;
    }

    void Update()
    {
        var rotationSpeedScaled = rotationSpeed * Time.deltaTime;
        var topSpeedScaled = topSpeed * Time.deltaTime;

        targetRotation =
            Quaternion.AngleAxis(
                (transform.rotation.eulerAngles.y + rotationSpeedScaled * input.Steering) % 360,
                Vector3.up);

        velocity = transform.forward *
            topSpeedScaled * input.Throttle;
        targetPosition += velocity;

        targetHeadlampIntensity = input.Throttle > 0.85f ? headlampMaxIntensity : 0;
        foreach (Light l in headlamps) 
        {
            l.intensity = Mathf.Lerp(l.intensity, targetHeadlampIntensity, Time.deltaTime * 25);
        }
    }

	void FixedUpdate () {
        rb.MoveRotation(targetRotation);
        rb.AddForce(transform.forward * velocity.magnitude * 30);
        if (impulse > 0)
        {
            rb.AddRelativeForce(Vector3.forward * impulse, ForceMode.Impulse);
            impulse = 0;
        }
	}
}
