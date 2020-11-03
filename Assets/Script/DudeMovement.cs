using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMovement : MonoBehaviour { 
    public float velocity;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    void MoveDude() {
        myRigidBody.MovePosition(
            transform.position + change * velocity * Time.deltaTime
            );

        //Debug.Log(velocity);
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        //Debug.Log(change);

        if (change != Vector3.zero && (myRigidBody.velocity == Vector2.zero)) {
            MoveDude();

            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        } else {
            Debug.Log("THE VELOCITY IS: " + myRigidBody.velocity);

        }
    }
}
