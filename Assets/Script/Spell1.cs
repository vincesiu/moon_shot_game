using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell1 : MonoBehaviour {

    private Transform dudeTransform;
    private Transform aimTransform;
    private Rigidbody2D myRigidBody;
    //private Transform aimWeaponEndPointTransform;
    private SpellState state = SpellState.Ready;
    public float SpellCD = 1;

    public GameObject spellPrefab;
    public bool attached;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 weaponEndPos;
        public Vector3 shootPos;
    }

    void Start() {
        //gameObject.SetActive(false);
    }

    private void Awake() {
        //aimTransform = transform.Find("Aim");
        myRigidBody = GetComponent<Rigidbody2D>();
        dudeTransform = GameObject.FindWithTag("MainDude").transform;
        //aimWeaponEndPointTransform = aimTransform.Find("WeaponEndPointPosition");
        //GetComponent<RigidBody2D>().enabled = false;
    }

    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        //return Vector3.Normalize(vec);
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    // Update is called once per frame
    void Update() {

        if (attached) {
            //GetComponent<Rigidbody2D>().enabled = false;
            Destroy(this.GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;

            //myRigidBody.MovePosition(dudeTransform.position);

            Aim();

            if (Input.GetMouseButtonDown(0)) {
                if (state == SpellState.Ready) {
                    shootSpell();
                    state = SpellState.Cooldown;
                }
            }

            if (state == SpellState.Cooldown) {
                SpellCD -= Time.deltaTime;
            }

            if (SpellCD <= 0) {
                state = SpellState.Ready;
                SpellCD = 1;
            }
        }

        /*
        if (Input.GetMouseButtonDown(0) && attached) {
            shootSpell();
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Inside collide function");
  
        if (collision.gameObject.tag == "MainDude") {
            attached = true;
            aimTransform = GameObject.FindWithTag("SpellAim").transform;

            this.transform.position = new Vector3(aimTransform.position.x, aimTransform.position.y + 0.4f, 0f);
            Destroy(this.GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;

            //need to attach the object to the mainDude
            //myRigidBody.MovePosition(new Vector3(aimTransform.position.x / 2, aimTransform.position.y / 2, 0f));

            gameObject.GetComponent<Transform>().SetParent(GameObject.FindWithTag("SpellAim").transform);
            //aimTransform = GameObject.FindWithTag("SpellAim").transform;

            //myRigidBody.MovePosition(new Vector3(aimTransform.position.x, aimTransform.position.y, 0f));
            //GetComponent<Rigidbody2D>().position = new Vector3(aimTransform.position.x, aimTransform.position.y, 0f);
            //Debug.Log("THIS BODY'S POS: " + myRigidBody.position);
            //Debug.Log("aim transform: " + aimTransform.position);

            Aim();
        }

    }

    private void Aim() {
        //Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 mousePosition = new Vector3(Input.mousePosition.x - Camera.main.pixelWidth / 2, Input.mousePosition.y - Camera.main.pixelHeight / 2, 0f);
        //Vector3 mousePosition = Vector3.Normalize(calculated);
        //Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;

        Vector3 aimDirection = mousePosition.normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        //aimTransform.eulerAngles = new Vector3(dudeTransform.position.x, dudeTransform.position.y, angle);
        //Debug.Log("Mouse World pos: " + mousePosition);
        //Debug.Log("aim transform: " + aimTransform.position);
    }

    public void shootSpell() {
        GameObject spell = Instantiate(spellPrefab) as GameObject;
        spell.transform.position = aimTransform.transform.position;
    }

    public enum SpellState{
        Ready,
        Cooldown
    }
}
