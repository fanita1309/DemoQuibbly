using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer), typeof(GridCell))]

public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color highlightColor = Color.cyan;
    public Color posColor = Color.green;
    public Color negColor = Color.red;
    private Color originalColor;
    public GridCell gridCell;
    public GameObject[] backgrounds;
    private bool setBackground = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridCell = GetComponent<GridCell>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (!setBackground) SetBackground();
    }

    void OnMouseEnter()
    {
        if (!GameManager.Instance.playingCard)
        {
            spriteRenderer.color = highlightColor;
        }
        else if (gridCell.cellFull || gridCell.gridIndex.x > 1)
        {
            spriteRenderer.color = negColor;
        }
        else spriteRenderer.color = posColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color=originalColor;
    }

    private void SetBackground()
    {
        if (gridCell.gridIndex.x %2 !=0)
        {
            backgrounds[0].SetActive(true);
        }
        if (gridCell.gridIndex.y % 2 != 0)
        {
            backgrounds[1].SetActive(true);
        }
        setBackground = true;
    }
}
