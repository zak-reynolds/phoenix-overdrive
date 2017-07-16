using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickup : Pickup {

    protected override void OnPickup(Vehicle vehicle)
    {
        if (vehicle.IsCarryingKey()) {
            Destroy(gameObject);
        }
    }
}
