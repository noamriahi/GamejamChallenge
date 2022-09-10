using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public static Action CollideEndPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            GetComponent<PlayerController>().StopMovemnt();
            FindObjectOfType<EnemyController>().StopMovement();
            CollideEndPoint?.Invoke();
        }
    }
}
