using UnityEngine;
using System.Collections;

public class SpawnInBox : MonoBehaviour {

    public float density;
    public GameObject[] objectArray;

    void Start()
    {
        for (int i = 0; i < density; i++)
        {
            Vector3 randPosition;
            randPosition = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
            randPosition = transform.TransformPoint(randPosition * 0.5f);
            Instantiate(objectArray[Random.Range(0, objectArray.Length)], new Vector3(randPosition.x, randPosition.y, randPosition.z), Quaternion.identity);
        }
    }
}
