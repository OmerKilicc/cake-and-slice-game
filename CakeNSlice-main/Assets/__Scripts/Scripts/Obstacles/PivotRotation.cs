using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] Transform _customPivot;
    [SerializeField] float _rotationSpeed = 15;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(_customPivot.position, Vector3.up, _rotationSpeed * Time.fixedDeltaTime);
    }
}
