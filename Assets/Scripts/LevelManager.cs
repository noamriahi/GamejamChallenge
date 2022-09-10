using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<LevelDataSO> levelDataSO;
    [SerializeField] GameObject theseus;
    [SerializeField] GameObject minotaur;
    [SerializeField] GameObject tile;
    [SerializeField] Transform tileParent;
    [SerializeField] GameObject upWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] Transform wallParent;
    [SerializeField] GameObject endPoint;

    [SerializeField] TMP_Text levelText;

    public static Action EndOfTheGame;
    public static Action ChangeLevel;

    GridManager gridManager;
    List<GameObject> levelObjects = new List<GameObject>();
    GameObject player;
    GameObject enemy;
    int level = 0;

    private void Awake()
    {
        FinishLevel.CollideEndPoint += NextLevel;
        gridManager = FindObjectOfType<GridManager>();
        player = Instantiate(theseus, gridManager.GetPositionByCoordinate(levelDataSO[level].playerPlace), Quaternion.identity);
        enemy = Instantiate(minotaur, gridManager.GetPositionByCoordinate(levelDataSO[level].enemyPlace), Quaternion.identity);
    }
    private void OnDestroy()
    {
        FinishLevel.CollideEndPoint -= NextLevel;
    }
    void Start()
    {
        StartNextLevel();
        levelText.text = $"Level {level}";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PreviousLevel();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DestroyThisLevel());
        }
    }

    private void StartNextLevel()
    {
        player.transform.position = gridManager.GetPositionByCoordinate(levelDataSO[level].playerPlace);
        enemy.transform.position = gridManager.GetPositionByCoordinate(levelDataSO[level].enemyPlace);


        for (int i = 0; i < levelDataSO[level].widgth; i++)
        {
            for (int j = 0; j < levelDataSO[level].height; j++)
            {
                GameObject tileGO = Instantiate(tile, gridManager.GetPositionByCoordinate(i, j), Quaternion.identity, tileParent);
                levelObjects.Add(tileGO);
            }
        }
        foreach (var upWallCoord in levelDataSO[level].upWallPositions)
        {
            GameObject upWallGO = Instantiate(upWall, gridManager.GetPositionByCoordinate(upWallCoord), Quaternion.identity, wallParent);
            levelObjects.Add(upWallGO);
        }
        foreach (var rightWallCoord in levelDataSO[level].rightWallPositions)
        {
            GameObject rightWallGO = Instantiate(rightWall, gridManager.GetPositionByCoordinate(rightWallCoord), Quaternion.identity, wallParent);
            levelObjects.Add(rightWallGO);
        }
        GameObject endPointGO = Instantiate(endPoint, gridManager.GetPositionByCoordinate(levelDataSO[level].endPoint), Quaternion.identity);
        levelObjects.Add(endPointGO);
        ChangeLevel?.Invoke();
    }
    IEnumerator DestroyThisLevel()
    {
       foreach(var go in levelObjects)
       {
           Destroy(go);
       }
        levelObjects.Clear();
        yield return null;
        StartNextLevel();
    }
    public void NextLevel()
    {
        level++;
        levelText.text = $"Level {level}";
        if (level>levelDataSO.Count-1)
        {
            level = levelDataSO.Count - 1;
            EndOfTheGame?.Invoke();
        }
        else
        {
            StartCoroutine(DestroyThisLevel());
        }
    }
    public void PreviousLevel()
    {
        level--;
        levelText.text = $"Level {level}";
        if (level < 0)
        {
            level = 0;
        }
        else
        {
            StartCoroutine(DestroyThisLevel());
            
        }
    }
}
