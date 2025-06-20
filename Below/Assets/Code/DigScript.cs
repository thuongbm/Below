using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tileMap;
    public TileBase[] damageStages;
    public float diggerDelay = 1.2f;
    
    private Camera cam;
    private Vector3Int currentTilePos;
    private float holdTime = 0f;
    
    
    void Start()
    {
        cam = Camera.main;
        currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
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
                if (IsAccessible(cellPos) && cellPos == currentTilePos)
                {
                    // thêm thuật toán
                    holdTime += Time.deltaTime;
                    float progress = holdTime / diggerDelay;
                    
                    
                    // 
                    if (progress >= 1)
                    {
                        tileMap.SetTile(cellPos, null);
                        Debug.Log("Deleted");
                    }
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
