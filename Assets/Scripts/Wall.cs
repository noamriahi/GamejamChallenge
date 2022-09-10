using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { UP,RIGHT}
public class Wall : MonoBehaviour
{
    [SerializeField] Side side;
    [SerializeField] Vector2Int gridPosition;
    GridManager gridManager;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }
    private void OnEnable()
    {
        gridPosition = gridManager.GetCoordinateByPosition(transform.position);
    }
    public Vector2Int GetCoordinate()
    {
        return gridPosition;
    }
    public Side GetSide()
    {
        return side;
    }
}
