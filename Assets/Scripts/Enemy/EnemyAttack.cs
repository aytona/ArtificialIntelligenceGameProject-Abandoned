using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float rateOfFire = 3f;
    public GameObject enemyBullet;

    void Awake ()
    {
        StartCoroutine(ShootCoroutine(rateOfFire));
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
        }
    }
}
