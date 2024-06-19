using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField]private float scrollAmount, Movespeed;
    [SerializeField]private Vector3 MoveDirection;

    void Update()
    {
       transform.position += MoveDirection * Movespeed * Time.deltaTime;

        if(transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - MoveDirection * scrollAmount;
        }
    }
}