using UnityEngine;
using System.Collections;

public class StartAttack : MonoBehaviour {

    public GameObject enemyBullet;
    public float rateOfFire;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enabler")
        {
            StartCoroutine(ShootCoroutine(rateOfFire));
        }
        if (other.tag == "PlayerBullet")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBullet) as GameObject;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }

    private IEnumerator ShootCoroutine(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Shoot();
            if (player == null)
                break;
        }
    }
}
