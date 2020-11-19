using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;

    public GameObject self1;

    // Start is called before the first frame update
    void Start()
    {
        self1 = GameObject.FindWithTag("Enemy");

        EventManager.current.onEnemyDamageEvent += OnEnemyDamage;

        EventManager.current.onEnemyDeathEvent += OnEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        UnityEngine.Debug.Log("inside enemy damage collision function");
        if (hit.gameObject.tag == "Spell1")
        {
            EventManager.current.EnemyDamageEvent(1, self1.GetInstanceID());
        }

    }


    void OnEnemyDamage(int damage, int target)
    {
        if (self1.GetInstanceID() == target)
        {
            health -= damage;
            UnityEngine.Debug.Log("Enemy took " + damage + " damage, " + health + " health remaining");
        }

        if (health <= 0)
        {
            EventManager.current.EnemyDeathEvent(self1.GetInstanceID());
        }
    }

    void OnEnemyDeath(int target)
    {
        UnityEngine.Debug.Log("inside enemy death");
        if (self1.GetInstanceID() == target) {
        Destroy(this.gameObject);
        }
    }


}
