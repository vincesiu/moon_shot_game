using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Awake() {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}
