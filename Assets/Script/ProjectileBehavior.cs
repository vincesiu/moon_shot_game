﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lifeTime;

    private Transform player;
    private Vector2 target;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x ==target.x && transform.position.y == target.y) {
            ImpactProjectile();
        }
    }

    /*void Death(){

        if (lifeTime <= 0) {
            Destroy(this.gameObject);
        }
        else{
            lifeTime -= Time.deltaTime;
        }
        
    }*/

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            ImpactProjectile();
        }
    }

    void ImpactProjectile()
    {
        EventManager.current.CharacterDamageEvent(1);
        Destroy(this.gameObject);
    }
}
