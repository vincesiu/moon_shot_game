using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeShootSpell : MonoBehaviour {

    public float speed = 50.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;


    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return Vector3.Normalize(vec);
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

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(GetMouseWorldPosition().x + speed, GetMouseWorldPosition().y + speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    // Update is called once per frame
    void Update() {
        //rb.velocity = new Vector2(0, GetMouseWorldPosition().y + speed);
        rb.MovePosition(transform.position + new Vector3(GetMouseWorldPosition().x * speed * Time.deltaTime, GetMouseWorldPosition().y * speed * Time.deltaTime, 0f));
        Debug.Log("Transform position: " + transform.position);
        Debug.Log("Mouse World Position: " + GetMouseWorldPosition());

        if (transform.position.x > screenBounds.x * -2 || transform.position.y > screenBounds.y * 2) {
            //Destroy(this.gameObject);
            //Debug.Log("camera bounds: " + screenBounds.x + " " + screenBounds.y);
            //Debug.Log("transform pos: " + transform.position.x + " " + transform.position.y);
        }
    }

}
