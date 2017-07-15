using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float translationDamping;

    [SerializeField]
    private float rotationDamping;

    private Quaternion rotationOffset = Quaternion.identity;

    public void SetRotationOffset(Quaternion offset)
    {
        rotationOffset = offset;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * translationDamping);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationDamping) * rotationOffset;
    }
}
