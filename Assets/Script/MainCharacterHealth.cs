using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterHealth : MonoBehaviour
{
    int health;
    // TODO eventually move this to 

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        EventManager.current.onCharacterDamageEvent += takeDamage;
    }

    void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Main character took " + damage + " damage, " + health + " health remaining");
        if (health <= 0) {
            Debug.Log("Main character has died");
            EventManager.current.CharacterDeathEvent();
        }
    }

}
