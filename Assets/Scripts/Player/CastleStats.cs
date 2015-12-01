using UnityEngine;
using System.Collections;

public class CastleStats : MonoBehaviour {

    public enum CastleTypes
    {
        LevelOne = 50,
        LevelTwo = 80,
        LevelThree = 120,
        LevelFour = 150,
        LevelFive = 200
    }
    public CastleTypes upgrade;

    private CastleTypes current;
    private int health;

    void Start()
    {
        // Initial health
        health = (int)current;
    }

    void Update()
    {
        SetType();

        // If the current type does not equal to the updated(upgrade) type
        if (current != upgrade)
        {
            // Upgrade health and change current type to the new upgrade type
            health = (int)upgrade;
            current = upgrade;
        }
    }

    private void SetType()
    {
        // Need someway to upgrade castle and change type through UI
    }
}
