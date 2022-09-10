using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    [SerializeField] float speed = 1f;

    public static Action EndOfEnemyTurn;

    enum MovementSide { RIGHT,LEFT,UP,DOWN };
    Vector2Int currentPosition;
    GridManager gridManager;
    Animator animator;
    bool firstTurn = true;
    WallManager wallManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        wallManager = FindObjectOfType<WallManager>();
        LevelManager.ChangeLevel += GetNewPosition;
    }
    private void OnDestroy()
    {
        LevelManager.ChangeLevel -= GetNewPosition;
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        currentPosition = gridManager.GetCoordinateByPosition(transform.position);
        animator = GetComponent<Animator>();
    }
    public void GetNewPosition()
    {
        StopAllCoroutines();
        if(animator!=null)
            animator.SetBool("Walk", false);
        currentPosition = gridManager.GetCoordinateByPosition(transform.position);
        EndOfEnemyTurn?.Invoke();
    }

    public void StartMove()
    {
      
        Vector2Int playerPosition = gridManager.GetCoordinateByPosition(player.transform.position);
        if(Vector2Int.Distance(currentPosition, playerPosition)> Vector2Int.Distance(currentPosition + Vector2Int.right, playerPosition) && CanWalk(currentPosition + Vector2Int.right,MovementSide.RIGHT))
        {
            currentPosition = currentPosition + Vector2Int.right;
            MakeAMove();
        }
        else if (Vector2Int.Distance(currentPosition, playerPosition) > Vector2Int.Distance(currentPosition + Vector2Int.left, playerPosition) && CanWalk(currentPosition + Vector2Int.left, MovementSide.LEFT))
        {
            currentPosition = currentPosition + Vector2Int.left;
            MakeAMove();
        }
        else if (Vector2Int.Distance(currentPosition, playerPosition) > Vector2Int.Distance(currentPosition + Vector2Int.up, playerPosition) && CanWalk(currentPosition + Vector2Int.up, MovementSide.UP))
        {
            currentPosition = currentPosition + Vector2Int.up;
            MakeAMove();
        }
        else if (Vector2Int.Distance(currentPosition, playerPosition) > Vector2Int.Distance(currentPosition + Vector2Int.down, playerPosition) && CanWalk(currentPosition + Vector2Int.down, MovementSide.DOWN))
        {
            currentPosition = currentPosition + Vector2Int.down;
            MakeAMove();
        }
        else
        {
            firstTurn = true;
            animator.SetBool("Walk", false);
            EndOfEnemyTurn?.Invoke();
        }
       
    }
    private void MakeAMove()
    {
        animator.SetBool("Walk", true);
        StartCoroutine(StartWalking());
    }
    IEnumerator StartWalking()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = gridManager.GetPositionByCoordinate(currentPosition);
        transform.LookAt(endPosition);
        float travelPercentage = 0f;
        while (travelPercentage < 1f)
        {
            travelPercentage += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
            yield return new WaitForEndOfFrame();
        }
        
        if (firstTurn)
        {
            firstTurn = false;
            StartMove();
        }
        else
        {
            firstTurn = true;
            animator.SetBool("Walk", false);
            EndOfEnemyTurn?.Invoke();
        }
    }
    private bool CanWalk(Vector2Int nextStep, MovementSide side)
    {
        if(side == MovementSide.RIGHT)
        {
            return !wallManager.HaveRightWall(currentPosition);
        }
        else if (side == MovementSide.LEFT)
        {
            return !wallManager.HaveRightWall(nextStep);
        }
        else if (side == MovementSide.DOWN)
        {
            return !wallManager.HaveUpWall(nextStep);
        }
        else
        {
            return !wallManager.HaveUpWall(currentPosition);
        }
    }

    public void StopMovement()
    {
        StopAllCoroutines();
        animator.SetBool("Walk", false);
    }
}
