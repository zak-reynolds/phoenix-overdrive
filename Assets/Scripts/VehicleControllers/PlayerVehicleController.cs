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

        [Header("Pheonix")]
        [SerializeField]
        private float pheonixSpeedBoost = 1.5f;
        private float effectivePheonixBoost = 1;
        [SerializeField]
        private float pheonixTime = 3f;
        private float pheonixTimer = 0;
        [SerializeField]
        private int pheonixGear = 5;
        private int gear = 0;
        [SerializeField]
        private float pheonixWindow = 0.5f;
        private float pheonixWindowTimer = 0;

        protected override void Update()
        {
            if (Input.GetButtonDown("Gas"))
            {
                TryToGearUp();

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

            if (pheonixTimer == 0 && pheonixWindowTimer == 0) gear = 0;
            pheonixWindowTimer = Mathf.Max(pheonixWindowTimer - Time.deltaTime, 0);
            pheonixTimer = Mathf.Max(pheonixTimer - Time.deltaTime, 0);
            effectivePheonixBoost = pheonixTimer > 0 ? pheonixSpeedBoost : 1;

            var input = new VehicleInput(
                throttle * effectivePheonixBoost,
                Input.GetAxis("Horizontal") * (1 + steeringBoostIntensity),
                pheonixTimer > 0);
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

        public int GetGear()
        {
            return gear;
        }

        private bool TryToGearUp()
        {
            if (pheonixTimer == 0 && vehicle.GetHP() > 0)
            {
                if (pheonixWindowTimer == 0)
                {
                    pheonixWindowTimer = pheonixWindow;
                    gear = 1;
                }
                else
                {
                    gear++;
                    pheonixWindowTimer = pheonixWindow;
                    if (gear >= pheonixGear)
                    {
                        vehicle.DecrementHP();
                        pheonixTimer = pheonixTime;
                    }
                }
                return true;
            }
            return false;
        }

        public float GetPheonixPercentage()
        {
            return pheonixTimer / pheonixTime;
        }

        public float GetGearingPercentage()
        {
            if (gear <= 1) return 0;
            return (float)(gear -1) / (pheonixGear - 1);
        }
    }
}
