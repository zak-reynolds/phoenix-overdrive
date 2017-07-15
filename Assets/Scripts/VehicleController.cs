using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class VehicleController : MonoBehaviour {
    private Vehicle vehicle;

    [SerializeField]
    private Facade vehicleFacade;
    [SerializeField]
    private VehicleCamRig camRig;

    private float wobbleIntensity = 0;
    private float throttle = 0;
    private float steeringBoostIntensity = 0;

    [SerializeField]
    private float throttleDecay = 0.5f;
    [SerializeField]
    private float throttleImpulse = 10f;

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
            camRig.SetFOV(75);
            camRig.ActivateZoomBoom();
            vehicle.AddImpulse(throttleImpulse);
        }
        if (Input.GetButtonUp("Gas"))
        {
            wobbleIntensity = 16;
            camRig.SetFOV(65);
            camRig.DeactivateZoomBoom();
        }
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
            Input.GetAxis("Horizontal") * (1 + steeringBoostIntensity));
        vehicle.SetInput(input);

        if (vehicleFacade) vehicleFacade.SetRotationOffset(pitchWobbleFunc(wobbleIntensity, 16));
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
