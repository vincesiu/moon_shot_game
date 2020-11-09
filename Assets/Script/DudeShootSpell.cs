using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeShootSpell : MonoBehaviour {

    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Transform dudeTransform;


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

    void Awake() {
        //dudeTransform = GameObject.FindWithTag("MainDude").transform;
        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.x, 0f));
        //Vector3 direction = worldMousePosition - dudeTransform.position;


        /*
        rb = this.GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 normalizedDir = Vector3.Normalize(direction);
        rb.velocity = new Vector3(normalizedDir.x + speed, normalizedDir.y + speed, 0f);
        */

        //rb.velocity = new Vector3(GetMouseWorldPosition().x + speed, GetMouseWorldPosition().y + speed, 0f);

        //Vector3 calculated = Vector3.Normalize(new Vector3(Input.mousePosition.x - Camera.main.pixelWidth / 2, Input.mousePosition.y - Camera.main.pixelHeight / 2, 0f));
        Vector3 calculated = new Vector3(Input.mousePosition.x - Camera.main.pixelWidth / 2, Input.mousePosition.y - Camera.main.pixelHeight / 2, 0f);
        Vector3 normalizedVec = Vector3.Normalize(calculated);

        Debug.Log("calculated: " + calculated);
        Debug.Log("normalized: " + normalizedVec);

        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(normalizedVec.x * speed, normalizedVec.y * speed, 0f);

        //Debug.Log("height: " + Camera.main.pixelHeight);
        //Debug.Log("width: " + Camera.main.pixelWidth);
        //Debug.Log("mousePos: " + Input.mousePosition);

    }
    void test() {
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - rb.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rb.position.y);
        //Vector2 direction = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Debug.Log("the camera position: " + Camera.main.transform.position);
        Debug.Log(direction);

    }

    /*
    void getDir() {

        Vector3 point = new Vector3();
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Camera.main.pixelHeight - Input.mousePosition.y;

        //point = GameObject.FindWithTag("MainDude").transform.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

        Debug.Log("mouse pos: " + mousePos);
        Debug.Log(point);
    }*/

    // Start is called before the first frame update
    void Start() {
        //rb = this.GetComponent<Rigidbody2D>();
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    // Update is called once per frame
    void Update() {
        //rb.velocity = new Vector2(0, GetMouseWorldPosition().y + speed);
        //rb.MovePosition(transform.position + new Vector3(GetMouseWorldPosition().x * speed * Time.deltaTime, GetMouseWorldPosition().y * speed * Time.deltaTime, 0f));
        //Debug.Log("Transform position: " + transform.position);
        //Debug.Log("Mouse World Position: " + GetMouseWorldPosition());

        Destroy(this.gameObject, 2.0f);
    }

}
