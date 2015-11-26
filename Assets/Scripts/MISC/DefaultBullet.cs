using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DefaultBullet : MonoBehaviour {

    public float speed = 5f;
    public Vector3 myDirection = Vector3.left;
    public float lifeSpan = 5f;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(LifespanCoroutine());
    }

    void FixedUpdate()
    {
        rb.AddForce(myDirection * speed);
    }

    private IEnumerator LifespanCoroutine()
    {
        yield return new WaitForSeconds(this.lifeSpan);
        Destroy(this.gameObject);
    }
}
