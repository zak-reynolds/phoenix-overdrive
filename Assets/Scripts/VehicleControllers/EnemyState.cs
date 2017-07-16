using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.VehicleControllers
{
    public abstract class EnemyState
    {
        public VehicleInput Input { get; protected set; }

        public abstract EnemyState ProcessState();

        public virtual void OnEnter(EnemyState lastState) { }
        public virtual void OnExit(EnemyState nextState) { }
    }
}
