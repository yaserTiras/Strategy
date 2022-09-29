using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Cell : MonoBehaviour
{

    [SerializeField]
    private Color _obstacleColor;

    [SerializeField] private Gradient _walkableColor;
    [SerializeField] protected bool walkable;
    [SerializeField] protected SpriteRenderer _renderer;

    private bool _selected;
    private Color _defaultColor;
    public ICoords Coords;
    public float GetDistance(Cell other) => Coords.GetDistance(other.Coords); // Helper to reduce noise in pathfinding
    public bool Walkable { get { return walkable; } set { walkable = value; } }
    public static event Action<Cell> OnHoverTile;

    public List<Cell> Neighbors { get; protected set; }
    public Cell Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public virtual void Initialize(bool walkable, ICoords coords)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = walkable ? _walkableColor.Evaluate(UnityEngine.Random.Range(0f, 1f)) : _obstacleColor;
        _defaultColor = _renderer.color;

        OnHoverTile += OnOnHoverTile;
        Coords = coords;
    }

    private void OnOnHoverTile(Cell selected) => _selected = selected == this;

    protected virtual void OnMouseDown()
    {
        if (!Walkable) return;
        OnHoverTile?.Invoke(this);
    }

    public Vector3 GetCornerPosition(RectangeCorner corner)
    {
        switch (corner)
        {
            case RectangeCorner.DownRight:
                return transform.TransformPoint(new Vector3(_renderer.sprite.bounds.max.x, _renderer.sprite.bounds.min.y, 0));
            case RectangeCorner.UpRight:
                return transform.TransformPoint(_renderer.sprite.bounds.max);
            case RectangeCorner.DownLeft:
                return transform.TransformPoint(_renderer.sprite.bounds.min);
            case RectangeCorner.UpLeft:
                return transform.TransformPoint(new Vector3(_renderer.sprite.bounds.min.x, _renderer.sprite.bounds.max.y, 0));
            default:
                return Vector3.zero;
        }
    }

    public abstract void CacheNeighbors();

    public void SetConnection(Cell nodeBase)
    {
        Connection = nodeBase;
    }

    public void BuildedOn(bool isBuilded)
    {
        if (!Walkable)
            return;

        Walkable = !isBuilded;
        ResetColor();
    }

    public void SetPlaceabilityColor(bool outOfRange)
    {
        Color c = _renderer.color;
        if (Walkable && !outOfRange)
        {
            c.a = 0.5f;
        }
        else
        {
            c = Color.red;
        }

        _renderer.color = c;
    }

    public void ResetColor()
    {
        _renderer.color = _defaultColor;
    }

    public void SetGCost(float g)
    {
        G = g;
    }

    public void SetHCost(float h)
    {
        H = h;
    }

    private void OnEnable() => OnHoverTile += OnOnHoverTile;
    private void OnDisable() => OnHoverTile -= OnOnHoverTile;
}

public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}


