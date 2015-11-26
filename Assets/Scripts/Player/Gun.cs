using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject defaultBullet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(this.defaultBullet) as GameObject;
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;
        }
    }
}
