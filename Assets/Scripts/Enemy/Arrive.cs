using UnityEngine;
using System.Collections;

public class Arrive : MonoBehaviour
{
    public Vector3 targetPosition;

    private Steering steering;
    private Vector3 finalPosition;

	void Start ()
    {
        steering = GetComponent<Steering>();
        finalPosition = new Vector3(targetPosition.x, targetPosition.y, Random.Range(-targetPosition.z, targetPosition.z));
    }

	void Update ()
    {
        Vector3 accel = steering.Arrive(finalPosition);
        steering.Steer(accel);
        steering.LookWhereYoureGoing();
	}
}
