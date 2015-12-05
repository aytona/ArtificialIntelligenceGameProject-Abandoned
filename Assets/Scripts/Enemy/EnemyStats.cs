using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    public enum EnemyTypes
    {
        MELEE = 35,
        RANGE = 15,
        BOSS = 100
    }
    public GameObject attackObject;

    private EnemyTypes type;
    private int health;
    private int bulletDamage = 5;
    private int shotgunDamage = 10;
    private int lazerDamage = 3;

    void Start()
    {
        SetType();
    }

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
            Destroy(other.gameObject);
        }
        if (gameObject.tag == type.ToString() + " Attack")
            attackObject.SetActive(true);
    }

    void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "Lazer")
            health -= lazerDamage;
        if (gameObject.tag == "Shotgun")
            health -= shotgunDamage;
    }

    void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == type.ToString() + " Attack")
            attackObject.SetActive(false);
    }

    private void SetType()
    {
        if (gameObject.tag == "Melee")
            type = EnemyTypes.MELEE;
        if (gameObject.tag == "Range")
            type = EnemyTypes.RANGE;
        if (gameObject.tag == "Boss")
            type = EnemyTypes.BOSS;
        health = (int)type;
    }
}
