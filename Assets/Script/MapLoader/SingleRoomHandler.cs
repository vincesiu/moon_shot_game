using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoomHandler : MonoBehaviour
{
    public GameObject enemyTemplate;
    private int numEnemies = 3;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onStartRoom += GenerateEnemies;
    }

    // Update is called once per frame
    void GenerateEnemies(string roomName)
    {
        Debug.Log("generate enemies called for room:" + roomName);
        if (roomName == gameObject.name)
        {

            Debug.Log("generating enemies...");
            
            var enemyArr = new GameObject[numEnemies];
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject enemy = Instantiate(enemyTemplate);
                enemy.transform.position = gameObject.transform.position;

                // Where did the 22 and 17 come from? See MapDebugger::GenerateRoom and how I hardcoded the room sizes. 
                // May the programming gods have mercy on my hubris
                var xOffset = 22 / 2;
                var yOffset = 17 / 2;
                enemy.transform.position += new Vector3(xOffset -1.0f + i, yOffset, 0.0f);
                enemyArr[i] = enemy;
            }
            
        }
    }
}
