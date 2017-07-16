﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    
	void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            OnPickup(other.GetComponent<Vehicle>());
        }
    }

    protected virtual void OnPickup(Vehicle vehicle)
    {
        Destroy(gameObject);
    }
}
