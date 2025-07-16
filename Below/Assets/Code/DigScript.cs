using UnityEngine;
using UnityEngine.Tilemaps;

public class DigScript : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase[] damageStages; // damageStages[3] là tile nứt
    public float diggerDelay = 4f;
    public PickaxeControler pickaxeControler;

    private Camera cam;
    private Vector3Int currentTilePos;
    private float holdTime = 0f;

    // Thêm biến mới
    private TileBase originalTile;
    private bool isCracked = false;

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
                    pickaxeControler.StartMining(tileMap.CellToWorld(cellPos));

                    if (holdTime >= diggerDelay / 2f && !isCracked)
                    {
                        tileMap.SetTile(cellPos, damageStages[3]);
                        isCracked = true;
                        Debug.Log("Cracked");
                    }

                    if (holdTime >= diggerDelay)
                    {
                        tileMap.SetTile(cellPos, null); // Xoá tile
                        Debug.Log("Deleted");
                        holdTime = 0f;
                        currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
                        isCracked = false;
                        pickaxeControler.StopMining();
                    }
                }
                else
                {
                    // Mới chuyển sang tile khác → reset
                    if (isCracked)
                    {
                        tileMap.SetTile(currentTilePos, originalTile); // Trả tile cũ lại
                        isCracked = false;
                    }

                    currentTilePos = cellPos;
                    holdTime = 0f;
                    originalTile = tileMap.GetTile(cellPos); // Lưu lại tile gốc
                    pickaxeControler.StartMining(tileMap.CellToWorld(cellPos));
                }
            }
            else
            {
                // Không có tile hoặc không được đào
                if (isCracked)
                {
                    tileMap.SetTile(currentTilePos, originalTile);
                    isCracked = false;
                }

                holdTime = 0f;
                currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            }
        }
        else
        {
            // Nhả chuột → reset lại nếu có nứt
            if (isCracked)
            {
                tileMap.SetTile(currentTilePos, originalTile);
                isCracked = false;
            }

            holdTime = 0f;
            currentTilePos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            pickaxeControler.StopMining();
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
