using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamRig : MonoBehaviour {

    [SerializeField]
    private Vector3 boom;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float tilt = 35f;

    private Vector3 targetPosition;

    void Start () {
		
	}
	
    void FixedUpdate()
    {
        targetPosition = target.position + Quaternion.Euler(0, target.rotation.eulerAngles.y, 0) * boom;
    }

	void LateUpdate () {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * 5f);
        transform.rotation = Quaternion.Euler(tilt, target.rotation.eulerAngles.y, 0);
	}
}
