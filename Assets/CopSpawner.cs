using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject enemy;

    private bool activated = false;
	
    void OnTriggerEnter(Collider other)
    {
        if (!activated && other.tag.Equals("Player"))
        {
            Instantiate(enemy, transform.position + transform.forward * -50, transform.rotation);
            activated = true;
        }
    }
}
