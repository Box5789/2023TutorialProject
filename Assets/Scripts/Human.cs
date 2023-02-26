using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private bool canMove = false;
    [SerializeField] private float speed = 2.0f;

    [SerializeField] private Rigidbody2D _rigid;

    private void OnEnable()
    {
        Invoke("SetDirection", 1.0f);
        Invoke("Destroy_Self", 300.0f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(canMove)
            _rigid.MovePosition(_rigid.position + dir * speed * Time.fixedDeltaTime);
    }

    private void SetDirection()
    {
        canMove = true;

        if (_rigid.position.x < -23)
            dir = Vector2.right;
        else if (_rigid.position.x > 23)
            dir = Vector2.left;
        else
        {
            int r = Random.Range(0, 2);

            if (r == 0)
                dir = Vector2.right;
            else
                dir = Vector2.left;
        }
        Invoke("CanMove", 5.0f);
    }

    private void CanMove()
    {
        canMove = false;

        Invoke("SetDirection", 1.5f);
    }


    private void Destroy_Self()
    {
        Destroy(gameObject);
    }
}
