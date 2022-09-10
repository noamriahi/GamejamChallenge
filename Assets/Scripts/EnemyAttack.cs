using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static Action PlayerDeath;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetTrigger("Attack");
            Invoke("Death", 1f);
        }
    }
    private void Death()
    {
        PlayerDeath?.Invoke();
    }
}
