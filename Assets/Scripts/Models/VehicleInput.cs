using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class VehicleInput
    {
        private float _throttle;
        private float _steering;
        public float Throttle { get { return _throttle; } }
        public float Steering { get { return _steering; } }

        public VehicleInput(float throttle, float steering)
        {
            _throttle = throttle;
            _steering = steering;
        }
    }
}
