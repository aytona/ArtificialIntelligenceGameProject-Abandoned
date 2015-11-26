using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBoundary
{
    public float xMin, xMax, zMin, zMax;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public PlayerBoundary boundary;
    public float speed;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 1.0f, moveVertical);

        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 1.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
    }
}
