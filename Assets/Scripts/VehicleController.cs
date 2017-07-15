using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class VehicleController : MonoBehaviour {
    private Vehicle vehicle;

    private float wobbleIntensity = 0;
    private float throttle = 0;
    private float steeringBoostIntensity = 0;

    [SerializeField]
    private float throttleDecay = 0.5f;

    [SerializeField]
    private float steeringBoost = 1.5f;
    [SerializeField]
    private float steeringBoostDecay = 0.5f;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

	void Update () {
        if (Input.GetButtonDown("Gas"))
        {
            wobbleIntensity = 10;
            steeringBoostIntensity = steeringBoost;
        }
        if (Input.GetButtonUp("Gas")) wobbleIntensity = 16;
        wobbleIntensity = Mathf.Max(wobbleIntensity - Time.deltaTime * 30, 0);
        steeringBoostIntensity = Mathf.Max(steeringBoostIntensity - Time.deltaTime / steeringBoostDecay, 0);

        if (Input.GetButton("Gas"))
        {
            throttle = 1;
        }
        else
        {
            throttle = Mathf.Max(throttle - Time.deltaTime / throttleDecay, 0);
        }

        var input = new VehicleInput(
            throttle,
            Input.GetAxis("Horizontal") * (1 + steeringBoostIntensity),
            wobbleIntensity
            );
        vehicle.SetInput(input);
	}
}
