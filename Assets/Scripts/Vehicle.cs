using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour {

    private Rigidbody rb = null;

    private VehicleInput input = new VehicleInput();

    private Vector3 velocity = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 targetPosition = Vector3.zero;
    private float impulse = 0;

    [Header("Performance")]
    [SerializeField]
    private float topSpeed = 2300;
    [SerializeField]
    private float rotationSpeed = 180;
    [SerializeField]
    private float drag = 10;
    [SerializeField]
    private float hpDrag = 0.75f;

    [Header("Damage")]
    [SerializeField]
    private float damageImpulseFloor = 80;
    [SerializeField]
    private float damageDebounce = 0.5f;
    private float damageDebounceTimer = 0;

    [Header("Visual")]
    [SerializeField]
    private Light[] headlamps;
    [SerializeField]
    private float headlampMaxIntensity = 3.75f;
    private float targetHeadlampIntensity = 3.75f;

    private int hp = 0;
    
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Parts"))
        {
            hp++;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.impulse.magnitude > damageImpulseFloor &&
            damageDebounceTimer == 0)
        {
            DamageTaken();
            damageDebounceTimer = damageDebounce;
        }
    }

    protected virtual void DamageTaken()
    {
        hp--;
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

        damageDebounceTimer = Mathf.Max(damageDebounceTimer - Time.deltaTime, 0);
    }

	void FixedUpdate () {
        rb.MoveRotation(targetRotation);
        rb.AddForce(transform.forward * velocity.magnitude * 30);
        if (impulse > 0)
        {
            rb.AddRelativeForce(Vector3.forward * impulse, ForceMode.Impulse);
            impulse = 0;
        }
        rb.drag = drag + hpDrag * hp;
	}

    public bool IsDoomed()
    {
        return hp < 0;
    }
}
