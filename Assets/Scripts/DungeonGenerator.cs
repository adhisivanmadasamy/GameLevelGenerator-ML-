using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public FlaskReq flaskreq;
    public RawImage MapImg;
    public Texture2D mapTexture;

    List<List<int>> dungeonMatrix = new List<List<int>>();
    List<List<int>> roomMatrix = new List<List<int>>();

    public GameObject RoomTile, EmptyTile;
    public GameObject TestProp;

    List<GameObject> instantiatedTiles = new List<GameObject>();
    List<GameObject> instantiatedProps = new List<GameObject>();

    public GameObject Floor, Wall, Door, Bench, Table, Collectable, BookShelf, Torch, Crate;

    public int RoomsUpdated = 0;
    public void to3D()
    {
        dungeonMatrix.Clear(); // Clear existing matrix (if any)

        // Partition dungeonData into 8 rows (each row containing 8 elements)
        for (int i = 0; i < 8; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < 8; j++)
            {
                int index = i * 8 + j;
                row.Add(flaskreq.DungeonData[index]);
            }
            dungeonMatrix.Add(row);
        }

        
    }

    IEnumerator roomTo3D(int x, int z)
    {
        roomMatrix.Clear();

        
        // Reconstruct the 16x16 matrix from the flat list
        for (int i = 0; i < 16; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < 16; j++)
            {
                int index = i * 16 + j;
                row.Add(flaskreq.RoomData[index]);
            }
            roomMatrix.Add(row);
        }

        // Debug log the reconstructed matrix
        for (int i = 0; i < roomMatrix.Count; i++)
        {
            Debug.Log($"Row {i}: {string.Join(", ", roomMatrix[i])}");
        }

        yield return StartCoroutine(GenerateProps(x, z));
    }



    public void DisplayDungeonMap()
    {
        mapTexture = new Texture2D(8, 8);
        mapTexture.filterMode = FilterMode.Point;

        for (int i = 0; i < 64; i++)
        {
            // Determine color based on integer value (0 for black, 1 for white)
            Color pixelColor = (flaskreq.DungeonData[i] == 0) ? Color.black : Color.white;

            // Calculate pixel position in Texture2D
            int x = i % 8;
            int y = i / 8;

            // Set pixel color in Texture2D
            mapTexture.SetPixel(x, y, pixelColor);
        }

        mapTexture.Apply();
        MapImg.texture = mapTexture;

        GenerateTiles();

    }
    
    public void GenerateTiles()
    {
        float tileSize = 16f;        

        to3D();

        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++)
            {
                Vector3 pos = new Vector3(x * (tileSize),0,z * (tileSize));
                if (dungeonMatrix[x][z] == 1)
                {
                    StartCoroutine(StartRoomReq(x,z));                
                }
                else
                {
                    GameObject tile = Instantiate(EmptyTile, pos, Quaternion.identity);
                    instantiatedTiles.Add(tile);
                }
                         
                
            }
        }        
    }

    IEnumerator StartRoomReq(int x, int z)
    {
        yield return StartCoroutine(flaskreq.GetRoomData());  // Wait for the GetRoomData coroutine to complete
        Debug.Log(x + "-" + z + " " + "RoomReq: " + (RoomsUpdated + 1));        
        yield return StartCoroutine(roomTo3D(x, z));
    }

    IEnumerator GenerateProps(int x, int z)
    {
        Debug.Log("Generating Room: " + (RoomsUpdated + 1));
        float tileSize = 16f;

        // Check if roomMatrix has the expected size
        if (roomMatrix.Count != 16 || roomMatrix.Any(row => row.Count != 16))
        {
            Debug.LogError("roomMatrix does not have the expected size of 16x16.");
            yield break;
        }

        for (int x2 = 0; x2 < 16; x2++)
        {
            for (int z2 = 0; z2 < 16; z2++)
            {
                Vector3 pos = new Vector3(x2 + (tileSize * x) - 8, 0, z2 + (tileSize * z) - 8);

                if (roomMatrix[x2].Count > z2)
                {
                    GameObject prop = FindProp(roomMatrix[x2][z2]);
                    if (prop != null)
                    {
                        GameObject instantiatedProp = Instantiate(prop, pos, Quaternion.identity);
                        instantiatedProps.Add(instantiatedProp);
                    }
                    else
                    {
                        Debug.LogError("FindProp returned null for value: " + roomMatrix[x2][z2]);
                    }
                }
                else
                {
                    Debug.LogError($"Index out of range for roomMatrix[{x2}] when accessing element {z2}.");
                }
            }
        }

        RoomsUpdated++;
        yield return RoomsUpdated;
    }


    public void ClearTiles()
    {
        foreach (GameObject tile in instantiatedTiles)
        {
            Destroy(tile);
        }
                
        instantiatedTiles.Clear();
        ClearProps();
    }

    public void ClearProps()
    {
        foreach (GameObject prop in instantiatedProps)
        {
            Destroy(prop);

        }
        instantiatedProps.Clear();
    }

    public GameObject FindProp(int num)
    {
        if (num == 0) return Floor;
        else if (num == 1) return Wall;
        else if (num == 2) return Bench;
        else if (num == 3) return Table;
        else if (num == 4) return Collectable;
        else if (num == 5) return BookShelf;
        else if (num == 6) return Torch;
        else if (num == 7) return Door;
        else if (num == 8) return Crate;
        else return null;
    }



}
