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
        private float _pitchWobble;
        public float Throttle { get { return _throttle; } }
        public float Steering { get { return _steering; } }
        public float PitchWobble { get { return _pitchWobble; } }

        public VehicleInput(float throttle, float steering, float pitchWobble)
        {
            _throttle = throttle;
            _steering = steering;
            _pitchWobble = pitchWobble;
        }
    }
}
