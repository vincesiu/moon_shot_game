using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed;

    public Transform target;

    public float health;

    public GameObject self1;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainDude").GetComponent<Transform>();

        self1 = GameObject.FindWithTag("Enemy");


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        UnityEngine.Debug.Log("inside enemy damage collision function");
        if (hit.gameObject.tag == "Spell1")
        {
            EventManager.current.EnemyDamageEvent(1, self1.GetInstanceID());
        }

    }
}
