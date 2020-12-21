using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    private float health;
   // public int id;
    public GameObject self1;

    // Start is called before the first frame update
    void Start()
    {
        //self1 = GameObject.FindWithTag("Enemy");
        health = 2;

        self1 = this.gameObject;

        UnityEngine.Debug.Log("this is self1 id value " + self1.GetInstanceID() + ", reporting " + health + " health remaining");

        //UnityEngine.Debug.Log("this is the position value: " + self1.transform.position);

        EventManager.current.onEnemyDamageEvent += OnEnemyDamage;

        EventManager.current.onEnemyDeathEvent += OnEnemyDeath;
    }


    void OnTriggerEnter2D(Collider2D hit)
    {
        //UnityEngine.Debug.Log("inside enemy damage collision function");
        if (hit.gameObject.tag == "Spell1")
        {
            UnityEngine.Debug.Log("this is self1 id value" + self1.GetInstanceID());
            EventManager.current.EnemyDamageEvent(1, self1.gameObject.GetInstanceID());
        }

    }


    void OnEnemyDamage(int damage, int target)
    {

        if (self1.gameObject.GetInstanceID() == target && health >= 0)
        {
            health -= damage;

            UnityEngine.Debug.Log("[stat] Enemy id " + self1.GetInstanceID() + " took " + damage + " damage, " + health + " health remaining " + "this is the id value: " + self1.GetInstanceID());

            if (health <= 0)
            {
                UnityEngine.Debug.Log("[event] Firing Enemy Death Event");
                EventManager.current.EnemyDeathEvent(self1.gameObject.GetInstanceID());
            }
        }


    }

    void OnEnemyDeath(int target)
    {
       // UnityEngine.Debug.Log("inside enemy death");
      if (self1.gameObject.GetInstanceID() == target) {
            Destroy(self1.gameObject);
       }
    }


}
