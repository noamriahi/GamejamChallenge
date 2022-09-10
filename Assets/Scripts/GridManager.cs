using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] float cellSize = 10f;
    Grid grid = new Grid(10f);
    public Vector3 GetPositionByCoordinate(int x, int y)
    {
        return grid.GetPositionFromCoordinates(x, y);
    }
    public Vector2Int GetCoordinateByPosition(Vector3 position)
    {
        return grid.GetCoordinatesFromPosition(position);
    }
    public Vector3 GetPositionByCoordinate(Vector2Int coordinates)
    {
        return GetPositionByCoordinate(coordinates.x, coordinates.y);
    }

}
