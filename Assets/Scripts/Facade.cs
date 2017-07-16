using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour
{
    [SerializeField]
    private float translationDamping = 15;

    [SerializeField]
    private float rotationDamping = 10;

    private Transform target;
    private Quaternion rotationOffset = Quaternion.identity;

    public void SetRotationOffset(Quaternion offset)
    {
        rotationOffset = offset;
    }

    void Start()
    {
        Debug.Assert(transform.parent != null, "Facade must have a parent");
        target = transform.parent;
        transform.parent = transform.parent.parent;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * translationDamping);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationDamping) * rotationOffset;
    }
}
