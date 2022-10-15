using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    [Range(2, 9)] public int _gridWidth = 1;
    [Range(2, 7)] public int _gridHeight = 1;

    public Dictionary<Vector2, Cell> Cells { get; private set; }

    public override void Initialize()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        Cell cell;
        Cells = new Dictionary<Vector2, Cell>();
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                cell = transform.GetChild(y * _gridWidth + x).GetComponent<Node>();
                cell.Initialize(true, new CellCoords { Pos = new Vector3(x, y) });
                cell.name = "(" + x + "," + y + ")";
                Cells.Add(new Vector2(x, y), cell);
            }
        }

        foreach (var node in Cells.Values)
            node.CacheNeighbors();
    }

    public Cell GetTileAtPosition(Vector2 pos) => Cells.TryGetValue(pos, out var tile) ? tile : null;

    public void CheckIsPlaceble(Item item)
    {
        foreach (Cell c in Cells.Values)
            c.ResetColor();

        Vector2 vec = item.hittedCell.Coords.Pos;
        bool isOutOfRange = false;
        if (vec.y + item.Dimensions.GetLength(1) > _gridHeight || vec.x + item.Dimensions.GetLength(0) > _gridWidth)
            isOutOfRange = true;

        for (int y = 0; y < item.Dimensions.GetLength(1); y++)
        {
            vec.y = item.hittedCell.Coords.Pos.y + y;
            for (int x = 0; x < item.Dimensions.GetLength(0); x++)
            {
                vec.x = item.hittedCell.Coords.Pos.x + x;
                if (Cells.ContainsKey(vec))
                    Cells[vec].SetPlaceabilityColor(isOutOfRange);
            }
        }
    }

    public bool ItemReleased(Item item)
    {
        Vector2 vec = item.hittedCell.Coords.Pos;
        bool isPlaceble = true;
        if (vec.y + item.Dimensions.GetLength(1) > _gridHeight || vec.x + item.Dimensions.GetLength(0) > _gridWidth)
        {
            return false;
        }

        for (int y = 0; y < item.Dimensions.GetLength(1); y++)
        {
            vec.y = item.hittedCell.Coords.Pos.y + y;
            for (int x = 0; x < item.Dimensions.GetLength(0); x++)
            {
                vec.x = item.hittedCell.Coords.Pos.x + x;
                if (!Cells[vec].Walkable)
                {
                    isPlaceble = false;
                    break;
                }
            }
            if (!isPlaceble)
                break;
        }


        for (int y = 0; y < item.Dimensions.GetLength(1); y++)
        {
            vec.y = item.hittedCell.Coords.Pos.y + y;
            for (int x = 0; x < item.Dimensions.GetLength(0); x++)
            {
                vec.x = item.hittedCell.Coords.Pos.x + x;
                Cells[vec].BuildedOn(isPlaceble);
            }
        }

        return isPlaceble;
    }

    public void ResetField()
    {
        foreach (Cell c in Cells.Values)
            c.ResetColor();
    }

    public Vector3 GetMiddlePosition(Item item)
    {
        Vector3 placedPosition = Vector3.zero;
        float TopUpCell = item.hittedCell.Coords.Pos.y + item.Dimensions.GetLength(1) - 1;
        float TopRightCell = item.hittedCell.Coords.Pos.x + item.Dimensions.GetLength(0) - 1;

        float differenceX = Cells[new Vector2(TopRightCell, item.hittedCell.Coords.Pos.y)].GetCornerPosition(RectangeCorner.DownRight).x - item.hittedCell.GetCornerPosition(RectangeCorner.DownLeft).x;
        float differenceY = Cells[new Vector2(item.hittedCell.Coords.Pos.x, TopUpCell)].GetCornerPosition(RectangeCorner.UpLeft).y - item.hittedCell.GetCornerPosition(RectangeCorner.DownLeft).y;

        differenceX *= 0.5f;
        differenceY *= 0.5f;

        placedPosition.x = differenceX + item.hittedCell.GetCornerPosition(RectangeCorner.DownLeft).x;
        placedPosition.y = differenceY + item.hittedCell.GetCornerPosition(RectangeCorner.DownLeft).y;

        return placedPosition;
    }



}
