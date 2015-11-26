using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Steering : MonoBehaviour
{
    public float maxVelocity;
    public float maxAcceleration;
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget;
    public float turnSpeed;
    public bool smoothing;
    public int numSamples;

    private Rigidbody rb;
    private Queue<Vector2> velocitySamples = new Queue<Vector2>();

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    public void Steer(Vector3 linearAcceleration)
    {
        rb.velocity += linearAcceleration * Time.deltaTime;
        if (rb.velocity.magnitude > maxVelocity)
            rb.velocity = rb.velocity.normalized * maxVelocity;
    }

    public void LookWhereYoureGoing()
    {
        Vector2 direction = rb.velocity;
        if (smoothing)
        {
            if (velocitySamples.Count == numSamples)
                velocitySamples.Dequeue();
            velocitySamples.Enqueue(rb.velocity);
            direction = Vector2.zero;
            foreach(Vector2 v in velocitySamples)
            {
                direction += v;
            }
            direction /= velocitySamples.Count;
        }
        LookAtDirection(direction);
    }

    public void LookAtDirection(Vector2 direction)
    {
        direction.Normalize();
        if (direction.sqrMagnitude > 0.001f)
        {
            float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.y, toRotation, Time.deltaTime * turnSpeed);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public Vector3 Arrive(Vector3 targetPosition)
    {
        Vector3 targetVelocity = targetPosition - transform.position;
        targetVelocity.y = 0;
        float dist = targetVelocity.magnitude;
        if (dist < targetRadius)
        {
            rb.velocity = Vector2.zero;
            return Vector2.zero;
        }
        float targetSpeed;
        if (dist > slowRadius)
            targetSpeed = maxVelocity;
        else
            targetSpeed = maxVelocity * (dist / slowRadius);
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;
        Vector3 acceleration = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);
        acceleration *= 1 / timeToTarget;
        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }
        return acceleration;
    }
}
