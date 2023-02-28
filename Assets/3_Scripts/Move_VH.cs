using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_VH : MonoBehaviour
{
    public bool isHorizontal = true;

    public int dir = 1;

    public float speed;
    public float former_side;
    public float latter_side;


    [SerializeField] private Rigidbody2D _rigid;


    private void Start()
    {
        var position = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (isHorizontal)
        {
            _rigid.MovePosition(_rigid.position + Vector2.right * dir * speed * Time.fixedDeltaTime);
            if (_rigid.position.x > latter_side)
            {
                dir = -1;
            }
            if (_rigid.position.x < former_side)
            {
                dir = 1;
            }
        }
        else
        {
            _rigid.MovePosition(_rigid.position + Vector2.up * dir * speed * Time.fixedDeltaTime);

            if (_rigid.position.y > latter_side)
            {
                dir = -1;
            }
            if (_rigid.position.y < former_side)
            {
                dir = 1;
            }
        }

        
    }

}
