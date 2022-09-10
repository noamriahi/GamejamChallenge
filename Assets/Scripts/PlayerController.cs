using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    enum MovementSide { RIGHT,LEFT,UP,DOWN}

    Animator animator;
    GridManager gridManager;
    Vector2Int currentPosition;
    bool canPlay = true;
    WallManager wallManager;
    public static Action EndOfMovement;
    
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        animator = GetComponent<Animator>();
        wallManager = FindObjectOfType<WallManager>();
        LevelManager.ChangeLevel += GetNewPosition;
    }
    private void OnDestroy()
    {
        LevelManager.ChangeLevel -= GetNewPosition;
    }
    private void Start()
    {
        currentPosition = gridManager.GetCoordinateByPosition(transform.position);
    }
    public void GetNewPosition()
    {
        currentPosition = gridManager.GetCoordinateByPosition(transform.position);
    }

    void Update()
    {
        if (canPlay)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && CanWalk(MovementSide.UP))
            {
                MakeAMove(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && CanWalk(MovementSide.DOWN))
            {
                MakeAMove(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && CanWalk(MovementSide.LEFT))
            {
                MakeAMove(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && CanWalk(MovementSide.RIGHT))
            {
                MakeAMove(1, 0);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                EndOfMovement?.Invoke();
            }
        }

    }

    private bool CanWalk(MovementSide side)
    {
        if (side == MovementSide.RIGHT)
        {
            return !wallManager.HaveRightWall(currentPosition);
        }
        else if (side == MovementSide.LEFT)
        {
            Debug.Log(currentPosition + Vector2Int.left);
            return !wallManager.HaveRightWall(currentPosition + Vector2Int.left);
            
        }
        else if (side == MovementSide.DOWN)
        {
            return !wallManager.HaveUpWall(currentPosition + Vector2Int.down);
        }
        else
        {
            return !wallManager.HaveUpWall(currentPosition);
        }
    }

    private void MakeAMove(int x,int y)
    {
        currentPosition.x += x;
        currentPosition.y += y;

        canPlay = false;
        animator.SetBool("Walk", true);
        StartCoroutine(MakeMovement());

    }

    IEnumerator MakeMovement()
    {
        Vector3 startPosition = transform.position;

        Vector3 endPosition = gridManager.GetPositionByCoordinate(currentPosition);
        transform.LookAt(endPosition);
        float travelPercentage = 0f;
        while(travelPercentage < 1f)
        {
            travelPercentage += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
            yield return new WaitForEndOfFrame();
        }

        animator.SetBool("Walk", false);
        EndOfMovement?.Invoke();
    }

    public void StartTurn()
    {
        canPlay = true;
    }
    public void StopMovemnt()
    {
        StopAllCoroutines();
        animator.SetBool("Walk", false);
        canPlay = true;
    }
}
