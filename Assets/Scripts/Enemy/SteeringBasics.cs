using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class SteeringBasics : MonoBehaviour {

    public float maxVelocity = 5f;
    public float maxAcceleration = 10f;                 // The max acceleration
    public float targetRadius = 0.005f;                 // The radius from the target that means we are close enough and have arrived
    public float slowRadius = 1f;                       // The radius from the target where we start to slow down
    public float timeToTarget = 0.1f;                   // The time in which we want to achieve the targetSpeed
    public float turnSpeed = 10f;
    public bool smoothing = true;
    public int numSamplesForSmoothing = 3;

    private Rigidbody rb;
    private Queue<Vector2> velocitySamples = new Queue<Vector2>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Updates the velocity of the current game object by the given linear acceleration
    public void Steer(Vector3 linearAcceleration)
    {
        rb.velocity += linearAcceleration * Time.deltaTime;
        if (rb.velocity.magnitude > maxVelocity)
            rb.velocity = rb.velocity.normalized * maxVelocity;
    }
    public void Steer(Vector2 linearAcceleration)
    {
        Steer(new Vector3(linearAcceleration.x, linearAcceleration.y, 0));
    }

    // Will return the steering for the current game object to seek a given position
    public Vector3 Seek(Vector3 targetPosition, float maxSeekAccel)
    {
        Vector3 acceleration = targetPosition - transform.position;
        // Remove the Y coordinate
        acceleration.y = 0;
        acceleration.Normalize();
        acceleration *= maxSeekAccel;
        return acceleration;
    }
    public Vector3 Seek(Vector3 targetPosition)
    {
        return Seek(targetPosition, maxAcceleration);
    }

    // Makes the current game object look where he is going
    public void LookWhereYoureGoing()
    {
        Vector2 direction = rb.velocity;
        if (smoothing)
        {
            if (velocitySamples.Count == numSamplesForSmoothing)
                velocitySamples.Dequeue();
            velocitySamples.Enqueue(rb.velocity);
            direction = Vector2.zero;
            foreach (Vector2 v in velocitySamples)
                direction += v;
            direction /= velocitySamples.Count;
        }
        LookAtDirection(direction);
    }
    public void LookAtDirection(Vector2 direction)
    {
        direction.Normalize();
        // If we have a non-zero direction then look toward that direction otherwise do nothing
        if (direction.sqrMagnitude > 0.001f)
        {
            float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
    public void LookAtDirection(Quaternion toRotation)
    {
        LookAtDirection(toRotation.eulerAngles.z);
    }
    public void LookAtDirection(float toRotation)
    {
        float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // Returns the steering for a character so it arrives at the target
    public Vector3 Arrive(Vector3 targetPosition)
    {
        Vector3 targetVelocity = targetPosition - transform.position;
        // Remove the Y coordinate
        targetVelocity.y = 0;
        float dist = targetVelocity.magnitude;
        // If we are within the stopping radius then stop
        if (dist < targetRadius)
        {
            rb.velocity = Vector2.zero;
            return Vector2.zero;
        }
        float targetSpeed;
        // Calculate the target speed, full speed at slowRadius distance and 0 speed at 0 distance
        if (dist > slowRadius)
            targetSpeed = maxVelocity;
        else
            targetSpeed = maxVelocity * (dist / slowRadius);
        // Give targetVelocity the correct speed
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;
        // Calculate the linear acceleration we want
        Vector3 acceleration = targetVelocity - new Vector3(rb.velocity.x, rb.velocity.y, 0);
        acceleration *= 1 / timeToTarget;
        // Make sure we are accelerating at max acceleration
        if(acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }
        return acceleration;
    }

    public Vector3 Interpose(Rigidbody target1, Rigidbody target2)
    {
        Vector3 midPoint = (target1.position + target2.position) / 2;

        float timeToReachMidPoint = Vector3.Distance(midPoint, transform.position) / maxVelocity;

        Vector3 futureTarget1Pos = target1.position + target1.velocity * timeToReachMidPoint;
        Vector3 futureTarget2Pos = target2.position + target2.velocity * timeToReachMidPoint;

        midPoint = (futureTarget1Pos + futureTarget2Pos) / 2;

        return Arrive(midPoint);
    }

    // Check to see if the target is in front of the character
    public bool IsInFront(Vector3 target)
    {
        return IsFacing(target, 0);
    }

    public bool IsFacing(Vector3 target, float cosineValue)
    {
        Vector2 facing = transform.right.normalized;

        Vector2 directionToTarget = (target - transform.position);
        directionToTarget.Normalize();

        return Vector2.Dot(facing, directionToTarget) >= cosineValue;
    }

    public static float GetBoundingRadius(Transform t)
    {
        SphereCollider col = t.GetComponent<SphereCollider>();
        return Mathf.Max(t.localScale.x, t.localScale.y, t.localScale.z) * col.radius;
    }
}
