using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixFacade : VehicleFacade {

    [SerializeField]
    private List<ParticleSystem> flames;
    private Collider damageCollider;
	
    protected override void Start()
    {
        base.Start();
        damageCollider = GetComponentInChildren<Collider>();
    }
	protected override void Update () {
        base.Update();
        if (vehicle != null)
        {
            if (vehicle.IsPheonix())
            {
                flames.ForEach((ps) => { if (!ps.isPlaying) ps.Play(); });
                damageCollider.enabled = true;
            }
            else
            {
                flames.ForEach((ps) => { if (ps.isPlaying) ps.Stop(); });
                damageCollider.enabled = false;
            }
        }
	}
}
