using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    float cellSize;
    public Grid(float _cellSize)
    {
        cellSize = _cellSize;
    }
    public Vector3 GetPositionFromCoordinates(int x, int y)
    {
        float positionX = x * cellSize;
        float positionY = y * cellSize;

        return new Vector3(positionX, 0, positionY);
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.z / cellSize);
        return new Vector2Int(x, y);
    }
}
