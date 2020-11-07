using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAim : MonoBehaviour {

    private Transform aimTransform;
    private Transform aimWeaponEndPointTransform;

    public GameObject spellPrefab;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 weaponEndPos;
        public Vector3 shootPos;
    }


    private void Awake() {
        aimTransform = transform.Find("Aim");
        aimWeaponEndPointTransform = aimTransform.Find("WeaponEndPointPosition");
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
        Aim();
        if (Input.GetMouseButtonDown(0)) {
            shootSpell();
        }
    }

    private void Aim() {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void shootSpell() {
        GameObject spell = Instantiate(spellPrefab) as GameObject;
        spell.transform.position = aimTransform.transform.position;
    }
}
