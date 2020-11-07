using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyKite : MonoBehaviour {
    public float speed;

    public Transform target;

    public float stopDistance;

    WaitForSecondsRealtime waitForSecondsRealtime;

    // Start is called before the first frame update
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Vector2.Distance(transform.position, target.position) > stopDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Kite();
        }
        
        //UnityEngine.Debug.Log(Vector2.Distance(transform.position, target.position));

        if (Vector2.Distance(transform.position, target.position) < stopDistance) {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x+Random.Range(-10.5f,10.5f), transform.position.y+Random.Range(-10.5f,10.5f)), speed * Time.deltaTime);
            UnityEngine.Debug.Log("inside shake if");

        }

        if (Vector2.Distance(transform.position, target.position) < stopDistance-2) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - stopDistance, transform.position.y - stopDistance), speed * Time.deltaTime);
            Kite();
            UnityEngine.Debug.Log("inside retreat if");
        }

    }


    IEnumerator Kite() {
        yield return new WaitForSeconds(300);

    }

}
