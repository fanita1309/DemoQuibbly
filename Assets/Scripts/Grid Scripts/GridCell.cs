using UnityEngine;


public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;
    public bool cellFull= false;
    public GameObject objectInCell;
    public CellOwner owner;
    public int cellID; // ID único para cada celda
}
