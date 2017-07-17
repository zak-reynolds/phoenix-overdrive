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

    [Header("Pheonix")]
    private bool isPheonix = false;

    [Header("Damage")]
    [SerializeField]
    private float damageImpulseFloor = 80;
    [SerializeField]
    private float damageDebounce = 0.5f;
    private float damageDebounceTimer = 0;
    [SerializeField]
    private float outOfBoundsCooldown = 1;
    private float outOfBoundsTimer = 1;
    private bool inBounds = true;

    [Header("Visual")]
    [SerializeField]
    private Light[] headlamps;
    [SerializeField]
    private float headlampMaxIntensity = 3.75f;
    private float targetHeadlampIntensity = 3.75f;

    [Header("Sounds")]
    public AudioClip pickup;
    public AudioClip flame;
    public AudioClip sizzle;
    public AudioClip rev;
    public AudioClip revEnd;
    public AudioClip squeal;
    public AudioClip collide;
    public AudioClip die;
    public AudioClip collisionSmall;
    public AudioClip collisionBig;
    [SerializeField]
    private AudioSource sizzleSource;
    [SerializeField]
    private AudioSource pheonixSource;

    private int hp = 0;
    private bool carryingKey = false;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
        sizzleSource = GetComponent<AudioSource>();
	}
	
    public void SetInput(VehicleInput input)
    {
        this.input = input;
    }

    public void AddImpulse(float amount)
    {
        impulse = amount;

        sizzleSource.PlayOneShot(squeal);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Parts"))
        {
            if (hp <= 5) hp++;

            sizzleSource.PlayOneShot(pickup);
        }
        if (other.tag.Equals("Key") &&
            tag.Equals("Player"))
        {
            carryingKey = true;

            sizzleSource.PlayOneShot(pickup);
        }
        if (other.tag.Equals("Lock") &&
            carryingKey)
        {
            hp = 0;
        }
        if (other.tag.Equals("Road"))
        {
            outOfBoundsTimer = outOfBoundsCooldown;
            inBounds = true;
            sizzleSource.Stop();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Road"))
        {
            outOfBoundsTimer = outOfBoundsCooldown;
            inBounds = false;
            sizzleSource.Play();
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("Pheonix") &&
            !tag.Equals("Player"))
        {
            hp--;
        }
        if (other.tag.Equals("Road"))
        {
            outOfBoundsTimer = outOfBoundsCooldown;
            inBounds = true;
            sizzleSource.Stop();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.impulse.magnitude > damageImpulseFloor &&
            damageDebounceTimer == 0)
        {
            DamageTaken();
            damageDebounceTimer = damageDebounce;
            sizzleSource.PlayOneShot(col.impulse.magnitude > damageImpulseFloor + 30 ? collisionBig : collisionSmall);
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
        outOfBoundsTimer -= Time.deltaTime;
        if (!inBounds && outOfBoundsTimer < 0)
        {
            outOfBoundsTimer = outOfBoundsCooldown;
            hp--;
        }

        isPheonix = input.IsPheonix;
        if (isPheonix && !pheonixSource.isPlaying) pheonixSource.Play();
        if (!isPheonix && pheonixSource.isPlaying) pheonixSource.Stop();
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

    public bool IsCarryingKey()
    {
        return carryingKey;
    }

    public bool IsPheonix()
    {
        return isPheonix;
    }

    public int GetHP()
    {
        return hp;
    }
    public int DecrementHP()
    {
        return --hp;
    }
    public bool InBounds()
    {
        return inBounds;
    }
}
