using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleFacade : Facade {

    [SerializeField]
    private ParticleSystem heat;
    protected Vehicle vehicle;
	
    protected override void Start()
    {
        base.Start();
        vehicle = target.GetComponent<Vehicle>();
    }
	protected override void Update () {
        base.Update();
        if (vehicle != null)
        {
            if (!vehicle.InBounds() && !heat.isPlaying) heat.Play();
            if (vehicle.InBounds() && heat.isPlaying) heat.Stop();
        }
	}
}
