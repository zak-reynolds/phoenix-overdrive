using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class VehicleInput
    {
        private float _throttle = 0;
        private float _steering = 0;
        public float Throttle { get { return _throttle; } }
        public float Steering { get { return _steering; } }

        public VehicleInput() : this(0, 0) {}

        public VehicleInput(float throttle, float steering)
        {
            _throttle = throttle;
            _steering = steering;
        }
    }
}
