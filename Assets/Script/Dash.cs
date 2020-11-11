using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {
    private DashState state = DashState.Ready;
    private float dashTime;
    //public float dashDistance = 5f;
    private Rigidbody2D myRigidBody;

    public float dashSpeed;
    public float startDashTime;


    public Vector3 speedTemp;

    bool keyPressed = false;


    void calculateVelocity() {
        float tempX = transform.position.x +  5 * Time.deltaTime;
        float tempY = transform.position.y + 5 * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        //myRigidBody.velocity = new Vector3(5, 5);
    }

    // Update is called once per frame
    void Update() {
 
        switch (state) {
            case DashState.Ready:
                if (dashTime > 0) {
                    //Debug.Log("the key is pressed: " + myRigidBody.velocity);
                    if (keyPressed) {
                        //keep updating the veloctiy of the dude
                        if (Input.GetKey(KeyCode.A)) {
                            //myRigidBody.velocity = Vector2.left * dashSpeed;
                            myRigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.left * dashSpeed * Time.deltaTime);
                            Debug.Log("the key is pressed: left");
                            Debug.Log("the key is pressed: " + myRigidBody.velocity);

                        } else if (Input.GetKey(KeyCode.D)) {
                            myRigidBody.velocity = Vector2.right * dashSpeed;

                            Debug.Log("the key is pressed: right");
                            Debug.Log("the key is pressed: " + myRigidBody.velocity);

                        } else if (Input.GetKey(KeyCode.W)) {
                            myRigidBody.velocity = Vector2.up * dashSpeed;

                            Debug.Log("the key is pressed: up");
                            Debug.Log("the key is pressed: " + myRigidBody.velocity);

                        } else if (Input.GetKey(KeyCode.S)) {
                            //myRigidBody.velocity = new Vector3(myRigidBody.velocity.x * 10f, myRigidBody.velocity.y * 10f);
                            myRigidBody.velocity = Vector2.down * dashSpeed;

                            Debug.Log("the key is pressed: down");
                            Debug.Log("the key is pressed: " + myRigidBody.velocity);

                        } else {
                            Debug.Log("NO KEY IS BEING PRESSED");
                        }

                        //reduce this until it's 0, then set the state to cooldown
                        dashTime -= Time.deltaTime;

                    } else {
                        //if space is not pressed, check if space key is pressed
                        keyPressed = Input.GetKeyDown(KeyCode.Space);
                    }
                } else {
                    state = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                //dashTime -= Time.deltaTime;
               

                if (dashTime <= 0) {
                    //reset the state of the dude
                    dashTime = startDashTime;
                    myRigidBody.velocity = new Vector3(0, 0);
                    keyPressed = false;
                    state = DashState.Ready;
                    Debug.Log("reverting state back to ready");
                }
                
                break;

        }

        //SPRINT STUFF
    /*
        switch (state) {
            case DashState.Ready:
                bool keyPressed = Input.GetKeyDown(KeyCode.Space);
                Debug.Log("the key is pressed: " + keyPressed);
                if (keyPressed) {
                    speedTemp = myRigidBody.velocity;

                    //multiply the velocity by 3
                    myRigidBody.velocity = new Vector3(myRigidBody.velocity.x * 10f, myRigidBody.velocity.y * 10f);

                    //set the state to dashing
                    state = DashState.Dashing;
                    Debug.Log("DASHING");

                }
                break;
            case DashState.Dashing:
                dashTime += Time.deltaTime * 4;
                if (dashTime >= dashDistance) {
                    dashTime += Time.deltaTime * 3;
                    state = DashState.Cooldown;
                    
                    //set speed back to the saved speed
                    myRigidBody.velocity = speedTemp;

                }
                break;
            case DashState.Cooldown:
                dashTime -= Time.deltaTime;

                if (dashTime <= 0) {
                    dashTime = 0;
                    state = DashState.Ready;
                }
                break;

        }    */

    }

    
    public enum DashState {
        Ready,
        Dashing,
        Cooldown
    }

}
