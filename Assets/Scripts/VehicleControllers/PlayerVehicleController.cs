using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.VehicleControllers
{
    public class PlayerVehicleController : VehicleController
    {
        [Header("Component Links")]
        [SerializeField]
        protected VehicleCamRig camRig;

        [Header("Control Feel")]
        [SerializeField]
        private float throttleDecay = 0.5f;
        [SerializeField]
        private float throttleImpulse = 25f;

        [SerializeField]
        private float steeringBoost = 1.5f;
        [SerializeField]
        private float steeringBoostDecay = 0.5f;

        protected override void Update()
        {
            if (Input.GetButtonDown("Gas"))
            {
                wobbleIntensity = gasOnWobble;
                steeringBoostIntensity = steeringBoost;
                camRig.SetFOV(75);
                camRig.ActivateZoomBoom();
                vehicle.AddImpulse(throttleImpulse);
            }
            if (Input.GetButtonUp("Gas"))
            {
                wobbleIntensity = gasOffWobble;
                camRig.SetFOV(65);
                camRig.DeactivateZoomBoom();
            }
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

            base.Update();
        }

        protected override void LateUpdate()
        {
            if (vehicle.IsDoomed())
            {
                var deathGob = Instantiate(deathPrefab, transform.position, transform.rotation);
                camRig.ResetTarget(deathGob.transform);
                Destroy(vehicle.gameObject);
            }
        }
    }
}
