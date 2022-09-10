using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject winCanvas;
    private void Awake()
    {
        EnemyAttack.PlayerDeath += EndOfTheGame;
        LevelManager.EndOfTheGame += WinTheGame;
    }
    private void OnDestroy()
    {
        EnemyAttack.PlayerDeath -= EndOfTheGame;
        LevelManager.EndOfTheGame -= WinTheGame;
    }
    private void EndOfTheGame()
    {
        loseCanvas.SetActive(true);
    }
    private void WinTheGame()
    {
        winCanvas.SetActive(true);
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
