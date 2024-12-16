using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEffect : MonoBehaviour
{
    [SerializeField]
    private Transform _vacuumNozzle;
    [SerializeField]
    private float _springForce = 10f;
    [SerializeField]
    private float _damping = 2f;
    [SerializeField]
    private float _maxDistance = 5f;
    [SerializeField]
    private float _rotationSpeed = 4f;

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 directionToVacuum = _vacuumNozzle.position - transform.position;
        float distanceToVacuum = directionToVacuum.magnitude;

        //use dot product to check if object is moving closer to nozzle
        bool isMovingCloser = Vector3.Dot(_rb.velocity.normalized, directionToVacuum.normalized) > 0;

        if (isMovingCloser)
        {//rotate towards nozzle when moving
            RotateTowardsNozzle(directionToVacuum);
        }

        if (distanceToVacuum <= _maxDistance)
        {//if within the maxdistance, calculate sprint force, and damping it, and add force
            Vector3 spring = directionToVacuum.normalized * _springForce;
            Vector3 dampingForce = -_rb.velocity * _damping;

            _rb.AddForce(spring + dampingForce);
        }
        else
        {
            //clamp opsition to MaxDistance if it exceeds the limit
            Vector3 clampedPosition = _vacuumNozzle.position - directionToVacuum.normalized * _maxDistance;
            transform.position = clampedPosition;
        }       
    }

    void RotateTowardsNozzle(Vector3 directionToVacuum)
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToVacuum, Vector3.up);

        // Smoothly interpolate the current rotation towards the target
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
    }
}
