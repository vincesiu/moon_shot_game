using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell2 : MonoBehaviour {

    private Transform dudeTransform;
    private Transform aimTransform;
    private Rigidbody2D myRigidBody;
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
        EventManager.current.onCharacterSpellPickup += onSpellPickup;
        EventManager.current.onCharacterWeaponAttachment += onWeaponAttachment;
    }

    private void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        dudeTransform = GameObject.FindWithTag("MainDude").transform;
    }

    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;

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
        EventManager.current.CharacterWeaponAttachment();
    }

    void onWeaponAttachment() {
        if (attached) {
            EventManager.current.onCharacterSpellPickup -= onSpellPickup;

            Destroy(this.GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;
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
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MainDude") {
            attached = true;
            EventManager.current.CharacterSpellPickup();
        }
    }

    void onSpellPickup() {
        if (attached) { 
            aimTransform = GameObject.FindWithTag("SpellAim").transform;

            foreach (Behaviour childCompnent in GameObject.FindWithTag("SpellAim").GetComponentsInChildren<Behaviour>()) {
                childCompnent.enabled = false;
            }

            foreach (Transform child in aimTransform) {
                if (child != null) {
                    //child.gameObject.SetActive(false);
                    GameObject.Destroy(child.gameObject);

                }
            }

            this.transform.position = new Vector3(aimTransform.position.x, aimTransform.position.y + 0.4f, 0f);
            Destroy(this.GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;
            aimTransform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Transform>().SetParent(GameObject.FindWithTag("SpellAim").transform);

            Aim();
        }

    }

    private void Aim() {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x - Camera.main.pixelWidth / 2, Input.mousePosition.y - Camera.main.pixelHeight / 2, 0f);
        Vector3 aimDirection = mousePosition.normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void shootSpell() {
        GameObject spell = Instantiate(spellPrefab) as GameObject;
        spell.transform.position = aimTransform.transform.position;
    }

    void OnDestroy() {
        foreach (Behaviour comp in this.gameObject.GetComponentsInChildren<Behaviour>()) {
            comp.enabled = false;
        }

        EventManager.current.onCharacterSpellPickup -= onSpellPickup;
        EventManager.current.onCharacterWeaponAttachment -= onWeaponAttachment;
    }

    public enum SpellState {
        Ready,
        Cooldown
    }
}
