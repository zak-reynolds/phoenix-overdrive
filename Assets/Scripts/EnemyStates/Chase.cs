using Assets.Scripts.VehicleControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Models;

namespace Assets.Scripts.EnemyStates
{
    public class Chase : EnemyState
    {
        protected Transform target;
        protected Transform self;

        public Chase(Transform target, Transform self)
        {
            this.target = target;
            this.self = self;
        }
        
        public override EnemyState ProcessState()
        {
            var targetDirection = (target.position - self.position).normalized;
            Input = new VehicleInput(
                1,
                Vector3.Dot(self.right, targetDirection) > 0 ? 1 : -1);

            return null;
        }
    }
}
