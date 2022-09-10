using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName ="LevelObject/Level")]
public class LevelDataSO : ScriptableObject
{
    public Vector2Int playerPlace;
    public Vector2Int enemyPlace;
    public int widgth;
    public int height;
    public List<Vector2Int> upWallPositions;
    public List<Vector2Int> rightWallPositions;
    public Vector2Int endPoint;
}
