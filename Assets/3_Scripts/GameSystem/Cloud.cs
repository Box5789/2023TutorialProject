using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D _rigid;

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigid.MovePosition(_rigid.position + Vector2.right * speed * Time.fixedDeltaTime);
        if (_rigid.position.x > 2300)
        {
            transform.position += new Vector3(-4800, 0,0);
        }
    }
}
