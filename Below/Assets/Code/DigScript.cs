using UnityEngine;
using UnityEngine.Tilemaps;

public class DigScript : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase[] damageStages; // damageStages[3] là tile nứt
    public float diggerDelay = 4f;

    private Camera cam;
    private Vector3Int currentTilePos;
    private float holdTime = 0f;

    void Start()
    {
        cam = Camera.main;
        currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Giữ chuột trái
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tileMap.WorldToCell(mousePos);

            if (tileMap.GetTile(cellPos) != null && IsAccessible(cellPos))
            {
                if (cellPos == currentTilePos)
                {
                    holdTime += Time.deltaTime;

                    if (holdTime >= diggerDelay / 2f && tileMap.GetTile(cellPos) != damageStages[3])
                    {
                        tileMap.SetTile(cellPos, damageStages[3]);
                        Debug.Log("Cracked");
                    }

                    if (holdTime >= diggerDelay)
                    {
                        tileMap.SetTile(cellPos, null); // Xoá tile
                        Debug.Log("Deleted");
                        holdTime = 0f;
                        currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
                    }
                }
                else
                {
                    // Mới chuyển sang tile khác → reset
                    currentTilePos = cellPos;
                    holdTime = 0f;
                }
            }
            else
            {
                // Không có tile hoặc không được đào
                holdTime = 0f;
                currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            }
        }
        else
        {
            // Nhả chuột → reset
            holdTime = 0f;
            currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
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
                return true; // Có không khí xung quanh
            }
        }
        return false;
    }
}
