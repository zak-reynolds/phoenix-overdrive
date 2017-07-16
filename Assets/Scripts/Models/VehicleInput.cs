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
        private bool _isPheonix = false;
        public float Throttle { get { return _throttle; } }
        public float Steering { get { return _steering; } }
        public bool IsPheonix { get { return _isPheonix; } }

        public VehicleInput() : this(0, 0, false) {}

        public VehicleInput(float throttle, float steering) : this(throttle, steering, false) { }

        public VehicleInput(float throttle, float steering, bool raiseGear)
        {
            _throttle = throttle;
            _steering = steering;
            _isPheonix = raiseGear;
        }
    }
}
