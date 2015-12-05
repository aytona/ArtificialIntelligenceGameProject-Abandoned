using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    public enum EnemyTypes
    {
        MELEE = 35,
        RANGE = 15,
        BOSS = 100
    }

    private EnemyTypes type;
    private int health;

    void Start()
    {
        CheckType();
        Debug.Log(health);
    }

    private void CheckType()
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
