using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private bool canMove = false;
    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private SpriteRenderer _sr;

    public void First_Set(float time)
    {
        speed = Random.Range(7, 12);
        StartCoroutine(Change_Transperency(true));
        StartCoroutine(Destroy_Self(time));
        StartCoroutine(SetDirection());
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

    private IEnumerator SetDirection()
    {
        while (true)
        {
            canMove = true;

            if (_rigid.position.x < -1850)
                dir = Vector2.right;
            else if (_rigid.position.x > 1850)
                dir = Vector2.left;
            else
            {
                int r = Random.Range(0, 2);

                if (r == 0)
                    dir = Vector2.right;
                else
                    dir = Vector2.left;
            }

            StartCoroutine(StopMove());
            yield return new WaitForSeconds(7);
        }
    }

    private IEnumerator StopMove()
    {
        yield return new WaitForSeconds(4);
        canMove = false;
    }


    private IEnumerator Destroy_Self(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(Change_Transperency(false));
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }


    WaitForSeconds WFS_FDT = new WaitForSeconds(0.02f);
    private IEnumerator Change_Transperency(bool isAppearing)
    {
        float time = 4.0f;
        float temp = 0.0f;

        if (isAppearing)
        {
            gameObject.SetActive(true);

            temp = 0.0f;

            while (temp < time)
            {
                temp += Time.fixedDeltaTime;
                _sr.color = new Color(1f, 1f, 1f, temp / time);
                yield return WFS_FDT;
            }
        }
        else
        {
            temp = 4.0f;

            while (temp > 0.0f)
            {
                temp -= Time.fixedDeltaTime;
                _sr.color = new Color(1f, 1f, 1f, temp / time);
                yield return WFS_FDT;
            }

            gameObject.SetActive(false);
        }


    }
}
