using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    PlayerController playerController;
    EnemyController enemyController;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();
        PlayerController.EndOfMovement += EndPlayerTurn;
        EnemyController.EndOfEnemyTurn += EndEnemyTurn;
    }
    private void OnDestroy()
    {
        PlayerController.EndOfMovement -= EndPlayerTurn;
        EnemyController.EndOfEnemyTurn -= EndEnemyTurn;
    }
    private void EndPlayerTurn()
    {

        Debug.Log("end of player movement");
        enemyController.StartMove();
    }
    private void EndEnemyTurn()
    {
        playerController.StartTurn();
    }
}
