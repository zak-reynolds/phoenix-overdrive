using Assets.Scripts.EnemyStates;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.VehicleControllers
{
    public class CopperVehicleController : VehicleController
    {
        private EnemyState currentState;

        protected override void Start()
        {
            currentState = new Chase(
                GameObject.Find("PlayerVehicle").transform,
                transform);

            base.Start();
        }

        protected override void Update()
        {
            EnemyState nextState = currentState.ProcessState();
            if (nextState != null)
            {
                currentState.OnExit(nextState);
                nextState.OnEnter(currentState);
                currentState = nextState;
            }
            
            vehicle.SetInput(currentState.Input);

            base.Update();
        }
    }
}
