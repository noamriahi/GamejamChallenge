using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    Dictionary<Vector2Int, GameObject> upsWalls;
    Dictionary<Vector2Int, GameObject> rightWalls;
    private void Awake()
    {
        LevelManager.ChangeLevel += GetNewWall;
    }
    private void OnDestroy()
    {
        LevelManager.ChangeLevel -= GetNewWall;
    }
    private void GetNewWall()
    {

        FillMyWalls();
    }

    private void FillMyWalls()
    {
        upsWalls = new Dictionary<Vector2Int, GameObject>();
        rightWalls = new Dictionary<Vector2Int, GameObject>();
        Debug.Log(upsWalls.Count);
        Debug.Log(rightWalls.Count);
        Debug.Log(FindObjectsOfType<Wall>().Length);
        foreach (Wall wall in FindObjectsOfType<Wall>())
        {
            if (wall.GetSide() == Side.RIGHT)
            {
                if (rightWalls.ContainsKey(wall.GetCoordinate()))
                {
                    Debug.LogError($"right contain {wall.GetCoordinate()}");
                    continue;
                }
                rightWalls.Add(wall.GetCoordinate(), wall.gameObject);
                
            }
            else
            {
                if (upsWalls.ContainsKey(wall.GetCoordinate()))
                {
                    Debug.LogError($"contain {wall.GetCoordinate()}");
                    continue;
                }
                upsWalls.Add(wall.GetCoordinate(), wall.gameObject);
               
            }
        }
        Debug.Log("done");
    }
    public bool HaveRightWall(Vector2Int coordinate)
    {
        return rightWalls.ContainsKey(coordinate);
    }
    public bool HaveUpWall(Vector2Int coordinate)
    {
        return upsWalls.ContainsKey(coordinate);
    }
}
