using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VehicleCamRig : MonoBehaviour {

    [SerializeField]
    private Vector3 boom;
    [SerializeField]
    private Transform target;

    private Camera camera;

    [SerializeField]
    private float tilt = 35f;
    [SerializeField]
    private float fovDampening = 10;

    private Vector3 targetPosition;
    private float targetFov = 60;

    void Start ()
    {
        camera = GetComponent<Camera>();
    }
    
    public void SetFOV(float value)
    {
        targetFov = value;
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

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFov, Time.deltaTime * fovDampening);
	}
}
