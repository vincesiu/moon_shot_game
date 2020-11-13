using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyKite : MonoBehaviour {
    public float speed;

    public float health;

    public Transform target;

    public float stopDistance;

    public float retreatDistance;

    public float shotIntervals;

    public float startTimeShots;

    public GameObject projectile;

    public float shakeInterval;

    public float startShakeTime;

    WaitForSecondsRealtime waitForSecondsRealtime;

    // Start is called before the first frame update
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        shotIntervals = startTimeShots;

        shakeInterval = startShakeTime;

    }

    // Update is called once per frame
    void FixedUpdate() {
        // runs towards enemy until enemy reaches stopping distance
        if (Vector2.Distance(transform.position, target.position) > stopDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Kite();
        }
        
        //UnityEngine.Debug.Log(Vector2.Distance(transform.position, target.position));
        // if enemy is in stopping distance, enemy will shake
        else if (Vector2.Distance(transform.position, target.position) < stopDistance && Vector2.Distance(transform.position, target.position) > retreatDistance) {

            Shake();
            Attack();
            UnityEngine.Debug.Log("inside shake if");

        }
        // if player closes the gap between enemy, enemy will retreat
        if (Vector2.Distance(transform.position, target.position) < retreatDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position/*new Vector2(transform.position.x - stopDistance, transform.position.y - stopDistance)*/, -speed * Time.deltaTime);
           
            UnityEngine.Debug.Log("inside retreat if");
        }

    }


    IEnumerator Kite() {
        yield return new WaitForSeconds(300);

    }

    void Attack() {
        if (shotIntervals <= 0) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotIntervals = startTimeShots;
        }
        else {
            shotIntervals -= Time.deltaTime;
        }


    }

    void Shake() {
        if (shakeInterval <= 0)
        {
            float randX = Random.Range(-10.5f, 10.5f);
            float randY = Random.Range(-10.5f, 10.5f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + randX, transform.position.y + randY), -speed * Time.deltaTime);
            shakeInterval = startShakeTime;
           
            UnityEngine.Debug.Log("this is randX");
            UnityEngine.Debug.Log(randX);
            UnityEngine.Debug.Log("this is randY");
            UnityEngine.Debug.Log(randY);
        }
        else {
            shakeInterval -= Time.deltaTime;
        }
    }

}
