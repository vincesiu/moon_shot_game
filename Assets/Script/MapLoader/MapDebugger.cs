using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDebugger : MonoBehaviour
{

    public BoundsInt woof;

    public Tilemap walls;
    public TileBase wall1;
    public TileBase wall2;

    public Tilemap rooms;
    public TileBase floor1;
    public TileBase floor2;
    
    // Start is called before the first frame update
    void Start()
    {
        BoundsInt nyan = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(22, 17, 1));
        GenerateRoom(nyan);

        BoundsInt nyan2 = new BoundsInt(new Vector3Int(0, 17, 0), new Vector3Int(22, 17, 1));
        GenerateRoom(nyan2);
    }

    // Example: Generates a room with the bottom left at 0,0,0, the interior size is 20x15 = 300 tiles, and there are walls surrounding the inner room
    // BoundsInt nyan = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(22, 17, 1));
    // GenerateRoom(nyan)
    void GenerateRoom(BoundsInt bounds)
    {
        int width = bounds.size.x;
        int height = bounds.size.y;
        int size = width * height;
        Debug.Log(size);

        // Floor tilemap generation
        TileBase[] tileArray = new TileBase[size];
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = Random.value > 0.5 ? floor1 : floor2;
        }
        rooms.SetTilesBlock(bounds, tileArray);


        // Wall tilemap generation
        // Has two sections. 
        // Section 1: generation of wall tiles
        // Section 2: generation of doors by removing walls
        ////////////////////////////////////////
        TileBase[] tileArray2 = new TileBase[size];
        // top / bottom sprites
        for (int idx = 0; idx < width; idx++) {
            tileArray2[idx] = Random.value > 0.5 ? wall1 : wall2;
            tileArray2[size - idx - 1] = Random.value > 0.5 ? wall1 : wall2;
        }
        // left / right side sprites
        for (int idx = 0; idx < height; idx++) { 
            tileArray2[width * idx] = Random.value > 0.5 ? wall1 : wall2;
            tileArray2[width * idx + width - 1] = Random.value > 0.5 ? wall1 : wall2;
        }

        // top door
        int idx2 = width / 2;
        tileArray2[idx2 - 1] = null;
        tileArray2[idx2] = null;

        // bottom door
        tileArray2[size - idx2 ] = null;
        tileArray2[size - idx2 - 1] = null;

        // left door
        idx2 = height / 2;
        tileArray2[idx2 * width ] = null;
        tileArray2[(idx2 + 1) * width] = null;

        // bottom door
        tileArray2[(idx2 * width) + width - 1] = null;
        tileArray2[((idx2 + 1)* width)+ width - 1] = null;
        
        walls.SetTilesBlock(bounds, tileArray2);
        

        
    }
}
