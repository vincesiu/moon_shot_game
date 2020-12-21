using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DudeMovement : MonoBehaviour { 
    public float velocity;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    public Animator animator;

    // Bunch of variables used for moving in the mini cutscene when entering the room
    private bool canMove;
    private Vector3 target;
    private bool shouldMoveIntoRoom;
    private int health;
    private bool slowWalk;

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
        canMove = true;

        EventManager.current.onEnableUserInput += EnableUserInput;
        shouldMoveIntoRoom = false;
        health = 3;
        slowWalk = false;
    }

    void EnableUserInput(bool enable)
    {
        canMove = enable;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Dude hit something: " + col.gameObject.name);
        if (col.gameObject.name.Contains("Room"))
        {
            Debug.Log("starting room sequence");
            StartCoroutine(RoomStartSequence(col.gameObject.name));
        }

        if (col.gameObject.name == "energy-ball(Clone)")
        {
            health -= 1;
            if (health <= 0)
            {
                Debug.Log("Main character has died");
                EventManager.current.CharacterDeathEvent();
            }
        }
        
        if (col.gameObject.name == "NextLevelEvilLair")
        {
            StartCoroutine(gameEndSequence());
        }

        Debug.Log(col.gameObject);
    }

    IEnumerator gameEndSequence()
    {
        slowWalk = true;
        shouldMoveIntoRoom = true;
        canMove = false;
        AsyncOperation handle = SceneManager.LoadSceneAsync(4);
        handle.allowSceneActivation = false;
        yield return new WaitForSeconds(3f);
        handle.allowSceneActivation = true;

    }

    IEnumerator RoomStartSequence(string name)
    {
        shouldMoveIntoRoom = true;
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        EventManager.current.StartRoom(name);
        yield return new WaitForSeconds(0.25f);
        shouldMoveIntoRoom = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update() {
        
        if (canMove) {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            change = Vector3.Normalize(change);
        } else if (shouldMoveIntoRoom)
        {

            change = new Vector3(0.0f, 1.0f, 0.0f);

            change = Vector3.Normalize(change);
            if (slowWalk)
            {

                change = new Vector3(0.0f, 0.1f, 0.0f);
            }
        }
        
        if (change != Vector3.zero && (myRigidBody.velocity == Vector2.zero)) {
            MoveDude();

            animator.SetFloat("Horizontal", change.x);
            animator.SetFloat("Vertical", change.y);
            var speed = change.sqrMagnitude;

            animator.SetFloat("Speed", speed);


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
