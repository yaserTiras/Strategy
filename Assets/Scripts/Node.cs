using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : Cell
{
    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

    public override void CacheNeighbors()
    {
        Neighbors = new List<Cell>();

        foreach (var tile in Dirs.Select(dir => FieldManager.instance.GetTileAtPosition(Coords.Pos + dir)).Where(tile => tile != null))
        {
            Neighbors.Add(tile);
        }
    }

    public override void Initialize(bool walkable, ICoords coords)
    {
        base.Initialize(walkable, coords);
    }
}


public struct CellCoords : ICoords
{
    public float GetDistance(ICoords other)
    {
        var dist = new Vector2Int(Mathf.Abs((int)Pos.x - (int)other.Pos.x), Mathf.Abs((int)Pos.y - (int)other.Pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    public Vector2 Pos { get; set; }
}
