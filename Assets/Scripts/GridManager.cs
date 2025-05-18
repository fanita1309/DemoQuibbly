using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridOwner
{
    Player,
    Enemy
}

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;
    public GameObject gridCellPrefab;
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;
    public GridOwner owner; // Define a quién pertenece este grid

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2(width / 2.0f - 0.5f, height / 2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                Vector2 spawnPosition = gridPosition - centerOffset;

                GameObject gridCell = Instantiate(gridCellPrefab);
                gridCell.transform.SetParent(transform);
                gridCell.transform.localPosition = spawnPosition;

                GridCell cellScript = gridCell.GetComponent<GridCell>();
                cellScript.gridIndex = gridPosition;
                cellScript.owner = (owner == GridOwner.Player) ? CellOwner.Player : CellOwner.Enemy;

                gridCells[x, y] = gridCell;
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (!CanPlaceCardInCell(cell))
            {
                Debug.Log("❌ No se puede colocar en esta celda.");
                return false;
            }

            if (cell.cellFull)
                return false;

            GameObject newObj = Instantiate(obj, cell.transform.position, Quaternion.identity);
            newObj.transform.SetParent(transform);
            gridObjects.Add(newObj);
            cell.objectInCell = newObj;
            cell.cellFull = true;
            return true;
        }

        return false;
    }

    // Solo permite que el jugador coloque en su propio grid
    protected virtual bool CanPlaceCardInCell(GridCell cell)
    {
        return owner == GridOwner.Player && cell.owner == CellOwner.Player;
    }
}