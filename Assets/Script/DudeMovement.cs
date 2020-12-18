using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMovement : MonoBehaviour { 
    public float velocity;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    public Animator animator;

    // Bunch of variables used for moving in the mini cutscene when entering the room
    private bool canMove;
    private Vector3 target;
    private bool shouldMoveTowardsTarget;

    //private Vector2 movement;
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
        canMove = false;

        EventManager.current.onEnableUserInput += EnableUserInput;
        target = new Vector3(10.0f, 15.0f, 0.0f);
        shouldMoveTowardsTarget = true;
    }

    void EnableUserInput(bool enable)
    {
        canMove = enable;
    }

    // Update is called once per frame
    void Update() {
        
        if (canMove) {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            change = Vector3.Normalize(change);
        } else if (shouldMoveTowardsTarget)
        {
            change = Vector3.MoveTowards(this.transform.position, target, 1.0f);
            change = Vector3.Normalize(change);
        }

        //Debug.Log(change);

        if (change != Vector3.zero && (myRigidBody.velocity == Vector2.zero)) {
            MoveDude();

            animator.SetFloat("Horizontal", change.x);
            animator.SetFloat("Vertical", change.y);
            animator.SetFloat("Speed", change.sqrMagnitude);
        } else {
            //Debug.Log("THE VELOCITY IS: " + myRigidBody.velocity);
            //animator.SetFloat("Horizontal", 0);
            //animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }

        change = Vector3.zero;
        
        /*
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        */
    }
}
