using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEffect : MonoBehaviour
{
    public Transform vacuumNozzle;
    public float springForce = 10f;
    public float damping = 2f;
    public float maxDistance = 5f;
    public float rotationSpeed = 4f;

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 directionToVacuum = vacuumNozzle.position - transform.position;
        float distanceToVacuum = directionToVacuum.magnitude;

        //use dot product to check if object is moving closer to nozzle
        bool isMovingCloser = Vector3.Dot(_rb.velocity.normalized, directionToVacuum.normalized) > 0;

        if (isMovingCloser)
        {//rotate towards nozzle when moving
            RotateTowardsNozzle(directionToVacuum);
        }

        if (distanceToVacuum <= maxDistance)
        {//if within the maxdistance, calculate sprint force, and damping it, and add force
            Vector3 spring = directionToVacuum.normalized * springForce;
            Vector3 dampingForce = -_rb.velocity * damping;

            _rb.AddForce(spring + dampingForce);
        }
        else
        {
            //clamp opsition to MaxDistance if it exceeds the limit
            Vector3 clampedPosition = vacuumNozzle.position - directionToVacuum.normalized * maxDistance;
            transform.position = clampedPosition;
        }       
    }

    void RotateTowardsNozzle(Vector3 directionToVacuum)
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToVacuum, Vector3.up);

        // Smoothly interpolate the current rotation towards the target
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }
}
