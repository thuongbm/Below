using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tileMap;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tileMap.WorldToCell(mousePos);

            if (tileMap.GetTile(cellPos) != null)
            {
                if (IsAccessible(cellPos))
                {
                    tileMap.SetTile(cellPos, null);
                    Debug.Log("Deleted");
                }

                else
                {
                    Debug.Log("Not Accessible");
                }
            }
        }
    }

    bool IsAccessible(Vector3Int tilePos)
    {
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, -1, 0),
        };

        foreach (Vector3Int direction in directions)
        {
            if (tileMap.GetTile(tilePos + direction) == null)
            {
                Debug.Log("Is Accessible?");
                return true;
            }
        }
        return false;
    }
}
