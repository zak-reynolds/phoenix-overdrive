using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class VehicleController : MonoBehaviour {
    protected Vehicle vehicle;

    [Header("Component Links")]
    [SerializeField]
    protected Facade vehicleFacade;

    [Header("Wobble")]
    [SerializeField]
    protected float gasOnWobble = 10;
    [SerializeField]
    protected float gasOffWobble = 16;

    protected float wobbleIntensity = 0;
    protected float throttle = 0;
    protected float steeringBoostIntensity = 0;

    protected virtual void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

	protected virtual void Update () {
        wobbleIntensity = Mathf.Max(wobbleIntensity - Time.deltaTime * 30, 0);
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
