using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDebugger : MonoBehaviour
{
    // Need this to set the parent
    public GameObject grid;

    // Prefabs
    public GameObject startRoomTemplate;
    public GameObject endRoomTemplate;
    public GameObject wallsTemplate;
    public GameObject roomTemplate;
    public GameObject hallwayTemplate;
    public GameObject doorsTemplate;
    public GameObject enemyTemplate;

    // Tile palette since I don't know how to handle resource loading
    public TileBase floor1;
    public TileBase floor2;
    public TileBase wall1;
    public TileBase wall2;
    public TileBase door;

    // Tilemaps 
    Tilemap doorsTilemap;
    Tilemap hallwayTilemap;
    Tilemap wallsTilemap;

    // TODO: remove later, used for manually opening and closing doors
    GameObject doorsObject;

    int remainingEnemies;

    private int roomCounter;
    
    enum RoomType
    {
        StartRoom,
        BossRoom,
        CommonRoom,
    }

    // Start is called before the first frame update
    void Start()
    {
        remainingEnemies = 0;

        roomCounter = 0;
        GameObject hallwayObject = Instantiate(hallwayTemplate);
        hallwayObject.transform.parent = grid.transform;
        hallwayObject.name = "GeneratedHallway";
        hallwayTilemap = hallwayObject.GetComponent<Tilemap>();

        //TODO remove this when I have event handling
        doorsObject = Instantiate(doorsTemplate);
        doorsObject.transform.parent = grid.transform;
        doorsObject.name = "GeneratedDoorway";
        doorsTilemap = doorsObject.GetComponent<Tilemap>();

        GenerateBackgroundWalls();

        
        for (int i = 0; i < 5; i++)
        {
            BoundsInt bounds = new BoundsInt(new Vector3Int(0, i * 17, 0), new Vector3Int(22, 17, 1));
            if (i == 0)
            {
                GenerateRoom(bounds, RoomType.StartRoom);
            } else if (i == 4)
            {
                GenerateRoom(bounds, RoomType.BossRoom);
            } else
            {
                GenerateRoom(bounds, RoomType.CommonRoom);
            }
        }

        doorsObject.SetActive(false);

        EventManager.current.onFinishRoom += turnOffDoors;
        EventManager.current.onStartRoom += turnOnDoors;
        EventManager.current.onStartRoom += SetEnemies;
        EventManager.current.onEnemyDeathEvent += DecreaseRemainingEnemies;
    }

    void turnOnDoors(string _)
    {
        doorsObject.SetActive(true);
    }

    void turnOffDoors()
    {
        doorsObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            doorsObject.SetActive(!doorsObject.activeSelf);
        }
    }

    void SetEnemies(string _)
    {
        remainingEnemies = 3;
    }

    void DecreaseRemainingEnemies(int _)
    {
        if (--remainingEnemies <= 0)
        {
            EventManager.current.FinishRoom();
        }
    }

    void GenerateBackgroundWalls()
    {
        // WARNING THIS IS A GOTCHA
        // Hardcoded bounds of the walls 
        int length = 22;
        int width = 17 * 5;

        int wallEdges = 10;
        int adjustedLength = length + wallEdges * 2;
        int adjustedWidth = width + wallEdges * 2;

        BoundsInt bounds = new BoundsInt(new Vector3Int(-wallEdges, -wallEdges, 0), new Vector3Int(adjustedLength, adjustedWidth, 1));
        TileBase[] tileArray = new TileBase[adjustedLength * adjustedWidth];
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = GenWallTile();
        }

        GameObject wallsObject = Instantiate(wallsTemplate);

        wallsObject.transform.parent = grid.transform;
        wallsObject.name = "GeneratedWalls";

        wallsTilemap = wallsObject.GetComponent<Tilemap>();
        wallsTilemap.SetTilesBlock(bounds, tileArray);
    }

    // Example: Generates a room with the bottom left at 0,0,0, the interior size is 20x15 = 300 tiles, and there are walls surrounding the inner room
    // BoundsInt nyan = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(22, 17, 1));
    // GenerateRoom(nyan)
    void GenerateRoom(BoundsInt globalBounds, RoomType roomType)
    {

        int width = globalBounds.size.x;
        int height = globalBounds.size.y;
        int size = width * height;

        // Floor tilemap generation
        TileBase[] roomTiles = new TileBase[size];
        TileBase[] wallTiles = new TileBase[size]; 
        for (int index = 0; index < roomTiles.Length; index++)
        {
            roomTiles[index] = Random.value > 0.5 ? floor1 : floor2;
            wallTiles[index] = null;
        }

        // top / bottom sprites
        for (int idx = 0; idx < width; idx++)
        {
            roomTiles[idx] = null;
            roomTiles[size - idx - 1] = null;
            wallTiles[idx] = GenWallTile();
            wallTiles[size - idx - 1] = GenWallTile();
        }
        // left / right side sprites
        for (int idx = 0; idx < height; idx++)
        {
            roomTiles[width * idx] = null;
            roomTiles[width * idx + width - 1] = null;
            wallTiles[width * idx] = GenWallTile();
            wallTiles[width * idx + width - 1] = GenWallTile();
        }


        GameObject roomObject;

        if (roomType == RoomType.BossRoom) {
            roomObject = Instantiate(roomTemplate);
        } else if (roomType == RoomType.StartRoom) {
            roomObject = Instantiate(startRoomTemplate);
        } else
        {
            roomObject = Instantiate(roomTemplate);
        }

        roomObject.transform.parent = grid.transform;
        roomObject.name = string.Format("room_{0}_{1}", roomType, roomCounter++);

        /*
        This is a dirty filthy hack and I hate this
        These next two lines fill me with hatred and anger, but I would rather complete this game rather than fix this properly. 
        There is a conflict between the coordinate space of the local room, and the global walls/doorways/hallways. If you dig 
        into the code in the SingleRoomHandler::GenerateEnemies function, you see that I HARDCODE generate the enemy positions 
        based on the rooms current transformation.
        
        This is hardcoded bad juju, but I can't think of a nice way to do enemy generation off the top of my head
    */
        BoundsInt fixMeRoomBounds = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(globalBounds.size.x, globalBounds.size.y, 1));
        roomObject.transform.position = new Vector3(globalBounds.position.x, globalBounds.position.y, 0);

        Tilemap roomTilemap = roomObject.GetComponent<Tilemap>();
        roomTilemap.SetTilesBlock(fixMeRoomBounds, roomTiles);

        // Hallway tilemap generation
        // Doorway tilemap generation
        TileBase[] hallwayTiles = new TileBase[size];
        TileBase[] doorwayTiles = new TileBase[size];

        // top door
        int idx2 = width / 2;
        SetDoorway(idx2 - 1, hallwayTiles, doorwayTiles, wallTiles);
        SetDoorway(idx2, hallwayTiles, doorwayTiles, wallTiles);

        // bottom door
        SetDoorway(size - idx2, hallwayTiles, doorwayTiles, wallTiles);
        SetDoorway(size - idx2 - 1, hallwayTiles, doorwayTiles, wallTiles);

        // left door
        idx2 = height / 2;
        SetDoorway(idx2 * width, hallwayTiles, doorwayTiles, wallTiles);
        SetDoorway((idx2 + 1) * width, hallwayTiles, doorwayTiles, wallTiles);

        // bottom door
        SetDoorway((idx2 * width) + width - 1, hallwayTiles, doorwayTiles, wallTiles);
        SetDoorway(((idx2 + 1) * width) + width - 1, hallwayTiles, doorwayTiles, wallTiles);

        wallsTilemap.SetTilesBlock(globalBounds, wallTiles);
        hallwayTilemap.SetTilesBlock(globalBounds, hallwayTiles);
        doorsTilemap.SetTilesBlock(globalBounds, doorwayTiles);

    }

    TileBase GenFloorTile()
    {
        return Random.value > 0.5 ? floor1 : floor2;
    }

    TileBase GenWallTile()
    {
        return Random.value > 0.5 ? wall1 : wall2;
    }

    TileBase GenDoorTile()
    {
        return door;
    }

    void SetDoorway(int idx, TileBase[] hallwayTiles, TileBase[] doorwayTiles, TileBase[] wallTiles)
    {
        hallwayTiles[idx] = GenFloorTile();
        doorwayTiles[idx] = GenDoorTile();
        wallTiles[idx] = null;
    }
}
