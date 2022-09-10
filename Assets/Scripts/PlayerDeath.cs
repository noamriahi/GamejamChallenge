using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        EnemyAttack.PlayerDeath += DeathStart;
        animator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        EnemyAttack.PlayerDeath -= DeathStart;
    }
    private void DeathStart()
    {
        animator.SetTrigger("Death");
    }
}
